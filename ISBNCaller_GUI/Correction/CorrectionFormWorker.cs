using ISBNCaller_Lib;
using System;
using System.Collections.Generic;
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
        private WriteTab mWriteTab { get; set; }
        private const string cCurrent = "Bisher: ";
        private const string cFirstName = "Vorname";
        private const string cName = "Nachname";

        #endregion
        #region Structs && Enums

        private enum mCmbBoxEnum
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
            mTxtBoxTitle.Text = toCorrect.Title;
            mTxtBoxSubTitle.Text = toCorrect.SubTitle;
            mTxtBoxNoInSeries.Text = toCorrect.NoInSeries;
            mTxtBoxPublished.Text = toCorrect.PublishingDate;
            mTxtBoxISBN13.Text = toCorrect.ISBN13;
            mTxtBoxISBN10.Text = toCorrect.ISBN10;
            FillCmbBoxes(toCorrect, mCmbBoxSeries, mCmbBoxEnum.Series);
            FillCmbBoxes(toCorrect, mCmbBoxFormat, mCmbBoxEnum.Format);
            FillAuthorsDGV(toCorrect);
        }

        private void FillAuthorsDGV(DBReader.SearchStruct toCorrect)
        {
            mDGVAuthors.ColumnCount = 2;
            mDGVAuthors.Columns[0].Name = cFirstName;
            mDGVAuthors.Columns[1].Name = cName;
            mDGVAuthors.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            mDGVAuthors.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            DBReader dbReader = new DBReader();
            List<ISBNWorker.DBAuthorStruct> authorsList = dbReader.GetAuthorsByBookID(toCorrect.BookID);
            for (int index = 0; index < authorsList.Count; index++)
            {
                DataGridViewRow dgvRow = new DataGridViewRow();
                DataGridViewCell dgvCell = new DataGridViewTextBoxCell();
                dgvCell.Value = authorsList[index].PreName;
                dgvRow.Cells.Add(dgvCell);
                DataGridViewCell dgvCell2 = new DataGridViewTextBoxCell();
                dgvCell2.Value = authorsList[index].Name;
                dgvRow.Cells.Add(dgvCell2);
                mDGVAuthors.Rows.Add(dgvRow);
            } // for
        }

        private void FillCmbBoxes(DBReader.SearchStruct toCorrect, ComboBox cmbBox, mCmbBoxEnum cmbBoxType)
        {
            switch (cmbBoxType)
            {
                case mCmbBoxEnum.Series:
                    mForm1.FillCmbBoxSeries(cmbBox);
                    mLabelCurrentSeries.Text = $"{cCurrent}{toCorrect.Series}";
                    int seriesIndex = cmbBox.FindString(toCorrect.Series);
                    cmbBox.SelectedIndex = seriesIndex;
                    break;
                case mCmbBoxEnum.Format:
                    mForm1.FillCmbBoxFormat(cmbBox);
                    int formatIndex = cmbBox.FindString(toCorrect.Format);
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
            throw new NotImplementedException();
        }

        internal void TxtBoxISBN10_TextChanged()
        {
            mWriteTab.CheckAndAnableBtnsCalculateISBN(mTxtBoxISBN13, mTxtBoxISBN10, mBtnCalculateISBN13, mBtnCalculateISBN10);
        }

        internal void TxtBoxISBN13_TextChanged()
        {
            mWriteTab.CheckAndAnableBtnsCalculateISBN(mTxtBoxISBN13, mTxtBoxISBN10, mBtnCalculateISBN13, mBtnCalculateISBN10);
        }

        #endregion
    }
}
