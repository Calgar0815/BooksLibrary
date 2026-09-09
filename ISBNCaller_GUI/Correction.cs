using ISBNCaller_Lib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

namespace ISBNCaller_GUI
{
    public partial class Correction : Form
    {
        #region Variables
        private Form1 mForm1 { get; set; }
        private List<DBReader.SearchStruct> mToCorrectList { get; set; }
        enum mCmbBoxEnum
        {
            Series,
            Format
        }

        #endregion
        #region Constants
        #region TabPage-Texte
        
        static string cTxtBoxTitle = "txtBoxTitleTabPage";
        static string cTxtBoxSubTitle ="txtBoxSubTitleTabPage";
        static string cTxtBoxNoInSeries = "txtBoxNoInSeriesTabPage";
        static string cTxtBoxPublishingDate = "txtBoxPublishingDateTabPage";
        static string cTxtBoxISBN13 = "txtBoxISBN13TabPage";
        static string cTxtBoxISBN10 = "txtBoxISBN10TabPage";
        static string cCmbBoxSeries = "cmbBoxSeriesTabPage";
        static string cCmbBoxFormat = "cmbBoxFormatTabPage";
        static string cLabelSeries2 = "labelSeries2TabPage";
        static string cDGVArtists = "dgvArtistsTabPage";
        static string cLabelPreviousSeriesName = "labelPreviousSeriesNameTabPage";
        static string cBtnTab = "BtnTab";
        static string cLabelTitle = "labelTitleTabPage";
        static string cLabelSubTitle = "labelSubTitleTabPage";
        static string cLabelSeries = "labelSeriesTabPage";
        static string cLabelNoInSeries = "labelNoInSeriesTabPage";
        static string cLabelPublishingDate = "labelPublishingDateTabPage";
        static string cLabelFormat = "labelFormatTabPage";
        static string cLabelISBN13 = "labelISBN13TabPage";
        static string cLabelISBN10 = "labelISBN10TabPage";
        static string cLabelAuthors = "labelAuthorsTabPage";

        #endregion
        #region Allgemeine Texte

        static string cReallyDeleteSeries = "Serienzuordnung wirklich löschen?";
        static string cReallyChangSeries = "Serienzuordnung wirklich ändern?";
        static string cCorrectionsSuccessful = "Die Korrekturen wurden erfolgreich durchgeführt.";
        static string cInfo = "Info";
        static string cCorrectionError = "Die Korrekturen konnten nicht durchgeführt werden.";
        static string cQuestion = "Frage";
        static string cStartCorrection = "Korrektur starten";
        static string cBisher = "Bisher: ";

        #endregion
        #region Globale Variablen
        
        static string cVorname = "Vorname"; // <- globale Variable!!!
        static string cNachname = "Nachname"; // <- globale Variable!!!
        static string cTitle = "Titel";
        static string cSubTitle = "Untertitel";
        static string cSeries = "Serie";
        static string cNoInSeries = "Nummer in Serie";
        static string cPublishingDate = "Veröffentlichung";
        static string cFormat = "Format";
        static string cISBN13 = "ISBN 13";
        static string cISBN10 = "ISBN 10";
        static string cAuthors = "Autor_innen";
        
        #endregion
        #endregion
        #region Constructor

        public Correction(Form1 form1, List<DBReader.SearchStruct> toCorrectList)
        {
            InitializeComponent();
            mForm1 = form1;
            mToCorrectList = toCorrectList;
            PrepareForCorrection(toCorrectList);
        }

        #endregion
        #region Events

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Close();
            // Prevent from closing
            // e.Cancel = true;
        }

        #endregion
        #region Methods

