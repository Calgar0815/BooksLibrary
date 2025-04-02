using ISBNCaller_Lib;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace ISBNCaller_GUI
{
    public partial class Form1 : Form
    {
        const string cSettingsPath = @"..\Settings.xml";
        internal string mLanguage { get; private set; }
        internal string mColorMode { get; private set; }
        public Form1()
        {
            InitializeComponent();
            LoadLanguage();
            LoadColorMode();
            mWriteTab = new WriteTab(this);
            mLentTab = new LentTab();
            mReturnTab = new ReturnTab();
            mSearchTab = new SearchTab(this);
            InitializeWriteTabObjects();
            InitializeLentTabObjects();
            InitializeReturnTabObjects();
            InitializeSearchTabObjects();
            mWriteTab.DisableTxtBoxes();
            this.AcceptButton = SearchTab_btnSearch;
            SearchTab_txtBoxISBN.Focus();
            WorkInProgressLabel.Visible = false;
        }

        enum mLanguagesEnum
        {
            de,
            en
        }

        enum mLanguageNamesEnum
        {
            Sprache,
            Language
        }

        public static List<mLanguagesEnum> GetLanguagesEnumList<mLanguagesEnum>() where mLanguagesEnum : Enum
    => ((mLanguagesEnum[])Enum.GetValues(typeof(mLanguagesEnum))).ToList();

        private void LoadLanguage()
        {
            if (!File.Exists(cSettingsPath))
            {
                XmlWriter writer = new XmlWriter(cSettingsPath);
                writer.CreateSettingsXML(cSettingsPath, "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n<Settings>\r\n\t<Language>de</Language>\r\n\t<ColorMode>Dark</ColorMode>\r\n</Settings>");
            } // if

            XmlReader reader = new XmlReader(cSettingsPath);
            mLanguage = reader.Read("Language");
            
            foreach (var lang in GetLanguagesEnumList<mLanguagesEnum>())
            {
                cmbBoxLanguage.Items.Add(lang.ToString());
            }

            int index = cmbBoxLanguage.FindString(mLanguage);
            if (index > -1)
            {
                cmbBoxLanguage.SelectedIndex = index;
            }


        }

        enum mColorModesEnum
        {
            System,
            Dark
        }

        public static List<mColorModesEnum> GetColorModesEnumList<mColorModesEnum>() where mColorModesEnum : Enum
    => ((mColorModesEnum[])Enum.GetValues(typeof(mColorModesEnum))).ToList();

        private void LoadColorMode()
        {
            XmlReader reader = new XmlReader(cSettingsPath);
            try
            {
                mColorMode = reader.Read("ColorMode");
            } // try
            catch (Exception ex)
            {
                XmlWriter writer = new XmlWriter(cSettingsPath);
                writer.AddNode("ColorMode", "Dark");
                mColorMode = "Dark";
            }

            foreach (var cm in GetColorModesEnumList<mColorModesEnum>())
            {
                cmbBoxColorMode.Items.Add(cm.ToString());
            }

            int index = cmbBoxColorMode.FindString(mColorMode);
            if (index > -1)
            {
                cmbBoxColorMode.SelectedIndex = index;
            }

            ChangeColor();
        }

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TabControl.SelectedTab.Text == "Eintragen")
            {
                this.AcceptButton = WriteTab_btnOK;
                WriteTab_txtBoxISBN_1.Focus();
            }

            if (TabControl.SelectedTab.Text == "Verleihen")
            {
                this.AcceptButton = LentTab_btnSearch;
                LentTab_txtBoxISBN.Focus();
            }

            if (TabControl.SelectedTab.Text == "Zurücknehmen")
            {
                ReturnTab_btnShowAll.Focus();
            }

            if (TabControl.SelectedTab.Text == "Suchen")
            {
                this.AcceptButton = SearchTab_btnSearch;
                SearchTab_txtBoxISBN.Focus();
                SearchTab_cmbBoxSeries.DataSource = null;
                SearchTab_cmbBoxSeries.Items.Clear();
                FillCmbBoxSeries(SearchTab_cmbBoxSeries);
            } // if
        }

        #region WriteTab

        WriteTab mWriteTab;

        private void InitializeWriteTabObjects()
        {
            mWriteTab.mTxtBoxISBN_1 = WriteTab_txtBoxISBN_1;
            mWriteTab.mBtnOK = WriteTab_btnOK;
            mWriteTab.mBtnCancel = WriteTab_btnCancel;
            mWriteTab.mDataGridViewAuthor = WriteTab_Author_dataGridViewAuthor;
            mWriteTab.mCmbBoxFormat = WriteTab_Book_cmbBoxFormat;
            mWriteTab.mTxtBoxISBN10 = WriteTab_Book_txtBoxISBN10;
            mWriteTab.mTxtBoxISBN13 = WriteTab_Book_txtBoxISBN13;
            mWriteTab.mTxtBoxPublishingDate = WriteTab_Book_txtBoxPublishingDate;
            mWriteTab.mTxtBoxSubTitle = WriteTab_Book_txtBoxSubTitle;
            mWriteTab.mTxtBoxTitle = WriteTab_Book_txtBoxTitle;
            mWriteTab.mChkBoxIsPartOfSeries = WriteTab_Book_chkBoxIsPartOfSeries;
            mWriteTab.mTxtBoxNoInSeries = WriteTab_Book_txtBoxNoInSeries;
            mWriteTab.mCmbBoxSeries = WriteTab_Book_cmbBoxSeries;
            mWriteTab.mChkBoxIsNewSeries = WriteTab_Book_chkBoxIsNewSeries;
            mWriteTab.mTxtBoxNewSeriesName = WriteTab_Book_txtBoxNewSeriesName;
            mWriteTab.mBtnCalculateISBN10 = WriteTab_Book_btnCalculateISBN10;
            mWriteTab.mBtnCalculateISBN13 = WriteTab_Book_btnCalculateISBN13;
            mWriteTab.mBtnRegisterWOutISBN = WriteTab_btnRegisterWOutISBN;
            mWriteTab.mLabelMaxNoCount = WriteTab_Book_labelMaxNoCount;
            mWriteTab.mWorkInProgressLabel = WorkInProgressLabel;
        }

        private void LanguageChanged(object sender, EventArgs e)
        {
            if (mLanguage == cmbBoxLanguage.Text) return;

            mLanguage = cmbBoxLanguage.Text;
            XmlReader reader = new XmlReader(cSettingsPath);
            string language = reader.Read("Language");
            if (language != mLanguage)
            {
                XmlWriter writer = new XmlWriter(cSettingsPath);
                bool ok = writer.Write("Language", mLanguage);
                if (!ok)
                {
                    MessageBox.Show("Sprache konnte nicht geändert werden.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Sprache ändern

            } // if
        }

        private void ColorModeChanged(object sender, EventArgs e)
        {
            if (mColorMode == cmbBoxColorMode.Text) return;

            mColorMode = cmbBoxColorMode.Text;
            XmlReader reader = new XmlReader(cSettingsPath);
            string colorMode = reader.Read("ColorMode");
            if (colorMode != mColorMode)
            {
                XmlWriter writer = new XmlWriter(cSettingsPath);
                bool ok = writer.Write("ColorMode", mColorMode);
                if (!ok)
                {
                    MessageBox.Show("Farbschema konnte nicht geändert werden.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                ChangeColor();

            } // if
        }

        private void ChangeColor()
        {
            #region Buttons
            ChangeButtonColors(SearchTab_btnSearch);
            ChangeButtonColors(SearchTab_btnCorrection);
            ChangeButtonColors(SearchTab_btnCorrection);
            ChangeButtonColors(WriteTab_btnCancel);
            ChangeButtonColors(WriteTab_btnOK);
            ChangeButtonColors(WriteTab_btnRegisterWOutISBN);
            ChangeButtonColors(WriteTab_Book_btnCalculateISBN10);
            ChangeButtonColors(WriteTab_Book_btnCalculateISBN13);
            ChangeButtonColors(LentTab_btnLent);
            ChangeButtonColors(LentTab_btnPull);
            ChangeButtonColors(LentTab_btnRemove);
            ChangeButtonColors(LentTab_btnSearch);
            ChangeButtonColors(ReturnTab_btnReturn);
            ChangeButtonColors(ReturnTab_btnSearch);
            ChangeButtonColors(ReturnTab_btnShowAll);
            #endregion

            if (mColorMode == mColorModesEnum.Dark.ToString())
            {
                tabPageSearch.BackColor = Color.Black;
                tabPageLent.BackColor = Color.Black;
                tabPageReturn.BackColor = Color.Black;
                tabPageWrite.BackColor = Color.Black;
            } // if
            else if (mColorMode == mColorModesEnum.System.ToString())
            {
                tabPageSearch.BackColor = Color.White;
                tabPageLent.BackColor = Color.White;
                tabPageReturn.BackColor = Color.White;
                tabPageWrite.BackColor = Color.White;
            } // else if

            #region Labels & RadioButtons & CheckBoxes
            ChangeLabelColors(SearchTab_labelAuthorPreName);
            ChangeLabelColors(SearchTab_labelAuthorSurName);
            ChangeLabelColors(SearchTab_labelFormat);
            ChangeLabelColors(SearchTab_labelISBN);
            ChangeLabelColors(SearchTab_labelPublishedFrom);
            ChangeLabelColors(SearchTab_labelPublishedTo);
            ChangeLabelColors(SearchTab_labelSeries);
            ChangeLabelColors(SearchTab_labelSubTitle);
            ChangeLabelColors(SearchTab_labelTitle);
            ChangeLabelColors(SearchTab_radioBtnWOutSeries);
            ChangeLabelColors(SearchTab_radioBtnWSeries);
            ChangeLabelColors(SearchTab_chkBoxOnlyShowFirstAuthor);
            ChangeLabelColors(SearchTab_chkBoxShowLent);
            #endregion

            #region TextBoxes & ComboBoxes
            ChangeTextBoxColors(SearchTab_txtBoxAuthorPreName);
            ChangeTextBoxColors(SearchTab_txtBoxAuthorSurName);
            ChangeTextBoxColors(SearchTab_txtBoxISBN);
            ChangeTextBoxColors(SearchTab_txtBoxSubTitle);
            ChangeTextBoxColors(SearchTab_txtBoxTitle);
            ChangeTextBoxColors(SearchTab_cmbBoxFormat);
            ChangeTextBoxColors(SearchTab_cmbBoxSeries);
            #endregion
        }

        private void ChangeTextBoxColors(Control control)
        {
            if (mColorMode == mColorModesEnum.Dark.ToString())
            {
                control.BackColor = Color.DarkGray;
                control.ForeColor = Color.White;
            } // if
            else if (mColorMode == mColorModesEnum.System.ToString())
            {
                control.BackColor = Color.White;
                control.ForeColor = Color.Black;
            } // else if
        }

        private void ChangeLabelColors(Control control)
        {
            if (mColorMode == mColorModesEnum.Dark.ToString())
            {
                control.ForeColor = Color.DarkGray;
            } // if
            else if (mColorMode == mColorModesEnum.System.ToString())
            {
                control.ForeColor = Color.Black;
            } // else if
        }

        private void ChangeButtonColors(Button button)
        {
            if (mColorMode == mColorModesEnum.Dark.ToString())
            {
                button.BackColor = Color.Black;
                button.ForeColor = Color.DarkGray;
            } // if
            else if (mColorMode == mColorModesEnum.System.ToString())
            {
                button.BackColor = Color.FromArgb(0, 255, 255, 255);
                button.ForeColor = Color.Black;
            } // else if
        }

        private List<Button> GetAllButtons(List<Button> buttonList, Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                if (control.Controls.Count > 0)
                {
                    GetAllButtons(buttonList, controls);
                }

                if (control.GetType() == typeof(Button))
                {
                    buttonList.Add((Button)control);
                }
            } // foreach

            return buttonList;
        }

        private void WriteTab_btnOK_Click(object sender, EventArgs e)
        {
            mWriteTab.btnOK_Click();
        }

        private void WriteTab_btnCancel_Click(object sender, EventArgs e)
        {
            mWriteTab.btnCancel_Click();
        }

        private void WriteTab_txtBoxISBN_1_TextChanged(object sender, EventArgs e)
        {
            mWriteTab.txtBoxISBN_1_TextChanged();
        }

        private void WriteTab_Author_dataGridViewAuthor_RowAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (e.RowIndex < 1) return;

            mWriteTab.dataGridViewAuthor_RowAdded(e.RowIndex - 1);
        }

        private void WriteTab_Book_chkBoxIsPartOfSeries_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkBox = (CheckBox)sender;
            mWriteTab.chkBoxIsPartOfSeries_CheckedChanged(chkBox);
        }

        private void WriteTab_Book_chkBoxIsNewSeries_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkBox = (CheckBox)sender;
            mWriteTab.chkBoxIsNewSeries_CheckedChanged(chkBox);
        }

        private void WriteTab_Book_cmbBoxSeries_SelectedIndexChanged(object sender, EventArgs e)
        {
            mWriteTab.cmbBoxSeries_SelectedIndexChanged();
        }

        private void WriteTab_Book_btnCalculateISBN10_Click(object sender, EventArgs e)
        {
            mWriteTab.btnCalculateISBN10_Click();
        }

        private void WriteTab_Book_btnCalculateISBN13_Click(object sender, EventArgs e)
        {
            mWriteTab.btnCalculateISBN13_Click();
        }

        private void WriteTab_btnRegisterWOutISBN_Click(object sender, EventArgs e)
        {
            mWriteTab.btnRegisterWOutISBN_Click();
        }

        private void WriteTab_Book_txtBoxISBN10_TextChanged(object sender, EventArgs e)
        {
            mWriteTab.CheckAndEnableTxtBoxesCalculateISBN();
        }

        private void WriteTab_Book_txtBoxISBN13_TextChanged(object sender, EventArgs e)
        {
            mWriteTab.CheckAndEnableTxtBoxesCalculateISBN();
        }

        private void WriteTab_Book_cmdBoxFormat_EnabledChanged(object sender, EventArgs e)
        {
            mWriteTab.cmbBoxFormat_EnabledChanged();
        }

        #endregion

        #region LentTab

        LentTab mLentTab;

        private void InitializeLentTabObjects()
        {
            LentTab_cmbBoxISBN.SelectedIndex = 0;
            LentTab_cmbBoxTitle.SelectedIndex = 0;
            LentTab_cmbBoxSubTitle.SelectedIndex = 0;
            mLentTab.mTxtBoxISBN = LentTab_txtBoxISBN;
            mLentTab.mCmbBoxISBN = LentTab_cmbBoxISBN;
            mLentTab.mTxtBoxTitle = LentTab_txtBoxTitle;
            mLentTab.mCmbBoxTitle = LentTab_cmbBoxTitle;
            mLentTab.mTxtBoxSubTitle = LentTab_txtBoxSubTitle;
            mLentTab.mCmbBoxSubTitle = LentTab_cmbBoxSubTitle;
            mLentTab.mTxtBoxAuthorPreName = LentTab_txtBoxAuthorPreName;
            mLentTab.mTxtBoxAuthorSurName = LentTab_txtBoxAuthorSurName;
            mLentTab.mDataGridViewSearch = LentTab_dataGridViewSearch;
            mLentTab.mBtnSearch = LentTab_btnSearch;
            mLentTab.mBtnPull = LentTab_btnPull;
            mLentTab.mDataGridViewLent = LentTab_dataGridViewLent;
            mLentTab.mBtnLent = LentTab_btnLent;
            mLentTab.mBtnRemove = LentTab_btnRemove;
            mLentTab.InitializeDGVs();
        }

        private void LentTab_btnSearch_Click(object sender, EventArgs e)
        {
            mLentTab.btnSearch_Click();
        }

        private void LentTab_btnPull_Click(object sender, EventArgs e)
        {
            mLentTab.btnPull_Click();
        }

        private void LentTab_btnLent_Click(object sender, EventArgs e)
        {
            mLentTab.btnLent_Click();
        }

        private void LentTab_btnRemove_Click(object sender, EventArgs e)
        {
            mLentTab.btnRemove_Click();
        }

        #endregion

        #region ReturnTab

        ReturnTab mReturnTab;

        private void InitializeReturnTabObjects()
        {
            mReturnTab.mBtnReturn = ReturnTab_btnReturn;
            mReturnTab.mBtnSearch = ReturnTab_btnSearch;
            mReturnTab.mBtnShowAll = ReturnTab_btnShowAll;
            mReturnTab.mDataGridViewReturn = ReturnTab_dataGridViewReturn;
            mReturnTab.mTxtBoxISBN = ReturnTab_txtBoxISBN;
            mReturnTab.mTxtBoxPreName = ReturnTab_txtBoxPreName;
            mReturnTab.mTxtBoxSurName = ReturnTab_txtBoxSurName;
            mReturnTab.mTxtBoxTitle = ReturnTab_txtBoxBookTitle;
            mReturnTab.mChkBoxIgnoreIsActive = ReturnTab_chkBoxIgnoreIsActive;
            mReturnTab.mBtnReturn.Enabled = false;
            mReturnTab.InitializeDGVs();
            mReturnTab.mBtnShowAll.GotFocus += new EventHandler(ReturnTabShowAll_GotFocus);
            mReturnTab.mDataGridViewReturn.GotFocus += new EventHandler(DGVReturn_GotFocus);
            mReturnTab.mTxtBoxISBN.GotFocus += new EventHandler(ReturnTabSearch_GotFocus);
            mReturnTab.mTxtBoxPreName.GotFocus += new EventHandler(ReturnTabSearch_GotFocus);
            mReturnTab.mTxtBoxSurName.GotFocus += new EventHandler(ReturnTabSearch_GotFocus);
            mReturnTab.mTxtBoxTitle.GotFocus += new EventHandler(ReturnTabSearch_GotFocus);
        }

        private void DGVReturn_GotFocus(object sender, EventArgs e)
        {
            this.AcceptButton = ReturnTab_btnReturn;
        }

        private void ReturnTabSearch_GotFocus(object sender, EventArgs e)
        {
            this.AcceptButton = ReturnTab_btnSearch;
        }

        private void ReturnTabShowAll_GotFocus(object sender, EventArgs e)
        {
            this.AcceptButton = ReturnTab_btnShowAll;
        }

        private void ReturnTab_btnShowAll_Click(object sender, EventArgs e)
        {
            mReturnTab.btnShowAllClick();
        }

        private void ReturnTab_btnSearch_Click(object sender, EventArgs e)
        {
            mReturnTab.btnSearch_Click();
        }

        private void ReturnTab_btnReturn_Click(object sender, EventArgs e)
        {
            mReturnTab.btnReturn_Click();
        }

        #endregion

        #region SearchTab

        SearchTab mSearchTab;
        private void InitializeSearchTabObjects()
        {
            mSearchTab.mTxtBoxISBN = SearchTab_txtBoxISBN;
            mSearchTab.mTxtBoxTitle = SearchTab_txtBoxTitle;
            mSearchTab.mTxtBoxSubTitle = SearchTab_txtBoxSubTitle;
            mSearchTab.mTxtBoxAuthorPreName = SearchTab_txtBoxAuthorPreName;
            mSearchTab.mTxtBoxAuthorSurName = SearchTab_txtBoxAuthorSurName;
            mSearchTab.mCmbBoxSeries = SearchTab_cmbBoxSeries;
            mSearchTab.mRadioBtnWSeries = SearchTab_radioBtnWSeries;
            mSearchTab.mRadioBtnWOutSeries = SearchTab_radioBtnWOutSeries;
            mSearchTab.mCmbBoxFormat = SearchTab_cmbBoxFormat;
            mSearchTab.mDtpFrom = SearchTab_dtpFrom;
            mSearchTab.mDtpTo = SearchTab_dtpTo;
            mSearchTab.mChkBoxShowLent = SearchTab_chkBoxShowLent;
            mSearchTab.mChkBoxOnlyShowFirstAuthor = SearchTab_chkBoxOnlyShowFirstAuthor;
            mSearchTab.mDataGridViewSearch = SearchTab_dataGridViewSearch;
            mSearchTab.InitializeDGV();
            mSearchTab.InitializeCmbBoxFormat();
            FillCmbBoxSeries(SearchTab_cmbBoxSeries);
        }

#if DEBUG
        private void SearchTab_btnSearch_Click(object sender, EventArgs e)
        {
            mSearchTab.btnSearchClick();
            SearchTab_btnCorrection.Enabled = true;
        }

        private void SearchTab_btnCorrection_Click(object sender, EventArgs e)
        {
            mSearchTab.btnCorrectionClick();
        }
#endif
        #endregion

        #region all

        public void FillCmbBoxSeries(ComboBox cmbBoxSeries)
        {
            DBReader dbReader = new DBReader();
            Dictionary<int, string> series = new Dictionary<int, string>();
            series.Add(-2, "");
            series = dbReader.GetAllSeries(series);
            cmbBoxSeries.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbBoxSeries.DisplayMember = "value";
            cmbBoxSeries.ValueMember = "key";
            cmbBoxSeries.DataSource = new BindingSource(series, null);
        }

        public void FillCmbBoxFormat(ComboBox cmbBoxFormat)
        {
            DBReader dbReader = new DBReader();
            List<string> format = new List<string>();
            format = dbReader.GetAllFormats();
            cmbBoxFormat.Items.AddRange(format.ToArray());
        }

        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
