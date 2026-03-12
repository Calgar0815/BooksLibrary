using ISBNCaller_Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace ISBNCaller_GUI.Correction
{
    class CorrectionFormWorker
    {
        #region Variables

        internal Label mLabelTitle { get; set; }
        internal Label mLabelSubTitle { get; set; }
        internal Label mLabelSeries { get; set; }
        internal Label mLabelCurrentSeries { get; set; }
        internal Label mLabelNoInSeries { get; set; }
        internal Label mLabelPublished { get; set; }
        internal Label mLabelFormat { get; set; }
        internal Label mLabelISBN13 { get; set; }
        internal Label mLabelISBN10 { get; set; }
        internal Label mLabelAuthor { get; set; }
        internal TextBox mTxtBoxTitle { get; set; }
        internal TextBox mTxtBoxSubTitle { get; set; }
        internal ComboBox mCmbBoxSeries { get; set; }
        internal TextBox mTxtBoxNoInSeries { get; set; }
        internal TextBox mTxtBoxPublished { get; set; }
        internal ComboBox mCmbBoxFormat { get; set; }
        internal TextBox mTxtBoxISBN13 { get; set; }
        internal TextBox mTxtBoxISBN10 { get; set; }
        internal DataGridView mDGVAuthors { get; set; }
        internal Button mBtnCalculateISBN13 { get; set; }
        internal Button mBtnCalculateISBN10 { get; set; }
        internal Button mBtnStartCorrection { get; set; }
        internal Button mBtnClose { get; set; }
        private Form1 mForm1 { get; set; }
        private DBReader.SearchStruct mToCorrect { get; set; }
        private List<ISBNWorker.DBAuthorStruct> mAuthorsFromDB { get; set; }
        private WriteTab mWriteTab { get; set; }
        private const string cCurrent = "Bisher: ";
        private const string cFirstName = "Vorname";
        private const string cName = "Nachname";
        private const string cAlreadyInDB = "In DB";
        private const string cDeleteFromDB = "Löschen";
        private const string cAuthorID = "AuthorID";

        #endregion
        #region Structs && Enums

        private struct ChangedItemsStruct
        {
            public ISBNWorker.DBBookStruct Book;
            public List<AuthorStruct> Authors;
            public string Series;
        }

        public struct AuthorStruct
        {
            public int AuthorID;
            public string PreName;
            public string Name;
            public bool AlreadyInDB;
            public bool ToDelete;
        }

        private enum CmbBoxEnum
        {
            Series,
            Format
        }

        #endregion
        #region Constructors

        internal CorrectionFormWorker(Form1 form1)
        {
            mForm1 = form1;
            mWriteTab = new WriteTab(form1);
        }

        #endregion
        #region Methods

        internal void PrepareForCorrection(DBReader.SearchStruct toCorrect)
        {
            mToCorrect = toCorrect;
            mTxtBoxTitle.Text = mToCorrect.Title;
            mTxtBoxSubTitle.Text = mToCorrect.SubTitle;
            mTxtBoxNoInSeries.Text = mToCorrect.NoInSeries;
            mTxtBoxPublished.Text = mToCorrect.PublishingDate;
            mTxtBoxISBN13.Text = mToCorrect.ISBN13;
            mTxtBoxISBN10.Text = mToCorrect.ISBN10;
            FillCmbBoxes(mCmbBoxSeries, CmbBoxEnum.Series);
            FillCmbBoxes(mCmbBoxFormat, CmbBoxEnum.Format);
            FillAuthorsDGV();
        }

        private void FillAuthorsDGV()
        {
            mDGVAuthors.ColumnCount = 5;
            mDGVAuthors.Columns[0].Name = cFirstName;
            mDGVAuthors.Columns[1].Name = cName;
            mDGVAuthors.Columns[2].Name = cAlreadyInDB;
            mDGVAuthors.Columns[3].Name = cDeleteFromDB;
            mDGVAuthors.Columns[4].Name = cAuthorID;
            mDGVAuthors.Columns[4].Visible = false;
            mDGVAuthors.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            mDGVAuthors.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            DBReader dbReader = new DBReader();
            mAuthorsFromDB = dbReader.GetAuthorsByBookID(mToCorrect.BookID);
            for (int index = 0; index < mAuthorsFromDB.Count; index++)
            {
                DataGridViewRow dgvRow = new DataGridViewRow();
                DataGridViewCell dgvCell = new DataGridViewTextBoxCell();
                dgvCell.Value = mAuthorsFromDB[index].PreName;
                dgvRow.Cells.Add(dgvCell);
                DataGridViewCell dgvCell2 = new DataGridViewTextBoxCell();
                dgvCell2.Value = mAuthorsFromDB[index].Name;
                dgvRow.Cells.Add(dgvCell2);
                DataGridViewCell dgvCell3 = new DataGridViewCheckBoxCell();
                dgvCell3.Value = true;
                dgvRow.Cells.Add(dgvCell3);
                DataGridViewCell dgvCell4 = new DataGridViewCheckBoxCell();
                dgvCell4.Value = false;
                dgvRow.Cells.Add(dgvCell4);
                DataGridViewCell dgvCell5 = new DataGridViewTextBoxCell();
                dgvCell5.Value = mAuthorsFromDB[index].AuthorID;
                dgvRow.Cells.Add(dgvCell5);
                mDGVAuthors.Rows.Add(dgvRow);
            } // for
        }

        private void FillCmbBoxes(ComboBox cmbBox, CmbBoxEnum cmbBoxType)
        {
            switch (cmbBoxType)
            {
                case CmbBoxEnum.Series:
                    mForm1.FillCmbBoxSeries(cmbBox);
                    mLabelCurrentSeries.Text = $"{cCurrent}{mToCorrect.Series}";
                    int seriesIndex = cmbBox.FindString(mToCorrect.Series);
                    cmbBox.SelectedIndex = seriesIndex;
                    break;
                case CmbBoxEnum.Format:
                    mForm1.FillCmbBoxFormat(cmbBox);
                    int formatIndex = cmbBox.FindString(mToCorrect.Format);
                    cmbBox.SelectedIndex = formatIndex;
                    break;
            } // switch
        }

        private string CalculateISBN(int isbnVersion, string otherISBN)
        {
            ISBNWorker isbnWorker = new ISBNWorker();
            string isbn = "";
            switch (isbnVersion)
            {
                case 10: isbn = isbnWorker.CalculateISBN10(otherISBN); break;
                case 13: isbn = isbnWorker.CalculateISBN13(otherISBN); break;
            }

            return isbn;
        }

        private bool CheckForChanges(out ChangedItemsStruct changedItemsStruct)
        {
            bool ok = false;
            changedItemsStruct = new ChangedItemsStruct();
            ISBNWorker.DBBookStruct dbBookStruct = new ISBNWorker.DBBookStruct();
            string msg = "";
            // Title
            if (mToCorrect.Title != mTxtBoxTitle.Text)
            {
                if (mTxtBoxTitle.Text == "")
                {
                    MessageBox.Show("Ein Titel muss angegeben werden", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return false;
                }

                dbBookStruct.Title = mTxtBoxTitle.Text;
                msg += $"\r\nTitle:\r\n\t{mToCorrect.Title}\r\n\t{mTxtBoxTitle.Text}";
            } // if
            // SubTitle
            if (mToCorrect.SubTitle != mTxtBoxSubTitle.Text)
            {
                dbBookStruct.SubTitle = mTxtBoxSubTitle.Text;
                string newText = mTxtBoxSubTitle.Text != "" ? mTxtBoxSubTitle.Text : "{ohne}";
                msg += $"\r\nUntertitel:\r\n\t{mToCorrect.SubTitle}\r\n\t{newText}";
            } // if
            // Series
            if (mToCorrect.Series != mCmbBoxSeries.Text)
            {
                changedItemsStruct.Series = mCmbBoxSeries.Text;
                string newText = mCmbBoxSeries.Text != "" ? mCmbBoxSeries.Text : "{ohne}";
                msg += $"\r\nSeries:\r\n\t{mToCorrect.Series}\r\n\t{newText}";
            } // if
            // NoInSeries
            if (mToCorrect.NoInSeries != mTxtBoxNoInSeries.Text && mTxtBoxNoInSeries.Enabled)
            {
                string newNumber = mTxtBoxNoInSeries.Text != "" ? mTxtBoxNoInSeries.Text : "0";
                dbBookStruct.NoInSeries = int.Parse(newNumber);
                msg += $"\r\nNummer in Serie:\r\n\t{mToCorrect.NoInSeries}\r\n\t{newNumber}";
            } // if
            // PublishingDate
            if (mToCorrect.PublishingDate != mTxtBoxPublished.Text)
            {
                dbBookStruct.PublishingDate = mTxtBoxPublished.Text;
                string newText = mTxtBoxPublished.Text != "" ? mTxtBoxPublished.Text : "{ohne}";
                msg += $"\r\nVeröffentlicht:\r\n\t{mToCorrect.PublishingDate}\r\n\t{newText}";
            } // if
            // Format
            if (mToCorrect.Format != mCmbBoxFormat.Text)
            {
                dbBookStruct.Format = mCmbBoxFormat.Text;
                string newText = mCmbBoxFormat.Text != "" ? mCmbBoxFormat.Text : "{ohne}";
                msg += $"\r\nFormat:\r\n\t{mToCorrect.Format}\r\n\t{newText}";
            } // if
            // ISBN10
            if (mToCorrect.ISBN10 != mTxtBoxISBN10.Text)
            {
                if (mTxtBoxISBN10.Text.Length != 10 && mTxtBoxISBN10.Text != "")
                {
                    MessageBox.Show("Die ISBN 10 muss genau 10 Zahlen besitzen.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return false;
                }

                dbBookStruct.ISBN10 = mTxtBoxISBN10.Text;
                string newText = mTxtBoxISBN10.Text != "" ? mTxtBoxISBN10.Text : "{ohne}";
                msg += $"\r\nISBN 10:\r\n\t{mToCorrect.ISBN10}\r\n\t{newText}";
            } // if
            // ISBN13
            if (mToCorrect.ISBN13 != mTxtBoxISBN13.Text)
            {
                if (mTxtBoxISBN13.Text.Length != 13 && mTxtBoxISBN13.Text != "")
                {
                    MessageBox.Show("Die ISBN 13 muss genau 13 Zahlen besitzen.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return false;
                }

                dbBookStruct.ISBN13 = mTxtBoxISBN13.Text;
                string newText = mTxtBoxISBN13.Text != "" ? mTxtBoxISBN13.Text : "{ohne}";
                msg += $"\r\nISBN 13:\r\n\t{mToCorrect.ISBN13}\r\n\t{newText}";
            } // if
            // Authors
            bool needsToBeChanged = HaveAuthorsChanged();
            List<AuthorStruct> changedAuthors = new List<AuthorStruct>();
            if (needsToBeChanged)
            {
                string dgvMsg = "";
                foreach (DataGridViewRow author in mDGVAuthors.Rows)
                {
                    string firstName = author.Cells[0].Value == null ? "" : author.Cells[0].Value.ToString();
                    string name = author.Cells[1].Value == null ? "" : author.Cells[1].Value.ToString();
                    if (firstName != "" & name != "")
                    {
                        bool alreadyInDB = (bool)author.Cells[2].Value;
                        bool toDelete = (bool)author.Cells[3].Value;
                        int authorID = int.Parse(author.Cells[4].Value.ToString());
                        if (alreadyInDB)
                        {
                            if (mAuthorsFromDB.Where(auth => auth.PreName == firstName && auth.Name == name).Count() < 1 && !toDelete)
                            {
                                dgvMsg += $"\r\n\t{author.Cells[0].Value} {author.Cells[1].Value} - ist in DB";
                                changedAuthors.Add(new AuthorStruct() { PreName = author.Cells[0].Value.ToString(), Name = author.Cells[1].Value.ToString(), AuthorID = authorID, ToDelete = toDelete, AlreadyInDB = alreadyInDB });
                            }
                            else if (authorID >= 0)
                            {
                                dgvMsg += $"\r\n\t{author.Cells[0].Value} {author.Cells[1].Value} - ist in DB";
                                dgvMsg += toDelete ? " - wird gelöscht" : "";
                                changedAuthors.Add(new AuthorStruct() { PreName = author.Cells[0].Value.ToString(), Name = author.Cells[1].Value.ToString(), AuthorID = authorID, ToDelete = toDelete, AlreadyInDB = alreadyInDB });
                            }
                        } // if
                        else if (!alreadyInDB && !toDelete)
                        {
                            dgvMsg += $"\r\n\t{author.Cells[0].Value} {author.Cells[1].Value} - ist nicht in DB";
                            changedAuthors.Add(new AuthorStruct() { PreName = author.Cells[0].Value.ToString(), Name = author.Cells[1].Value.ToString(), AuthorID = authorID, ToDelete = toDelete, AlreadyInDB = alreadyInDB });
                        }
                    } // if
                } // foreach
                if (dgvMsg != "")
                {
                    string oldAuthors = "";
                    foreach (ISBNWorker.DBAuthorStruct author in mAuthorsFromDB)
                    {
                        oldAuthors += $"\r\n\t{author.PreName} {author.Name}";
                    }

                    msg += $"\r\n\r\nAutor_innen-Liste alt:{oldAuthors}\r\nneu:{dgvMsg}";
                } // if
                else if (changedAuthors.Count != mAuthorsFromDB.Count)
                {
                    msg += "\r\n\r\nEs wurden alle Autor_innen entfernt";
                }
            } // if



            if (msg != "")
            {
                changedItemsStruct.Authors = changedAuthors;
                changedItemsStruct.Book = dbBookStruct;
                ok = DialogResult.Yes == MessageBox.Show($"Sollen folgende Änderungen wirklich vorgenommen werden?\r\n\r\nalt -> neu{msg}", "Frage", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }
            else
            {
                MessageBox.Show("Es wurden keine Änderungen vorgenommen", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return ok;
        }

        private bool HaveAuthorsChanged()
        {
            bool needsToBeChanged = false;
            foreach (DataGridViewRow author in mDGVAuthors.Rows)
            {
                string firstName = author.Cells[0].Value == null ? "" : author.Cells[0].Value.ToString();
                string name = author.Cells[1].Value == null ? "" : author.Cells[1].Value.ToString();
                var res = mAuthorsFromDB.Where(auth => auth.Name == name && auth.PreName == firstName);
                if (firstName != "" && name != "" && mAuthorsFromDB.Where(auth => auth.Name == name && auth.PreName == firstName).Count() == 0)
                {
                    needsToBeChanged = true;
                }

                if (firstName != "" && name != "" && (bool)author.Cells[3].Value)
                {
                    needsToBeChanged = true;
                }
            } // foreach

            return needsToBeChanged;
        }

        private string CreateUpdateCommand(ChangedItemsStruct changedItemsStruct)
        {
            string cmd = "";
            List<string> bookParameters = new List<string>();
            bool bookChanged = false;
            #region Book
            // Title
            if (changedItemsStruct.Book.Title != null)
            {
                bookChanged = true;
                bookParameters.Add($"Title='{changedItemsStruct.Book.Title}'");
            }
            // SubTitle
            if (changedItemsStruct.Book.SubTitle != null)
            {
                bookChanged = true;
                bookParameters.Add($"SubTitle='{changedItemsStruct.Book.SubTitle}'");
            }
            // PublishingDate
            if (changedItemsStruct.Book.PublishingDate != null)
            {
                bookChanged = true;
                bookParameters.Add($"PublishingDate='{changedItemsStruct.Book.PublishingDate}'");
            }
            // Format
            if (changedItemsStruct.Book.Format != null)
            {
                bookChanged = true;
                bookParameters.Add($"Format='{changedItemsStruct.Book.Format}'");
            }
            // ISBN10
            if (changedItemsStruct.Book.ISBN10 != null)
            {
                bookChanged = true;
                bookParameters.Add($"ISBN10='{changedItemsStruct.Book.ISBN10}'");
            }
            // ISBN13
            if (changedItemsStruct.Book.ISBN13 != null)
            {
                bookChanged = true;
                bookParameters.Add($"ISBN13='{changedItemsStruct.Book.ISBN13}'");
            }

            if (bookChanged)
            {
                string bookCmd = $"UPDATE Books SET {String.Join(", ", bookParameters)} WHERE BookID={mToCorrect.BookID};";
                cmd += bookCmd;
            }
            #endregion

            // Authors
            bool authorsChanged = false;
            List<string> changedAuthorsParameters = new List<string>();
            List<string> deletedAuthorsParameters = new List<string>();
            List<string> newAuthorsParameters = new List<string>();
            List<string> newAuthorsAlreadyInDBParameters = new List<string>();
            foreach (AuthorStruct author in changedItemsStruct.Authors)
            {
                // Authors deleted
                if (author.ToDelete)
                {
                    if (author.AlreadyInDB && author.AuthorID >= 0)
                    {
                        authorsChanged = true;
                        deletedAuthorsParameters.Add(author.AuthorID.ToString());
                    }
                } // if
                // Authors added (not in DB)
                else if (!author.AlreadyInDB)
                {
                    authorsChanged = true;
                    newAuthorsParameters.Add($"{author.PreName}, {author.Name}");
                }
                // Authors added (in DB)
                else if (author.AuthorID == -1)
                {
                    authorsChanged = true;
                    newAuthorsAlreadyInDBParameters.Add($"Prename='{author.PreName}' AND Name='{author.Name}'");
                }
                // Authors changed
                else if (mAuthorsFromDB.Where(auth => auth.AuthorID == author.AuthorID && (auth.Name != author.Name || auth.PreName != author.PreName)).Count() > 0)
                {
                    authorsChanged = true;
                    changedAuthorsParameters.Add($"UPDATE Authors SET Prename='{author.PreName}', Name='{author.Name}' WHERE AuthorID={author.AuthorID};");
                }
            } // foreach

            if (authorsChanged)
            {
                string authorsChangedCmd = String.Join("; ", changedAuthorsParameters);
                string newAuthorsCmd = "";
                for (int index = 0; index < newAuthorsParameters.Count; index++)
                {
                    newAuthorsCmd += $"DO $$ DECLARE newAuthorID integer; BEGIN INSERT INTO Authors (Prename, Name) VALUES({newAuthorsParameters[index]}) RETURNING AuthorID INTO NewAuthorID; INSERT INTO BookAuthor(BookID, AuthorID) VALUES ({mToCorrect.BookID}, NewAuthorID); END $$;";
                }

                string newAuthorAlreadyInDBCmd = "";
                for (int index = 0; index < newAuthorsAlreadyInDBParameters.Count; index++)
                {
                    newAuthorAlreadyInDBCmd += $"ExistingAuthorID := SELECT AuthorID FROM Authors WHERE {newAuthorsAlreadyInDBParameters[index]} LIMIT 1; INSERT INTO BookAuthor (BookID, AuthorID) VALUES ({mToCorrect.BookID}, ExistingAuthorID);";
                }

                string deletedAuthorsCmd = $"UPDATE Authors SET Deleted = true WHERE AuthorID IN({String.Join(", ", deletedAuthorsParameters)});";

                cmd += $"{authorsChangedCmd} {newAuthorsCmd} {newAuthorAlreadyInDBCmd} {deletedAuthorsCmd}";
            }

            // Series
            // NoInSeries





            return cmd;
        }

        #endregion
        #region Events

        internal void BtnCalculateISBN10_OnClick()
        {
            string isbn13 = mTxtBoxISBN13.Text;
            mTxtBoxISBN10.Text = CalculateISBN(10, isbn13);
        }

        internal void BtnCalculateISBN13_OnCLick()
        {
            string isbn10 = mTxtBoxISBN10.Text;
            mTxtBoxISBN13.Text = CalculateISBN(13, isbn10);
        }

        internal void BtnStartCorrection_OnClick(object sender, EventArgs e)
        {
            ChangedItemsStruct changedItemsStruct;
            bool ok = CheckForChanges(out changedItemsStruct);
            if (ok)
            {
                string cmd = CreateUpdateCommand(changedItemsStruct);
                // WriteCorrectionsToDB()
                // UPDATE-Command bauen
                // Via DBWriter.WriteToDB() feuern
                // MessageBox.Show(Erfolg");
                // Close();
            }
        }

        internal void TxtBoxISBN10_TextChanged()
        {
            mWriteTab.CheckAndEnableBtnsCalculateISBN(mTxtBoxISBN13, mTxtBoxISBN10, mBtnCalculateISBN13, mBtnCalculateISBN10);
        }

        internal void TxtBoxISBN13_TextChanged()
        {
            mWriteTab.CheckAndEnableBtnsCalculateISBN(mTxtBoxISBN13, mTxtBoxISBN10, mBtnCalculateISBN13, mBtnCalculateISBN10);
        }

        internal void CmbBoxSeries_SelectedIndexChanged()
        {
            if (mCmbBoxSeries.Text == "")
            {
                mTxtBoxNoInSeries.Enabled = false;
            }
            else
            {
                mTxtBoxNoInSeries.Enabled = true;
            }
        }

        internal void DGVAuthors_RowAdded(int rowIndex)
        {
            DataGridViewRow dgvRow = mDGVAuthors.Rows[rowIndex];
            if (dgvRow.Cells.Count == 5)
            {
                dgvRow.Cells[2] = new DataGridViewCheckBoxCell();
                dgvRow.Cells[2].Value = false;
                dgvRow.Cells[3] = new DataGridViewCheckBoxCell();
                dgvRow.Cells[3].Value = false;
                dgvRow.Cells[4].Value = "-1";
            } // if
        }

        #endregion
    }
}