        private void PrepareForCorrection(List<DBReader.SearchStruct> toCorrectList)
        {
            AddLabelsToTabPage(ref tabPage1, 0);
            AddTextBoxesToTabPage(ref tabPage1, 0, toCorrectList[0]);
            AddOtherControlsToTabPage(ref tabPage1, 0, toCorrectList[0]);
            tabPage1.Text = toCorrectList[0].Title;
            Size tabPageSize = tabPage1.Size;

            if (toCorrectList.Count > 1)
            {
                List<int> bookIDs = new List<int>();
                bookIDs.Add(toCorrectList[0].BookID);
                for (int index = 1; index < toCorrectList.Count; index++)
                {
                    if (!bookIDs.Contains(toCorrectList[index].BookID))
                    {
                        bookIDs.Add(toCorrectList[index].BookID);
                        TabPage myTabPage = new TabPage(toCorrectList[index].Title);
                        myTabPage.Size = tabPageSize;
                        myTabPage.BackColor = Color.White;
                        AddLabelsToTabPage(ref myTabPage, index);
                        AddTextBoxesToTabPage(ref myTabPage, index, toCorrectList[index]);
                        AddOtherControlsToTabPage(ref myTabPage, index, toCorrectList[index]);
                        tabControl1.TabPages.Add(myTabPage);
                    } // if
                } // for
            } // if
        }

        #region Add TextBoxes to TabPage

        private void AddTextBoxesToTabPage(ref TabPage tabPage, int index, DBReader.SearchStruct toCorrect)
        {
            string text = toCorrect.Title;
            Point location = new Point(95, 7);
            string name = $"{cTxtBoxTitle}{index}";
            Size size = new Size(666, 20);
            AddTextBoxToTabPage(text, name, location, size, 1, ref tabPage);
            text = toCorrect.SubTitle;
            location = new Point(95, 33);
            name = $"{cTxtBoxSubTitle}{index}";
            size = new Size(666, 20);
            AddTextBoxToTabPage(text, name, location, size, 1, ref tabPage);
            size = new Size(660, 20);
            text = toCorrect.NoInSeries;
            location = new Point(100, 85);
            name = $"{cTxtBoxNoInSeries}{index}";
            AddTextBoxToTabPage(text, name, location, size, 1, ref tabPage);
            text = toCorrect.PublishingDate;
            location = new Point(100, 111);
            name = $"{cTxtBoxPublishingDate}{index}";
            size = new Size(660, 20);
            AddTextBoxToTabPage(text, name, location, size, 1, ref tabPage);
            text = toCorrect.ISBN13;
            location = new Point(95, 163);
            name = $"{cTxtBoxISBN13}{index}";
            size = new Size(666, 20);
            AddTextBoxToTabPage(text, name, location, size, 1, ref tabPage);
            text = toCorrect.ISBN10;
            location = new Point(95, 189);
            name = $"{cTxtBoxISBN10}{index}";
            size = new Size(666, 20);
            AddTextBoxToTabPage(text, name, location, size, 1, ref tabPage);
        }

        private void AddTextBoxToTabPage(string text, string txtBoxName, Point location, Size txtBoxSize, int tabIndex, ref TabPage tabPage)
        {

            TextBox txtBox = new TextBox()
            {
                Location = location,
                Text = text,
                TabIndex = tabIndex,
                Name = txtBoxName,
                Size = txtBoxSize,
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top
            };

            tabPage.Controls.Add(txtBox);
        }

        #endregion
        #region Add other Controls to TabPage

        private void AddOtherControlsToTabPage(ref TabPage tabPage, int index, DBReader.SearchStruct toCorrect)
        {
            string seriesName = toCorrect.Series;
            Point location = new Point(95, 59);
            string name = $"{cCmbBoxSeries}{index}";
            Size size = new Size(400, 20);
            AddComboBoxToTabPage(seriesName, name, location, size, 1, mCmbBoxEnum.Series, ref tabPage, index, $"{cLabelSeries2}{index}");
            string formatName = toCorrect.Format;
            location = new Point(95, 137);
            name = $"{cCmbBoxFormat}{index}";
            size = new Size(666, 20);
            AddComboBoxToTabPage(formatName, name, location, size, 1, mCmbBoxEnum.Format, ref tabPage, index);
            location = new Point(95, 215);
            size = new Size(666, 60);
            DataGridView dgvAuthors = AddAuthorsDGVToTabPage($"{cDGVArtists}{index}", location, size, ref tabPage);
            FillDGVAuthors(toCorrect, ref dgvAuthors);
            location = new Point(661, 280);
            size = new Size(100, 23);
            AddCorrectButtonToTabPage(location, size, index, ref tabPage);

            SetCmbBoxSelectedIndex(ref tabPage, $"{cCmbBoxSeries}{index}", seriesName);
            SetCmbBoxSelectedIndex(ref tabPage, $"{cCmbBoxFormat}{index}", formatName);
        }

