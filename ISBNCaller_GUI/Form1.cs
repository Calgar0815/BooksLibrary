using ISBNCaller_Lib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ISBNCaller_GUI
{
    public partial class Form1 : Form
    {
        #region Variables
        const string cSettingsPath = @"..\Settings.xml";
        const string cLanguagesFilePath = @"..\LabelTexts.xml";
        internal string mLanguage { get; private set; }
        internal string mColorMode { get; private set; }

        #endregion
        #region Constructor

        public Form1()
        {
            InitializeComponent();
            LoadColorMode();
            mWriteTab = new WriteTab(this);
            mLentTab = new LentTab();
            mReturnTab = new ReturnTab();
            mSearchTab = new SearchTab(this);
            InitializeWriteTabObjects();
            InitializeLentTabObjects();
            InitializeReturnTabObjects();
            InitializeSearchTabObjects();
#if DEBUG || RELEASE
            LoadLanguage();
#endif
            mWriteTab.DisableTxtBoxes();
            this.AcceptButton = SearchTab_btnSearch;
            SearchTab_txtBoxISBN.Focus();
            WriteTab_WorkInProgressLabel.Visible = false;
        }

        #endregion
        #region Overall Settings

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

#if DEBUG || RELEASE
        private void LoadLanguage()
        {
            XmlReader reader = new XmlReader(cSettingsPath);
            mLanguage = reader.Read($"/Settings/Language");

            foreach (var lang in GetLanguagesEnumList<mLanguagesEnum>())
            {
                cmbBoxLanguage.Items.Add(lang.ToString());
            }

            int index = cmbBoxLanguage.FindString(mLanguage);
            if (index > -1)
            {
                cmbBoxLanguage.SelectedIndex = index;
            }

            LanguageWorker languageWorker = new LanguageWorker(mLanguage, cLanguagesFilePath, this);
            languageWorker.LoadTexts();
        }
#endif

        internal enum mColorModesEnum
        {
            System,
            Dark
        }

        public static List<mColorModesEnum> GetColorModesEnumList<mColorModesEnum>() where mColorModesEnum : Enum
    => ((mColorModesEnum[])Enum.GetValues(typeof(mColorModesEnum))).ToList();

        private void LoadColorMode()
        {
            if (!File.Exists(cSettingsPath))
            {
                XmlWriter writer = new XmlWriter(cSettingsPath);
                writer.CreateSettingsXML(cSettingsPath, "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n<Settings>\r\n\t<Language>de</Language>\r\n\t<ColorMode>Dark</ColorMode>\r\n</Settings>");
            } // if

            XmlReader reader = new XmlReader(cSettingsPath);
            try
            {
                mColorMode = reader.Read($"/Settings/ColorMode");
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
            if (tabControl.SelectedTab.Text == "Eintragen")
            {
                this.AcceptButton = WriteTab_btnOK;
                WriteTab_txtBoxISBN_1.Focus();
            }

            if (tabControl.SelectedTab.Text == "Verleihen")
            {
                this.AcceptButton = LentTab_btnSearch;
                LentTab_txtBoxISBN.Focus();
            }

            if (tabControl.SelectedTab.Text == "Zurücknehmen")
            {
                ReturnTab_btnShowAll.Focus();
            }

            if (tabControl.SelectedTab.Text == "Suchen")
            {
                this.AcceptButton = SearchTab_btnSearch;
                SearchTab_txtBoxISBN.Focus();
                SearchTab_cmbBoxSeries.DataSource = null;
                SearchTab_cmbBoxSeries.Items.Clear();
                FillCmbBoxSeries(SearchTab_cmbBoxSeries);
            } // if
        }

#if DEBUG || RELEASE
        private void LanguageChanged(object sender, EventArgs e)
        {
            if (mLanguage == cmbBoxLanguage.Text) return;

            mLanguage = cmbBoxLanguage.Text;
            XmlReader reader = new XmlReader(cSettingsPath);
            string language = reader.Read($"/Settings/Language");
            if (language != mLanguage)
            {
                XmlWriter writer = new XmlWriter(cSettingsPath);
                bool ok = writer.Write("Language", mLanguage);
                if (!ok)
                {
                    MessageBox.Show("Sprache konnte nicht geändert werden.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                LanguageWorker languageWorker = new LanguageWorker(mLanguage, cLanguagesFilePath, this);
                languageWorker.LoadTexts();
            } // if
        }
#endif

        private void ColorModeChanged(object sender, EventArgs e)
        {
            if (mColorMode == cmbBoxColorMode.Text) return;

            mColorMode = cmbBoxColorMode.Text;
            XmlReader reader = new XmlReader(cSettingsPath);
            string colorMode = reader.Read($"/Settings/ColorMode");
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
            ColorWorker colorWorker = new ColorWorker(mColorMode);

            #region Buttons
            colorWorker.ChangeButtonColors(SearchTab_btnSearch);
            colorWorker.ChangeButtonColors(WriteTab_btnCancel);
            colorWorker.ChangeButtonColors(WriteTab_btnOK);
            colorWorker.ChangeButtonColors(WriteTab_btnRegisterWOutISBN);
            colorWorker.ChangeButtonColors(WriteTab_Book_btnCalculateISBN10);
            colorWorker.ChangeButtonColors(WriteTab_Book_btnCalculateISBN13);
            colorWorker.ChangeButtonColors(LentTab_btnLent);
            colorWorker.ChangeButtonColors(LentTab_btnPull);
            colorWorker.ChangeButtonColors(LentTab_btnRemove);
            colorWorker.ChangeButtonColors(LentTab_btnSearch);
            colorWorker.ChangeButtonColors(ReturnTab_btnReturn);
            colorWorker.ChangeButtonColors(ReturnTab_btnSearch);
            colorWorker.ChangeButtonColors(ReturnTab_btnShowAll);
            #endregion

            #region Backgrounds, etc.

            if (mColorMode == mColorModesEnum.Dark.ToString())
            {
                this.BackColor = Color.Black;
                tabPageSearch.BackColor = Color.Black;
                tabPageLent.BackColor = Color.Black;
                tabPageReturn.BackColor = Color.Black;
                tabPageWrite.BackColor = Color.Black;
            } // if
            else if (mColorMode == mColorModesEnum.System.ToString())
            {
                this.BackColor = Color.White;
                tabPageSearch.BackColor = Color.White;
                tabPageLent.BackColor = Color.White;
                tabPageReturn.BackColor = Color.White;
                tabPageWrite.BackColor = Color.White;
            } // else if

            #endregion

            #region Labels & RadioButtons & CheckBoxes
            colorWorker.ChangeLabelColors(SearchTab_labelAuthorPreName);
            colorWorker.ChangeLabelColors(SearchTab_labelAuthorSurName);
            colorWorker.ChangeLabelColors(SearchTab_labelFormat);
            colorWorker.ChangeLabelColors(SearchTab_labelISBN);
            colorWorker.ChangeLabelColors(SearchTab_labelPublishedFrom);
            colorWorker.ChangeLabelColors(SearchTab_labelPublishedTo);
            colorWorker.ChangeLabelColors(SearchTab_labelSeries);
            colorWorker.ChangeLabelColors(SearchTab_labelSubTitle);
            colorWorker.ChangeLabelColors(SearchTab_labelTitle);
            colorWorker.ChangeLabelColors(SearchTab_radioBtnWOutSeries);
            colorWorker.ChangeLabelColors(SearchTab_radioBtnWSeries);
            colorWorker.ChangeLabelColors(SearchTab_chkBoxOnlyShowFirstAuthor);
            colorWorker.ChangeLabelColors(SearchTab_chkBoxUseDates);
            colorWorker.ChangeLabelColors(SearchTab_chkBoxShowLent);
            colorWorker.ChangeLabelColors(WriteTab_WorkInProgressLabel);
            colorWorker.ChangeLabelColors(WriteTab_Book_labelFormat);
            colorWorker.ChangeLabelColors(WriteTab_Book_labelISBN10);
            colorWorker.ChangeLabelColors(WriteTab_Book_labelISBN13);
            colorWorker.ChangeLabelColors(WriteTab_Book_labelMaxNo);
            colorWorker.ChangeLabelColors(WriteTab_Book_labelMaxNoCount);
            colorWorker.ChangeLabelColors(WriteTab_Book_labelNoInSeries);
            colorWorker.ChangeLabelColors(WriteTab_Book_labelPublishingDate);
            colorWorker.ChangeLabelColors(WriteTab_Book_labelSubTitle);
            colorWorker.ChangeLabelColors(WriteTab_Book_labelTitle);
            colorWorker.ChangeLabelColors(WriteTab_Book_chkBoxIsNewSeries);
            colorWorker.ChangeLabelColors(WriteTab_Book_chkBoxIsPartOfSeries);
            colorWorker.ChangeLabelColors(LentTab_labelAuthorPreName);
            colorWorker.ChangeLabelColors(LentTab_labelAuthorSurName);
            colorWorker.ChangeLabelColors(LentTab_labelISBN);
            colorWorker.ChangeLabelColors(LentTab_labelSubTitle);
            colorWorker.ChangeLabelColors(LentTab_labelTitle);
            colorWorker.ChangeLabelColors(ReturnTab_labelBookTitle);
            colorWorker.ChangeLabelColors(ReturnTab_labelISBN);
            colorWorker.ChangeLabelColors(ReturnTab_labelLentTo);
            colorWorker.ChangeLabelColors(ReturnTab_labelOptional);
            colorWorker.ChangeLabelColors(ReturnTab_labelPreName);
            colorWorker.ChangeLabelColors(ReturnTab_labelSurName);
            colorWorker.ChangeLabelColors(ReturnTab_chkBoxIgnoreIsActive);
            #endregion

            #region TextBoxes & ComboBoxes
            colorWorker.ChangeTextBoxColors(SearchTab_txtBoxAuthorPreName);
            colorWorker.ChangeTextBoxColors(SearchTab_txtBoxAuthorSurName);
            colorWorker.ChangeTextBoxColors(SearchTab_txtBoxISBN);
            colorWorker.ChangeTextBoxColors(SearchTab_txtBoxSubTitle);
            colorWorker.ChangeTextBoxColors(SearchTab_txtBoxTitle);
            colorWorker.ChangeTextBoxColors(SearchTab_cmbBoxFormat);
            colorWorker.ChangeTextBoxColors(SearchTab_cmbBoxSeries);
            colorWorker.ChangeTextBoxColors(WriteTab_txtBoxISBN_1);
            colorWorker.ChangeTextBoxColors(WriteTab_Book_txtBoxISBN10);
            colorWorker.ChangeTextBoxColors(WriteTab_Book_txtBoxISBN13);
            colorWorker.ChangeTextBoxColors(WriteTab_Book_txtBoxNewSeriesName);
            colorWorker.ChangeTextBoxColors(WriteTab_Book_txtBoxNoInSeries);
            colorWorker.ChangeTextBoxColors(WriteTab_Book_txtBoxPublishingDate);
            colorWorker.ChangeTextBoxColors(WriteTab_Book_txtBoxSubTitle);
            colorWorker.ChangeTextBoxColors(WriteTab_Book_txtBoxTitle);
            colorWorker.ChangeTextBoxColors(WriteTab_Book_cmbBoxFormat);
            colorWorker.ChangeTextBoxColors(WriteTab_Book_cmbBoxSeries);
            colorWorker.ChangeTextBoxColors(LentTab_txtBoxAuthorPreName);
            colorWorker.ChangeTextBoxColors(LentTab_txtBoxAuthorSurName);
            colorWorker.ChangeTextBoxColors(LentTab_txtBoxISBN);
            colorWorker.ChangeTextBoxColors(LentTab_txtBoxSubTitle);
            colorWorker.ChangeTextBoxColors(LentTab_txtBoxTitle);
            colorWorker.ChangeTextBoxColors(LentTab_cmbBoxISBN);
            colorWorker.ChangeTextBoxColors(LentTab_cmbBoxSubTitle);
            colorWorker.ChangeTextBoxColors(LentTab_cmbBoxTitle);
            colorWorker.ChangeTextBoxColors(ReturnTab_txtBoxBookTitle);
            colorWorker.ChangeTextBoxColors(ReturnTab_txtBoxISBN);
            colorWorker.ChangeTextBoxColors(ReturnTab_txtBoxPreName);
            colorWorker.ChangeTextBoxColors(ReturnTab_txtBoxSurName);
            #endregion

            #region DataGridView
            colorWorker.ChangeDataGridViewColors(SearchTab_dataGridViewSearch);
            colorWorker.ChangeDataGridViewColors(WriteTab_Author_dataGridViewAuthor);
            colorWorker.ChangeDataGridViewColors(LentTab_dataGridViewLent);
            colorWorker.ChangeDataGridViewColors(LentTab_dataGridViewSearch);
            colorWorker.ChangeDataGridViewColors(ReturnTab_dataGridViewReturn);
            #endregion
        }

#if DEBUG || WITHOUTLANGUAGESELECTION_DEBUG
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
#endif

        #endregion
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
            mWriteTab.mWorkInProgressLabel = WriteTab_WorkInProgressLabel;
            mWriteTab.InitializeDGV();
        }

        private void WriteTab_btnOK_Click(object sender, EventArgs e)
        {
            mWriteTab.btnOK_Click();
            ColorWorker colorWorker = new ColorWorker(mColorMode);
            colorWorker.ChangeDataGridViewColors(WriteTab_Author_dataGridViewAuthor);
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
            ColorWorker colorWorker = new ColorWorker(mColorMode);
            colorWorker.ChangeDataGridViewColors(WriteTab_Author_dataGridViewAuthor);
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
            ColorWorker colorWorker = new ColorWorker(mColorMode);
            colorWorker.ChangeDataGridViewColors(LentTab_dataGridViewLent);
            colorWorker.ChangeDataGridViewColors(LentTab_dataGridViewSearch);
        }

        private void LentTab_btnPull_Click(object sender, EventArgs e)
        {
            mLentTab.btnPull_Click();
            ColorWorker colorWorker = new ColorWorker(mColorMode);
            colorWorker.ChangeDataGridViewColors(LentTab_dataGridViewLent);
            colorWorker.ChangeDataGridViewColors(LentTab_dataGridViewSearch);
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
            ColorWorker colorWorker = new ColorWorker(mColorMode);
            colorWorker.ChangeDataGridViewColors(ReturnTab_dataGridViewReturn);
        }

        private void ReturnTab_btnSearch_Click(object sender, EventArgs e)
        {
            mReturnTab.btnSearch_Click();
            ColorWorker colorWorker = new ColorWorker(mColorMode);
            colorWorker.ChangeDataGridViewColors(ReturnTab_dataGridViewReturn);
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
            mSearchTab.mChkBoxUseDates = SearchTab_chkBoxUseDates;
            mSearchTab.mBtnClearFields = SearchTab_btnClearFields;
            mSearchTab.mBtnSearch = SearchTab_btnSearch;
            mSearchTab.mBtnCorrection = SearchTab_btnCorrection;
            mSearchTab.InitializeDGV();
            mSearchTab.InitializeCmbBoxFormat();
            FillCmbBoxSeries(SearchTab_cmbBoxSeries);
        }

        private void SearchTab_btnSearch_Click(object sender, EventArgs e)
        {
            mSearchTab.btnSearchClick();
            ColorWorker colorWorker = new ColorWorker(mColorMode);
            colorWorker.ChangeDataGridViewColors(SearchTab_dataGridViewSearch);
        }

        private void SearchTab_chkBoxUseDates_CheckedChanged(object sender, EventArgs e)
        {
            mSearchTab.chkBoxUseDatesCheckedChanged();
        }

        private void SearchTab_btnClearFields_Click(object sender, EventArgs e)
        {
            mSearchTab.btnClearFieldsClick();
        }

        private void SearchTab_DataGridViewSearch_SelectionChanged(object sender, EventArgs e)
        {
            mSearchTab.dataGridViewSearchSelectionChanged();
        }

        private void SearchTab_txtBoxISBN_TextChanged(object sender, EventArgs e)
        {
            mSearchTab.txtBoxISBNTextChanged();
        }

        private void SearchTab_txtBoxTitle_TextChanged(object sender, EventArgs e)
        {
            mSearchTab.txtBoxTitleTextChanged();
        }

        private void SearchTab_txtBoxSubTitle_TextChanged(object sender, EventArgs e)
        {
            mSearchTab.txtBoxSubTitleTextChanged();
        }

        private void SearchTab_txtBoxAuthorPreName_TextChanged(object sender, EventArgs e)
        {
            mSearchTab.txtBoxAuthorPreNameTextChanged();
        }

        private void SearchTab_txtBoxAuthorSurName_TextChanged(object sender, EventArgs e)
        {
            mSearchTab.txtBoxAuthorSurNameTextChanged();
        }

        private void SearchTab_radioBtnWOutSeries_CheckedChanged(object sender, EventArgs e)
        {
            mSearchTab.radioBtnWOutSeriesCheckedChanged();
        }

        private void SearchTab_cmbBoxSeries_SelectedIndexChanged(object sender, EventArgs e)
        {
            mSearchTab.cmbBoxSeriesSelectedIndexChanged();
        }

        private void SearchTab_cmbBoxFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            mSearchTab.cmbBoxFormatSelectedIndexChanged();
        }

        private void SearchTab_dtpFrom_ValueChanged(object sender, EventArgs e)
        {
            mSearchTab.dtpFromValueChanged();
        }

        private void SearchTab_dtpTo_ValueChanged(object sender, EventArgs e)
        {
            mSearchTab.dtpToValueChanged();
        }

        private void SearchTab_chkBoxShowLent_CheckedChanged(object sender, EventArgs e)
        {
            mSearchTab.chkBoxShowLentCheckedChanged();
        }

        private void SearchTab_chkBoxOnlyShowFirstAuthor_CheckedChanged(object sender, EventArgs e)
        {
            mSearchTab.chkBoxOnlyShowFirstAuthorCheckedChanged();
        }

#if DEBUG || WITHOUTLANGUAGESELECTION_DEBUG
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
