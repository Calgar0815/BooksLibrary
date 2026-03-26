using ISBNCaller_Lib;
using System;
using System.Windows.Forms;

namespace ISBNCaller_GUI.Correction
{
    public partial class CorrectionForm : Form
    {
        #region Variables

        private CorrectionFormWorker mCorrectionFormWorker { get; set; }

        #endregion
        #region Constructor

        public CorrectionForm(DBReader.SearchStruct toCorrect, Form1 form1)
        {
            InitializeComponent();
            mCorrectionFormWorker = new CorrectionFormWorker(form1);
            PrepareForCorrection(toCorrect);
        }

        #endregion
        #region Methods

        private void PrepareForCorrection(DBReader.SearchStruct toCorrect)
        {
            mCorrectionFormWorker.mLabelTitle = CorrectionForm_labelTitle;
            mCorrectionFormWorker.mLabelSubTitle = CorrectionForm_labelSubTitle;
            mCorrectionFormWorker.mLabelSeries = CorrectionForm_labelSeries;
            mCorrectionFormWorker.mLabelCurrentSeries = CorrectionForm_labelCurrentSeries;
            mCorrectionFormWorker.mLabelCurrentSeriesNo = CorrectionForm_labelCurrentSeriesNo;
            mCorrectionFormWorker.mLabelNoInSeries = CorrectionForm_labelNoInSeries;
            mCorrectionFormWorker.mLabelPublished = CorrectionForm_labelPublished;
            mCorrectionFormWorker.mLabelFormat = CorrectionForm_labelFormat;
            mCorrectionFormWorker.mLabelISBN13 = CorrectionForm_labelISBN13;
            mCorrectionFormWorker.mLabelISBN10 = CorrectionForm_labelISBN10;
            mCorrectionFormWorker.mLabelAuthor = CorrectionForm_labelAuthor;
            mCorrectionFormWorker.mTxtBoxTitle = CorrectionForm_txtBoxTitle;
            mCorrectionFormWorker.mTxtBoxSubTitle = CorrectionForm_txtBoxSubTitle;
            mCorrectionFormWorker.mCmbBoxSeries = CorrectionForm_cmbBoxSeries;
            mCorrectionFormWorker.mTxtBoxNoInSeries = CorrectionForm_txtBoxNoInSeries;
            mCorrectionFormWorker.mTxtBoxPublished = CorrectionForm_txtBoxPublished;
            mCorrectionFormWorker.mCmbBoxFormat = CorrectionForm_cmbBoxFormat;
            mCorrectionFormWorker.mTxtBoxISBN13 = CorrectionForm_txtBoxISBN13;
            mCorrectionFormWorker.mTxtBoxISBN10 = CorrectionForm_txtBoxISBN10;
            mCorrectionFormWorker.mDGVAuthors = CorrectionForm_dgvAuthors;
            mCorrectionFormWorker.mBtnCalculateISBN13 = CorrectionForm_btnCalculateISBN13;
            mCorrectionFormWorker.mBtnCalculateISBN10 = CorrectionForm_btnCalculateISBN10;
            mCorrectionFormWorker.mBtnStartCorrection = CorrectionForm_btnStartCorrection;
            mCorrectionFormWorker.mBtnDeleteBook = CorrectionForm_btnDeleteBook;
            mCorrectionFormWorker.mBtnClose = CorrectionForm_btnClose;
            mCorrectionFormWorker.PrepareForCorrection(toCorrect);
        }

        #endregion
        #region Events

        private void CorrectionForm_btnCalculateISBN13_OnClick(object sender, EventArgs e)
        {
            mCorrectionFormWorker.BtnCalculateISBN13_OnClick();
        }

        private void CorrectionForm_btnCalculateISBN10_OnClick(object sender, EventArgs e)
        {
            mCorrectionFormWorker.BtnCalculateISBN10_OnClick();
        }

        private void CorrectionForm_btnStartCorrection_OnClick(object sender, EventArgs e)
        {
            DialogResult result = mCorrectionFormWorker.BtnStartCorrection_OnClick();
            if (result == DialogResult.OK)
            {
                Close();
            }
        }

        private void CorrectionForm_txtBoxISBN13_TextChanged(object sender, EventArgs e)
        {
            mCorrectionFormWorker.TxtBoxISBN13_TextChanged();
        }

        private void CorrectionForm_txtBoxISBN10_TextChanged(object sender, EventArgs e)
        {
            mCorrectionFormWorker.TxtBoxISBN10_TextChanged();
        }

        private void CorrectionForm_btnClose_OnClick(object sender, EventArgs e)
        {
            Close();
        }

        private void CorrectionForm_cmbBoxSeries_SelectedIndexChanged(object sender, EventArgs e)
        {
            mCorrectionFormWorker.CmbBoxSeries_SelectedIndexChanged();
        }

        private void CorrectionForm_dgvAuthors_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            DataGridView dgvAuthors = (DataGridView)sender;
            if (e.RowIndex < 1 || dgvAuthors.Rows[e.RowIndex].Cells[2].Value != null) return;

            mCorrectionFormWorker.DGVAuthors_RowAdded(e.RowIndex - 1);
        }

        private void CorrectionForm_btnDeleteBook_Click(object sender, EventArgs e)
        {
            DialogResult result = mCorrectionFormWorker.BtnDeleteBook_OnClick();
            if(result == DialogResult.OK)
            {
                Close();
            }
        }

        #endregion
    }
}