        private DataGridView AddAuthorsDGVToTabPage(string name, Point location, Size size, ref TabPage tabPage)
        {
            DataGridView dataGridView = new DataGridView();
            dataGridView.AllowUserToAddRows = false;
            dataGridView.ColumnHeadersVisible = false;
            dataGridView.RowHeadersVisible = false;
            dataGridView.Location = location;
            dataGridView.Name = name;
            dataGridView.Size = size;
            dataGridView.MinimumSize = size;
            dataGridView.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Top;
            dataGridView.ColumnCount = 2;
            dataGridView.Columns[0].Name = cVorname;
            dataGridView.Columns[1].Name = cNachname;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.ScrollBars = ScrollBars.Both;
            tabPage.Controls.Add(dataGridView);

            return dataGridView;
        }

        private void AddComboBoxToTabPage(string text, string cmbBoxName, Point location, Size size, int tabIndex, mCmbBoxEnum cmbBoxType, ref TabPage tabPage, int tabPageIndex, string additionalLabelName = "")
        {
            ComboBox cmbBox = new ComboBox()
            {
                Location = location,
                TabIndex = tabIndex,
                Name = cmbBoxName,
                Size = size,
                Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left
            };

            cmbBox.Enabled = true;
            switch (cmbBoxType)
            {
                case mCmbBoxEnum.Series:
                    mForm1.FillCmbBoxSeries(cmbBox);
                    int controlIndex = tabPage.Controls.IndexOfKey(additionalLabelName);
                    Label label = (Label)tabPage.Controls[controlIndex];
                    label.Name = $"{cLabelPreviousSeriesName}{tabPageIndex}";
                    label.Text = text == "" ? "" : $"{cBisher}{text}";
                    break;
                case mCmbBoxEnum.Format: mForm1.FillCmbBoxFormat(cmbBox); break;
            }

            tabPage.Controls.Add(cmbBox);
        }

        private void AddCorrectButtonToTabPage(Point location, Size size, int tabIndex, ref TabPage tabPage)
        {
            Button btnThisIsCorrected = new Button()
            {
                Text = cStartCorrection,
                Location = location,
                Name = $"{cBtnTab}{tabIndex}",
                Size = size,
                BackColor = Color.LightGray,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right
            };

            btnThisIsCorrected.Click += BtnThisWillNowBeCorrected_CheckedChanged;
            tabPage.Controls.Add(btnThisIsCorrected);
        }

        #region Underlying Methods

