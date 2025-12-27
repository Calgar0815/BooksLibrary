using ISBNCaller_Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

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

        #endregion
        #region Structs && Enums

        private struct ChangedItemsStruct
        {
            public ISBNWorker.DBBookStruct Book;
            public List<ISBNWorker.DBAuthorStruct> Author;
            public string Series;
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
            mDGVAuthors.ColumnCount = 2;
            mDGVAuthors.Columns[0].Name = cFirstName;
            mDGVAuthors.Columns[1].Name = cName;
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
            if (mToCorrect.Title != mTxtBoxTitle.Text)
            {
                if(mTxtBoxTitle.Text == "")
                {
                    MessageBox.Show("Ein Titel muss angegeben werden", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                    return false;
                }

                dbBookStruct.Title = mTxtBoxTitle.Text;
                msg += $"\r\nTitle:\r\n\t{mToCorrect.Title}\r\n\t{mTxtBoxTitle.Text}";
            } // if
            if (mToCorrect.SubTitle != mTxtBoxSubTitle.Text)
            {
                dbBookStruct.SubTitle = mTxtBoxSubTitle.Text;
                string newText = mTxtBoxSubTitle.Text != "" ? mTxtBoxSubTitle.Text : "{ohne}";
                msg += $"\r\nUntertitel:\r\n\t{mToCorrect.SubTitle}\r\n\t{newText}";
            } // if
            if (mToCorrect.Series != mCmbBoxSeries.Text)
            {
                changedItemsStruct.Series = mCmbBoxSeries.Text;
                string newText = mCmbBoxSeries.Text != "" ? mCmbBoxSeries.Text : "{ohne}";
                msg += $"\r\nSeries:\r\n\t{mToCorrect.Series}\r\n\t{newText}";
            } // if
            if (mToCorrect.NoInSeries != mTxtBoxNoInSeries.Text && mTxtBoxNoInSeries.Enabled)
            {
                string newNumber = mTxtBoxNoInSeries.Text != "" ? mTxtBoxNoInSeries.Text : "0";
                dbBookStruct.NoInSeries = int.Parse(newNumber);
                msg += $"\r\nNummer in Serie:\r\n\t{mToCorrect.NoInSeries}\r\n\t{newNumber}";
            } // if
            if (mToCorrect.PublishingDate != mTxtBoxPublished.Text)
            {
                dbBookStruct.PublishingDate = mTxtBoxPublished.Text;
                string newText = mTxtBoxPublished.Text != "" ? mTxtBoxPublished.Text : "{ohne}";
                msg += $"\r\nVeröffentlicht:\r\n\t{mToCorrect.PublishingDate}\r\n\t{newText}";
            } // if
            if (mToCorrect.Format != mCmbBoxFormat.Text)
            {
                dbBookStruct.Format = mCmbBoxFormat.Text;
                string newText = mCmbBoxFormat.Text != "" ? mCmbBoxFormat.Text : "{ohne}";
                msg += $"\r\nFormat:\r\n\t{mToCorrect.Format}\r\n\t{newText}";
            } // if
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
            List<ISBNWorker.DBAuthorStruct> changedAuthors = new List<ISBNWorker.DBAuthorStruct>();
            string dgvMsg = "";
            foreach (DataGridViewRow author in mDGVAuthors.Rows)
            {
                string firstName = author.Cells[0].Value == null ? "" : author.Cells[0].Value.ToString();
                string name = author.Cells[1].Value == null ? "" : author.Cells[1].Value.ToString();
                if (firstName != "" & name != "")
                {
                    if (mAuthorsFromDB.Where(auth => auth.PreName == firstName && auth.Name == name).Count() < 1)
                    {
                        dgvMsg += $"\r\n\t{author.Cells[0].Value} {author.Cells[1].Value}";
                    }

                    changedAuthors.Add(new ISBNWorker.DBAuthorStruct() { PreName = author.Cells[0].Value.ToString(), Name = author.Cells[1].Value.ToString() });
                } // if
            } // foreach
            if (dgvMsg != "")
            {
                string oldAuthors = "";
                foreach(ISBNWorker.DBAuthorStruct author in mAuthorsFromDB)
                {
                    oldAuthors += $"\r\n\t{author.PreName} {author.Name}";
                }

                msg += $"\r\n\r\nAutor_innen alt:{oldAuthors}\r\nneu:{dgvMsg}";
            } // if
            else if (changedAuthors.Count != mAuthorsFromDB.Count)
            {
                msg += "\r\n\r\nEs wurden alle Autor_innen entfernt";
            }

            changedItemsStruct.Author = changedAuthors;
            changedItemsStruct.Book = dbBookStruct;
            if (msg != "")
            {
                ok = DialogResult.Yes == MessageBox.Show($"Sollen folgende Änderungen wirklich vorgenommen werden?\r\n\r\nalt -> neu{msg}", "Frage", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }
            else
            {
                MessageBox.Show("Es wurden keine Änderungen vorgenommen", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return ok;
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
                // WriteCorrectionsToDB()
                // UPDATE-Command bauen
                // Via DBWriter.WriteToDB() feuern
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

        #endregion
    }
}
