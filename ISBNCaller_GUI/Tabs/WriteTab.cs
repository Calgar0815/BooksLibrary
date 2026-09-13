using ISBNCaller.DBWorker;
using ISBNCaller_Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ISBNCaller_GUI
{
    internal class WriteTab
    {
        #region Variables

        //internal bool mWriteSearch = true;
        internal TextBox mTxtBoxISBN_1 { get; set; }
        internal Button mBtnOK { get; set; }
        internal Button mBtnWriteToDB { get; set; }
        internal Button mBtnCancel { get; set; }
        internal DataGridView mDataGridViewAuthor { get; set; }
        internal ComboBox mCmbBoxFormat { get; set; }
        internal TextBox mTxtBoxISBN10 { get; set; }
        internal TextBox mTxtBoxISBN13 { get; set; }
        internal TextBox mTxtBoxPublishingDate { get; set; }
        internal TextBox mTxtBoxSubTitle { get; set; }
        internal TextBox mTxtBoxTitle { get; set; }
        internal CheckBox mChkBoxIsPartOfSeries { get; set; }
        internal TextBox mTxtBoxNoInSeries { get; set; }
        internal ComboBox mCmbBoxSeries { get; set; }
        internal CheckBox mChkBoxIsNewSeries { get; set; }
        internal TextBox mTxtBoxNewSeriesName { get; set; }
        internal Button mBtnCalculateISBN10 { get; set; }
        internal Button mBtnCalculateISBN13 { get; set; }
        internal Button mBtnRegisterWOutISBN { get; set; }
        internal Label mLabelMaxNoCount { get; set; }
        internal Label mWorkInProgressLabel { get; set; }
        private Form1 mForm1 { get; set; }
        private bool mChangeCmbFormat { get; set; }

        #endregion
        #region Constructor

        public WriteTab(Form1 form1)
        {
            mForm1 = form1;
            mChangeCmbFormat = true;
        }

        #endregion
        #region Methods
        #region Events

        internal void btnOK_Click()
        {
            string isbn = mTxtBoxISBN_1.Text;
            isbn = isbn.Replace("-", "");
            if (isbn.Length != 10 && isbn.Length != 13)
            {
                MessageBox.Show("Die ISBN muss 10 oder 13 Stellen lang sein.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            mWorkInProgressLabel.Visible = true;
            bool ok = WriteTab_ShowResult();
            mWorkInProgressLabel.Visible = false;
            if (!ok) return;

            // abfragen
            mBtnCancel.Enabled = true;
            mBtnWriteToDB.Enabled = true;
            mBtnOK.Enabled = false;
        }

        internal void btnWriteToDB_Click()
        {
            mChangeCmbFormat = true;
            // in die DB schreiben
            bool written = WriteToDB();
            if (written)
            {
                MessageBox.Show("Erfolgreich gespeichert");
                WriteTab_SetBack();
            }
            else
            {
                MessageBox.Show("Daten konnten nicht geschrieben werden");
            }
        }

        internal void btnCancel_Click()
        {
            WriteTab_SetBack();
            mTxtBoxISBN_1.Focus();
            mTxtBoxISBN_1.SelectAll();
            mChangeCmbFormat = true;
        }

        internal void txtBoxISBN_1_TextChanged()
        {
            if (mBtnWriteToDB.Enabled)
            {
                WriteTab_SetBack();
            }
        }

        internal void dataGridViewAuthor_RowAdded(int rowIndex)
        {
            DataGridViewRow dgvRow = mDataGridViewAuthor.Rows[rowIndex];
            if (dgvRow.Cells.Count == 3)
            {
                dgvRow.Cells[2] = new DataGridViewCheckBoxCell();
                dgvRow.Cells[2].Value = false;
            }
        }

        internal void chkBoxIsPartOfSeries_CheckedChanged(CheckBox chkBox)
        {
            if (chkBox.Checked)
            {
                mCmbBoxSeries.Enabled = true;
                mChkBoxIsNewSeries.Enabled = true;
                mTxtBoxNoInSeries.Enabled = true;
                mForm1.FillCmbBoxSeries(mCmbBoxSeries);
            } // if
            else
            {
                mCmbBoxSeries.Enabled = false;
                mChkBoxIsNewSeries.Checked = false;
                mChkBoxIsNewSeries.Enabled = false;
                mTxtBoxNoInSeries.Enabled = false;
                mTxtBoxNoInSeries.Text = "";
                mCmbBoxSeries.DataSource = null;
                mCmbBoxSeries.Items.Clear();
            } // else
        }

        internal void chkBoxIsNewSeries_CheckedChanged(CheckBox chkBox)
        {
            if (chkBox.Checked)
            {
                mTxtBoxNewSeriesName.Enabled = true;
                mTxtBoxNewSeriesName.Focus();
            }
            else
            {
                mTxtBoxNewSeriesName.Enabled = false;
                mTxtBoxNewSeriesName.Text = "";
            }
        }

        internal void cmbBoxSeries_SelectedIndexChanged()
        {
            if (mCmbBoxSeries.SelectedValue != null)
            {
                int seriesID = (int)mCmbBoxSeries.SelectedValue;
                DBReader dbReader = new DBReader(mForm1.mDBConnection, mForm1.mBooksDB);
                int maxNoInSeries = dbReader.GetMaxNoInSeries(seriesID);
                mLabelMaxNoCount.Text = $"{maxNoInSeries}";
            } // if
        }

        internal void btnCalculateISBN10_Click()
        {
            string isbn13 = mTxtBoxISBN13.Text;
            isbn13 = isbn13.Replace("-", "");
            ISBNWorker isbnWorker = new ISBNWorker();
            string isbn10 = isbnWorker.CalculateISBN10(isbn13);
            mTxtBoxISBN10.Text = isbn10;
        }

        internal void btnCalculateISBN13_Click()
        {
            string isbn10 = mTxtBoxISBN10.Text;
            isbn10 = isbn10.Replace("-", "");
            ISBNWorker isbnWorker = new ISBNWorker();
            string isbn13 = isbnWorker.CalculateISBN13(isbn10);
            mTxtBoxISBN13.Text = isbn13;
        }

        internal void btnRegisterWOutISBN_Click()
        {
            mChangeCmbFormat = true;
            mChkBoxIsPartOfSeries.Enabled = true;
            ChangeTxtBoxesStates(true);
            if (mDataGridViewAuthor.Rows.Count == 0)
            {
                mDataGridViewAuthor.ColumnCount = 3;
                mDataGridViewAuthor.Columns[0].Name = "Vorname";
                mDataGridViewAuthor.Columns[1].Name = "Name";
                mDataGridViewAuthor.Columns[2].Name = "In der DB";
                mDataGridViewAuthor.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                mDataGridViewAuthor.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            } // if

            // abfragen
            mBtnOK.Enabled = false;
            mBtnWriteToDB.Enabled = true;
            mBtnCancel.Enabled = true;
            mBtnRegisterWOutISBN.Enabled = false;
        }

        internal void cmbBoxFormat_EnabledChanged()
        {
            if (mChangeCmbFormat)
            {
                mCmbBoxFormat.DataSource = null;
                mCmbBoxFormat.Items.Clear();
                mForm1.FillCmbBoxFormat(mCmbBoxFormat);
            } // if
        }

        #endregion

        private void WriteTab_SetBack()
        {
            mBtnOK.Enabled = true;
            mBtnWriteToDB.Enabled = false;
            mBtnCancel.Enabled = false;
            mDataGridViewAuthor.RowCount = 1;
            mCmbBoxFormat.ResetText();
            mCmbBoxFormat.Enabled = false;
            mTxtBoxISBN10.Text = "";
            mTxtBoxISBN13.Text = "";
            mTxtBoxPublishingDate.Text = "";
            mTxtBoxSubTitle.Text = "";
            mTxtBoxTitle.Text = "";
            mChkBoxIsPartOfSeries.Checked = false;
            mChkBoxIsPartOfSeries.Enabled = false;
            mTxtBoxNoInSeries.Text = "";
            mTxtBoxNoInSeries.Enabled = false;
            mCmbBoxSeries.ResetText();
            mCmbBoxSeries.Enabled = false;
            mChkBoxIsNewSeries.Checked = false;
            mChkBoxIsNewSeries.Enabled = false;
            mTxtBoxNewSeriesName.Text = "";
            mTxtBoxNewSeriesName.Enabled = false;
            mBtnCalculateISBN10.Enabled = false;
            mBtnCalculateISBN13.Enabled = false;
            ChangeTxtBoxesStates(false);
            mBtnRegisterWOutISBN.Enabled = true;
        }

        internal void InitializeDGV()
        {
            mDataGridViewAuthor.ColumnCount = 3;
            mDataGridViewAuthor.Columns[0].Name = "Vorname";
            mDataGridViewAuthor.Columns[1].Name = "Name";
            mDataGridViewAuthor.Columns[2].Name = "In der DB";
            mDataGridViewAuthor.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            mDataGridViewAuthor.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        private bool WriteTab_ShowResult()
        {
            /// Daten holen
            ISBNWorker isbnWorker = new ISBNWorker();
            string isbn = mTxtBoxISBN_1.Text;
            ISBNWorker.BookStruct book = new ISBNWorker.BookStruct();
            try
            {
                book = isbnWorker.AskForISBN(isbn);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            foreach (string autor in book.Autoren)
            {
                string preName = autor;
                string name = "";
                string[] splitString = autor.Split(' ');
                name = splitString[splitString.Length - 1];
                preName = preName.Replace($@" {name}", "");
                DataGridViewRow dgvRow = new DataGridViewRow();
                DataGridViewCell dgvCellPreName = new DataGridViewTextBoxCell();
                dgvCellPreName.Value = preName;
                dgvRow.Cells.Add(dgvCellPreName);
                DataGridViewCell dgvCellName = new DataGridViewTextBoxCell();
                dgvCellName.Value = name;
                dgvRow.Cells.Add(dgvCellName);
                DataGridViewCell dgvCellKnownInDB = new DataGridViewCheckBoxCell();
                dgvCellKnownInDB.Value = false;
                dgvRow.Cells.Add(dgvCellKnownInDB);
                mDataGridViewAuthor.Rows.Add(dgvRow);
            } // foreach

            mTxtBoxISBN10.Text = book.ISBN10;
            mTxtBoxISBN13.Text = book.ISBN13;
            mTxtBoxPublishingDate.Text = book.PublishingDate;
            mTxtBoxSubTitle.Text = book.Untertitel;
            mTxtBoxTitle.Text = book.Titel;
            if (book.SerialNo != "")
            {
                mTxtBoxNoInSeries.Text = book.SerialNo;
                mChkBoxIsPartOfSeries.Checked = true;
            }

            if (book.Format != "")
            {
                int index = mCmbBoxFormat.FindString(book.Format);
                if (index > -1)
                {
                    mCmbBoxFormat.SelectedIndex = index;
                    mChangeCmbFormat = false;
                }
            } // if

            if (book.SeriesName != "")
            {
                int index = mCmbBoxSeries.FindString(book.SeriesName);
                if (index > -1)
                {
                    mCmbBoxSeries.SelectedIndex = index;
                }
            } // if

            mChkBoxIsPartOfSeries.Enabled = true;
            ChangeTxtBoxesStates(true);

            return true;
        }

        private void ChangeTxtBoxesStates(bool enabled)
        {
            mDataGridViewAuthor.Enabled = enabled;
            mCmbBoxFormat.Enabled = enabled;
            mTxtBoxISBN10.Enabled = enabled;
            mTxtBoxISBN13.Enabled = enabled;
            mTxtBoxPublishingDate.Enabled = enabled;
            mTxtBoxSubTitle.Enabled = enabled;
            mTxtBoxTitle.Enabled = enabled;
            mChkBoxIsPartOfSeries.Enabled = enabled;
        }

        internal void DisableTxtBoxes()
        {
            mTxtBoxISBN_1.Text = "";
            mTxtBoxNoInSeries.Enabled = false;
            mTxtBoxNewSeriesName.Enabled = false;
            mCmbBoxSeries.Enabled = false;
            mChkBoxIsNewSeries.Enabled = false;
            mBtnCalculateISBN10.Enabled = false;
            mBtnCalculateISBN13.Enabled = false;
            ChangeTxtBoxesStates(false);
        }

        internal void CheckAndEnableTxtBoxesCalculateISBN()
        {
            string txtBoxISBN10Text = mTxtBoxISBN10.Text;
            txtBoxISBN10Text = txtBoxISBN10Text.Replace("-", "");
            string txtBoxISBN13Text = mTxtBoxISBN13.Text;
            txtBoxISBN13Text = txtBoxISBN13Text.Replace("-", "");
            if (txtBoxISBN10Text.Length == 10 && txtBoxISBN13Text.Length != 13)
            {
                mBtnCalculateISBN13.Enabled = true;
            }
            else
            {
                mBtnCalculateISBN13.Enabled = false;
            }

            if (txtBoxISBN13Text.Length == 13 && txtBoxISBN10Text.Length != 10)
            {
                mBtnCalculateISBN10.Enabled = true;
            }
            else
            {
                mBtnCalculateISBN10.Enabled = false;
            }
        }

        #region Write to DB

        private bool WriteToDB()
        {
            DBReader dbReader = new DBReader(mForm1.mDBConnection, mForm1.mBooksDB);
            List<ISBNWorker.DBAuthorStruct> returnedAuthors = new List<ISBNWorker.DBAuthorStruct>();
            if (!CheckAuthorsForDublettes(dbReader, ref returnedAuthors))
            {
                return false;
            }

            ISBNWorker.DBBookStruct dbBook = new ISBNWorker.DBBookStruct();
            bool ok = PrepareWriteBookToDB(dbReader, ref dbBook);

            if (ok)
            {
                DBWriter dbWriter = new DBWriter(mForm1.mDBConnection);
                bool written = dbWriter.WriteBook(dbBook);
                if (!written) return false;

                List<ISBNWorker.DBBookStruct> books = dbReader.GetBooksByTitle(dbBook.Title);
                int bookID = books[books.Count - 1].BookID;
                int authorID = -1;
                if (written)
                {
                    written = WriteAuthorsToDB(dbWriter, dbReader, ref returnedAuthors, ref authorID);
                }

                if (written)
                {
                    written = WriteBookAuthorToDB(dbWriter, returnedAuthors, bookID, authorID);
                }

                if (written)
                {
                    written = WriteBookSeriesToDB(dbWriter, dbReader, dbBook, bookID);
                }

                ok = written;
            } // if

            return ok;
        }

        private bool WriteAuthorsToDB(DBWriter dbWriter, DBReader dbReader, ref List<ISBNWorker.DBAuthorStruct> returnedAuthors, ref int authorID)
        {
            if (mDataGridViewAuthor.Rows.Count == 2)
            {
                ISBNWorker.DBAuthorStruct authorStruct = new ISBNWorker.DBAuthorStruct();
                DataGridViewRow dgvRow = mDataGridViewAuthor.Rows[0];
                authorStruct.Name = dgvRow.Cells[1].Value.ToString();
                authorStruct.PreName = dgvRow.Cells[0].Value == null ? null : dgvRow.Cells[0].Value.ToString();
                if (!(bool)dgvRow.Cells[2].Value)
                {
                    bool written = dbWriter.WriteAuthor(authorStruct);
                    if (!written) return false;
                } // if

                ISBNWorker.DBAuthorStruct returnedAuthor = authorStruct.PreName == null ? dbReader.GetAuthorByName(authorStruct.Name) : dbReader.GetAuthor(authorStruct.PreName, authorStruct.Name);
                authorID = returnedAuthor.AuthorID;
            } // if
            else if (mDataGridViewAuthor.Rows.Count > 2)
            {
                List<ISBNWorker.DBAuthorStruct> authors = new List<ISBNWorker.DBAuthorStruct>();
                List<KeyValuePair<string, string>> authorNames = new List<KeyValuePair<string, string>>();
                for (int index = 0; index < mDataGridViewAuthor.Rows.Count - 1; index++)
                {
                    ISBNWorker.DBAuthorStruct authorStruct = new ISBNWorker.DBAuthorStruct();
                    DataGridViewRow dgvRow = mDataGridViewAuthor.Rows[index];
                    if (!(bool)dgvRow.Cells[2].Value)
                    {
                        authorStruct.Name = dgvRow.Cells[1].Value.ToString();
                        authorStruct.PreName = dgvRow.Cells[0].Value == null ? null : dgvRow.Cells[0].Value.ToString();
                        authors.Add(authorStruct);
                        if (authorStruct.PreName == null)
                        {
                            authorNames.Add(new KeyValuePair<string, string>("", authorStruct.Name));
                        }
                        else
                        {
                            authorNames.Add(new KeyValuePair<string, string>(authorStruct.PreName, authorStruct.Name));
                        }
                    } // if
                } // for

                if (authors.Count > 0)
                {
                    bool written = dbWriter.WriteAuthors(authors);
                    if (!written) return false;

                    returnedAuthors.AddRange(dbReader.GetAuthors(authorNames));
                } // if
            } // else if

            return true;
        }

        private bool WriteBookSeriesToDB(DBWriter dbWriter, DBReader dbReader, ISBNWorker.DBBookStruct dbBook, int bookID)
        {
            bool written = false;
            if (dbBook.IsPartOfSeries)
            {
                int seriesID = -1;
                if (mChkBoxIsNewSeries.Checked)
                {
                    dbWriter.WriteSeries(mTxtBoxNewSeriesName.Text);
                    seriesID = dbReader.GetSeriesIDBySeriesName(mTxtBoxNewSeriesName.Text);
                }
                else
                {
                    seriesID = (int)mCmbBoxSeries.SelectedValue;
                }

                if (dbBook.NoInSeries == -1)
                {
                    written = dbWriter.WriteBookSeries(bookID, seriesID);
                }
                else
                {
                    written = dbWriter.WriteBookSeries(bookID, seriesID, dbBook.NoInSeries);
                }
            } // if
            else
            {
                written = true;
            }

            return written;
        }

        private bool WriteBookAuthorToDB(DBWriter dbWriter, List<ISBNWorker.DBAuthorStruct> returnedAuthors, int bookID, int authorID)
        {
            bool written = false;
            if (authorID > -1)
            {
                written = dbWriter.WriteBookAuthor(bookID, authorID);
            }
            else if (returnedAuthors.Count > 0)
            {
                List<int> authorIDs = new List<int>();
                foreach (ISBNWorker.DBAuthorStruct author in returnedAuthors)
                {
                    authorIDs.Add(author.AuthorID);
                }

                written = dbWriter.WriteBookAuthors(bookID, authorIDs);
            } // else if
            else
            {
                written = true;
            }

            return written;
        }

        private bool PrepareWriteBookToDB(DBReader dbReader, ref ISBNWorker.DBBookStruct dbBook)
        {
            bool ok = true;
            dbBook.IsPartOfSeries = mChkBoxIsPartOfSeries.Checked;
            dbBook.NoInSeries = dbBook.IsPartOfSeries ? (mTxtBoxNoInSeries.Text != "" ? int.Parse(mTxtBoxNoInSeries.Text) : -1) : -1;
            dbBook.Format = mCmbBoxFormat.Text == "" ? null : mCmbBoxFormat.Text;
            string isbn10 = mTxtBoxISBN10.Text;
            dbBook.ISBN10 = isbn10 == "" || isbn10 == "unbekannt" ? null : isbn10.Replace("-", "");
            string isbn13 = mTxtBoxISBN13.Text;
            dbBook.ISBN13 = isbn13 == "" || isbn13 == "unbekannt" ? null : isbn13.Replace("-", "");
            dbBook.PublishingDate = mTxtBoxPublishingDate.Text == "" ? null : mTxtBoxPublishingDate.Text;
            dbBook.SubTitle = mTxtBoxSubTitle.Text == "" ? null : EscapeTitle(mTxtBoxSubTitle.Text);
            dbBook.Title = EscapeTitle(mTxtBoxTitle.Text);

            if (ok && dbBook.ISBN10 != null)
            {
                if (dbBook.ISBN10.Length != 10)
                {
                    MessageBox.Show($"Die ISBN 10 ist nicht 10 Zeichen lang.");

                    return false;
                } // if

                bool dublette = CheckForBookDublettes(dbBook.ISBN10, dbReader);
                if (dublette)
                {
                    ok = false;
                    MessageBox.Show($"Es gibt bereits einen Eintrag in der Buchtabelle mit der ISBN {dbBook.ISBN10}.");
                }
            } // if

            if (ok && dbBook.ISBN13 != null)
            {
                if (dbBook.ISBN13.Length != 13)
                {
                    MessageBox.Show($"Die ISBN 13 ist nicht 13 Zeichen lang.");

                    return false;
                } // if

                bool dublette = CheckForBookDublettes(dbBook.ISBN13, dbReader);
                if (dublette)
                {
                    ok = false;
                    MessageBox.Show($"Es gibt bereits einen Eintrag in der Buchtabelle mit der ISBN {dbBook.ISBN13}.");
                }
            } // if

            return ok;
        }

        private string EscapeTitle(string text)
        {
            if (text.Contains("'"))
            {
                text = text.Replace("'", "''");
            }

            if (text.Contains("\\"))
            {
                text = text.Replace("\\", "\\\\");
            }

            return text;
        }

        #region Dublettes Checks
        private bool CheckAuthorsForDublettes(DBReader dbReader, ref List<ISBNWorker.DBAuthorStruct> returnedAuthors)
        {
            for (int index = 0; index < mDataGridViewAuthor.Rows.Count - 1; index++)
            {
                DataGridViewRow dgvRow = mDataGridViewAuthor.Rows[index];
                DataGridViewCell dgvCellName = dgvRow.Cells[1];
                var val = dgvRow.Cells[0].Value;
                string preName = val == null ? "" : val.ToString();
                string name = dgvRow.Cells[1].Value.ToString();
                bool needToCheckDublette = !(bool)dgvRow.Cells[2].Value;
                if (needToCheckDublette)
                {
                    bool dublette = CheckForAuthorDublettes(preName, name, dbReader);
                    if (dublette)
                    {
                        MessageBox.Show($"Es gibt bereits einen Eintrag in der Autorentabelle mit den Werten Vorname={preName} und Name={name}. Bitte Eintrag der DGV anpassen.");
                        return false;
                    }
                } // if
                else
                {
                    List<KeyValuePair<string, string>> authorNames = new List<KeyValuePair<string, string>>();
                    authorNames.Add(new KeyValuePair<string, string>(preName, name));
                    List<ISBNWorker.DBAuthorStruct> authors = dbReader.GetAuthors(authorNames);
                    if (authorNames.Count != authors.Count)
                    {
                        MessageBox.Show($"Es gibt noch nicht alle angegebenen Autor_innen in der DB. Bitte DGV anpassen.");
                        return false;
                    } // if
                    else
                    {
                        returnedAuthors.AddRange(authors);
                    }
                } // else
            } // foreach

            return true;
        }

        private bool CheckForAuthorDublettes(string preName, string name, DBReader dbReader)
        {
            ISBNWorker.DBAuthorStruct authorStruct = dbReader.GetAuthor(preName, name);
            if (authorStruct.AuthorID == 0)
            {
                return false;
            }

            return true;
        }

        private bool CheckForBookDublettes(string isbn, DBReader dbReader)
        {
            ISBNWorker.DBBookStruct bookStruct = dbReader.GetBookByISBN(isbn);
            if (bookStruct.BookID == 0)
            {
                return false;
            }

            return true;
        }

        #endregion
        #endregion
        #endregion
    }
}