        private void FillDGVAuthors(DBReader.SearchStruct toCorrect, ref DataGridView dgvAuthors)
        {
            DBReader dbReader = new DBReader(mForm1.mDBConnection);
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
                dgvAuthors.Rows.Add(dgvRow);
            } // for
        }

        private void SetCmbBoxSelectedIndex(ref TabPage tabPage, string cmbBoxName, string searchedText)
        {
            int controlIndex = tabPage.Controls.IndexOfKey(cmbBoxName);
            ComboBox cmbBox = (ComboBox)tabPage.Controls[controlIndex];
            int textIndex = -1;
            int timingIndex = 0;
            while (textIndex == -1 && timingIndex < 3)
            {
                textIndex = cmbBox.FindString(searchedText);
                if (textIndex > -1)
                {
                    cmbBox.SelectedIndex = textIndex;
                }

                timingIndex++;
            } // while
        }

        private void BtnThisWillNowBeCorrected_CheckedChanged(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string no = btn.Name.Replace(cBtnTab, "");
            int tabIndex = int.Parse(no);

            TabPage tabPage = (TabPage)tabControl1.GetControl(tabIndex);
            int keyIndex = tabPage.Controls.IndexOfKey($"{cLabelPreviousSeriesName}{tabIndex}");
            string labelText = tabPage.Controls[keyIndex].Text;
            labelText = labelText.Replace($"{cBisher}", "");
            keyIndex = tabPage.Controls.IndexOfKey($"{cCmbBoxSeries}{tabIndex}");
            ComboBox cmbBoxSeries = (ComboBox)tabPage.Controls[keyIndex];
            KeyValuePair<int, string> kvp = (KeyValuePair<int, string>)cmbBoxSeries.Items[cmbBoxSeries.SelectedIndex];
            if (cmbBoxSeries.SelectedIndex == 0 && labelText != "")
            {
                DialogResult dr = MessageBox.Show(cReallyDeleteSeries, cQuestion, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr.Equals(DialogResult.No))
                {
                    return;
                }
            } // if
            else if (kvp.Value != labelText)
            {
                DialogResult dr = MessageBox.Show(cReallyChangSeries, cQuestion, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr.Equals(DialogResult.No))
                {
                    return;
                }
            } // else if

            if (CorrectInformations(tabIndex))
            {
                MessageBox.Show(cCorrectionsSuccessful, cInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(cCorrectionError, cInfo, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        #endregion
        #endregion
        #region Add Labels to TabPage
        private void AddLabelsToTabPage(ref TabPage tabPage, int index)
        {
            string text = $"{cTitle}:";
            Point location = new Point(10, 10);
            string name = $"{cLabelTitle}{index}";
            Size size = new Size(83, 13);
            AddLabelToTabPage(text, name, location, size, ref tabPage);
            text = $"{cSubTitle}:";
            location = new Point(10, 36);
            name = $"{cLabelSubTitle}{index}";
            size = new Size(83, 13);
            AddLabelToTabPage(text, name, location, size, ref tabPage);
            text = $"{cSeries}:";
            location = new Point(10, 62);
            name = $"{cLabelSeries}{index}";
            size = new Size(83, 13);
            AddLabelToTabPage(text, name, location, size, ref tabPage);
            text = "";
            location = new Point(500, 62);
            name = $"{cLabelSeries2}{index}";
            size = new Size(200, 20);
            AddLabelToTabPage(text, name, location, size, ref tabPage);
            text = $"{cNoInSeries}:";
            location = new Point(10, 88);
            name = $"{cLabelNoInSeries}{index}";
            size = new Size(90, 13);
            AddLabelToTabPage(text, name, location, size, ref tabPage);
            text = $"{cPublishingDate}:";
            location = new Point(10, 114);
            name = $"{cLabelPublishingDate}{index}";
            size = new Size(90, 13);
            AddLabelToTabPage(text, name, location, size, ref tabPage);
            text = $"{cFormat}:";
            location = new Point(10, 140);
            name = $"{cLabelFormat}{index}";
            size = new Size(83, 13);
            AddLabelToTabPage(text, name, location, size, ref tabPage);
            text = $"{cISBN13}:";
            location = new Point(10, 166);
            name = $"{cLabelISBN13}{index}";
            size = new Size(83, 13);
            AddLabelToTabPage(text, name, location, size, ref tabPage);
            text = $"{cISBN10}:";
            location = new Point(10, 192);
            name = $"{cLabelISBN10}{index}";
            size = new Size(83, 13);
            AddLabelToTabPage(text, name, location, size, ref tabPage);
            text = $"{cAuthors}:";
            location = new Point(10, 218);
            name = $"{cLabelAuthors}{index}";
            size = new Size(83, 13);
            AddLabelToTabPage(text, name, location, size, ref tabPage);
        }

        private void AddLabelToTabPage(string text, string labelName, Point location, Size labelSize, ref TabPage tabPage)
        {
            Label label = new Label()
            {
                Text = text,
                Location = location,
                Name = labelName,
                Size = labelSize
            };

            tabPage.Controls.Add(label);
        }

        #endregion
        #region Correct

        private bool CorrectInformations(int tabIndex)
        {
            DBReader.SearchStruct toCorrectList = new DBReader.SearchStruct();
            
        //    toCorrectList.BookID = Raussuchen!!!;
        //toCorrectList.Title = "";
        //    toCorrectList.SubTitle = "";
        //    toCorrectList.Series = "";
        //    toCorrectList.NoInSeries = "";
        //    toCorrectList.AuthorPreName = "";
        //    toCorrectList.AuthorSurName = "";
        //    toCorrectList.PublishingDate = "";
        //    toCorrectList.Format = "";
        //    toCorrectList.ISBN13 = "";
        //    toCorrectList.ISBN10 = "";
        //    toCorrectList.IsLent = Raussuchen!!!;
            throw new NotImplementedException();
        }

        #endregion

        private void btnClose_Click(object sender, EventArgs e)
        {
            mForm1.Enabled = true;
            this.Close();
        }

        #endregion
    }
}
