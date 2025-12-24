using System.Windows.Forms;

namespace ISBNCaller_GUI
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            WriteTab_btnOK = new Button();
            WriteTab_btnCancel = new Button();
            tabControl = new TabControl();
            tabPageSearch = new TabPage();
            SearchTab_dataGridViewSearch = new DataGridView();
            SearchTab_groupBoxInput = new GroupBox();
            SearchTab_chkBoxUseDates = new CheckBox();
            SearchTab_cmbBoxSeries = new ComboBox();
            SearchTab_chkBoxOnlyShowFirstAuthor = new CheckBox();
            SearchTab_chkBoxShowLent = new CheckBox();
            SearchTab_dtpTo = new DateTimePicker();
            SearchTab_labelPublishedTo = new Label();
            SearchTab_dtpFrom = new DateTimePicker();
            SearchTab_labelPublishedFrom = new Label();
            SearchTab_labelFormat = new Label();
            SearchTab_cmbBoxFormat = new ComboBox();
            SearchTab_labelSeries = new Label();
            SearchTab_radioBtnWOutSeries = new RadioButton();
            SearchTab_radioBtnWSeries = new RadioButton();
            SearchTab_txtBoxSubTitle = new TextBox();
            SearchTab_labelSubTitle = new Label();
            SearchTab_txtBoxAuthorSurName = new TextBox();
            SearchTab_labelAuthorSurName = new Label();
            SearchTab_labelAuthorPreName = new Label();
            SearchTab_txtBoxAuthorPreName = new TextBox();
            SearchTab_txtBoxTitle = new TextBox();
            SearchTab_labelTitle = new Label();
            SearchTab_txtBoxISBN = new TextBox();
            SearchTab_labelISBN = new Label();
            SearchTab_btnSearch = new Button();
            SearchTab_btnCorrection = new Button();
            tabPageWrite = new TabPage();
            WriteTab_WorkInProgressLabel = new Label();
            WriteTab_txtBoxISBN_1 = new TextBox();
            WriteTab_groupBoxAuthor = new GroupBox();
            WriteTab_Author_dataGridViewAuthor = new DataGridView();
            WriteTab_btnRegisterWOutISBN = new Button();
            WriteTab_groupBoxBook = new GroupBox();
            WriteTab_Book_cmbBoxFormat = new ComboBox();
            WriteTab_Book_btnCalculateISBN13 = new Button();
            WriteTab_Book_btnCalculateISBN10 = new Button();
            WriteTab_Book_labelMaxNoCount = new Label();
            WriteTab_Book_labelMaxNo = new Label();
            WriteTab_Book_txtBoxNoInSeries = new TextBox();
            WriteTab_Book_labelNoInSeries = new Label();
            WriteTab_Book_cmbBoxSeries = new ComboBox();
            WriteTab_Book_txtBoxNewSeriesName = new TextBox();
            WriteTab_Book_chkBoxIsNewSeries = new CheckBox();
            WriteTab_Book_chkBoxIsPartOfSeries = new CheckBox();
            WriteTab_Book_labelISBN13 = new Label();
            WriteTab_Book_txtBoxISBN13 = new TextBox();
            WriteTab_Book_labelISBN10 = new Label();
            WriteTab_Book_txtBoxISBN10 = new TextBox();
            WriteTab_Book_labelFormat = new Label();
            WriteTab_Book_labelPublishingDate = new Label();
            WriteTab_Book_txtBoxPublishingDate = new TextBox();
            WriteTab_Book_labelSubTitle = new Label();
            WriteTab_Book_txtBoxSubTitle = new TextBox();
            WriteTab_Book_txtBoxTitle = new TextBox();
            WriteTab_Book_labelTitle = new Label();
            WriteTab_ISBNLabel_1 = new Label();
            tabPageLent = new TabPage();
            LentTab_groupBoxVerleihen = new GroupBox();
            LentTab_btnRemove = new Button();
            LentTab_btnLent = new Button();
            LentTab_dataGridViewLent = new DataGridView();
            LentTab_groupBoxSuchen = new GroupBox();
            LentTab_cmbBoxSubTitle = new ComboBox();
            LentTab_cmbBoxTitle = new ComboBox();
            LentTab_cmbBoxISBN = new ComboBox();
            LentTab_btnPull = new Button();
            LentTab_btnSearch = new Button();
            LentTab_dataGridViewSearch = new DataGridView();
            LentTab_txtBoxAuthorSurName = new TextBox();
            LentTab_txtBoxAuthorPreName = new TextBox();
            LentTab_txtBoxSubTitle = new TextBox();
            LentTab_txtBoxTitle = new TextBox();
            LentTab_txtBoxISBN = new TextBox();
            LentTab_labelAuthorSurName = new Label();
            LentTab_labelAuthorPreName = new Label();
            LentTab_labelSubTitle = new Label();
            LentTab_labelTitle = new Label();
            LentTab_labelISBN = new Label();
            tabPageReturn = new TabPage();
            ReturnTab_GroupBoxGetBack = new GroupBox();
            ReturnTab_btnReturn = new Button();
            ReturnTab_dataGridViewReturn = new DataGridView();
            ReturnTab_GroupBoxSearch = new GroupBox();
            ReturnTab_txtBoxBookTitle = new TextBox();
            ReturnTab_labelBookTitle = new Label();
            ReturnTab_btnSearch = new Button();
            ReturnTab_txtBoxSurName = new TextBox();
            ReturnTab_txtBoxPreName = new TextBox();
            ReturnTab_txtBoxISBN = new TextBox();
            ReturnTab_labelOptional = new Label();
            ReturnTab_labelSurName = new Label();
            ReturnTab_labelPreName = new Label();
            ReturnTab_labelLentTo = new Label();
            ReturnTab_labelISBN = new Label();
            ReturnTab_chkBoxIgnoreIsActive = new CheckBox();
            ReturnTab_btnShowAll = new Button();
            cmbBoxLanguage = new ComboBox();
            cmbBoxColorMode = new ComboBox();
            tabControl.SuspendLayout();
            tabPageSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)SearchTab_dataGridViewSearch).BeginInit();
            SearchTab_groupBoxInput.SuspendLayout();
            tabPageWrite.SuspendLayout();
            WriteTab_groupBoxAuthor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)WriteTab_Author_dataGridViewAuthor).BeginInit();
            WriteTab_groupBoxBook.SuspendLayout();
            tabPageLent.SuspendLayout();
            LentTab_groupBoxVerleihen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)LentTab_dataGridViewLent).BeginInit();
            LentTab_groupBoxSuchen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)LentTab_dataGridViewSearch).BeginInit();
            tabPageReturn.SuspendLayout();
            ReturnTab_GroupBoxGetBack.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ReturnTab_dataGridViewReturn).BeginInit();
            ReturnTab_GroupBoxSearch.SuspendLayout();
            SuspendLayout();
            // 
            // WriteTab_btnOK
            // 
            WriteTab_btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            WriteTab_btnOK.Location = new System.Drawing.Point(716, 203);
            WriteTab_btnOK.Margin = new Padding(4, 3, 4, 3);
            WriteTab_btnOK.Name = "WriteTab_btnOK";
            WriteTab_btnOK.Size = new System.Drawing.Size(88, 27);
            WriteTab_btnOK.TabIndex = 17;
            WriteTab_btnOK.Text = "Suchen";
            WriteTab_btnOK.UseVisualStyleBackColor = true;
            WriteTab_btnOK.Click += WriteTab_btnOK_Click;
            // 
            // WriteTab_btnCancel
            // 
            WriteTab_btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            WriteTab_btnCancel.Enabled = false;
            WriteTab_btnCancel.Location = new System.Drawing.Point(811, 203);
            WriteTab_btnCancel.Margin = new Padding(4, 3, 4, 3);
            WriteTab_btnCancel.Name = "WriteTab_btnCancel";
            WriteTab_btnCancel.Size = new System.Drawing.Size(88, 27);
            WriteTab_btnCancel.TabIndex = 18;
            WriteTab_btnCancel.Text = "Abbrechen";
            WriteTab_btnCancel.UseVisualStyleBackColor = true;
            WriteTab_btnCancel.Click += WriteTab_btnCancel_Click;
            // 
            // tabControl
            // 
            tabControl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControl.Controls.Add(tabPageSearch);
            tabControl.Controls.Add(tabPageWrite);
            tabControl.Controls.Add(tabPageLent);
            tabControl.Controls.Add(tabPageReturn);
            tabControl.Location = new System.Drawing.Point(1, -1);
            tabControl.Margin = new Padding(4, 3, 4, 3);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new System.Drawing.Size(933, 516);
            tabControl.TabIndex = 0;
            tabControl.SelectedIndexChanged += TabControl_SelectedIndexChanged;
            // 
            // tabPageSearch
            // 
            tabPageSearch.Controls.Add(SearchTab_dataGridViewSearch);
            tabPageSearch.Controls.Add(SearchTab_groupBoxInput);
            tabPageSearch.Controls.Add(SearchTab_btnSearch);
            tabPageSearch.Controls.Add(SearchTab_btnCorrection);
            tabPageSearch.Location = new System.Drawing.Point(4, 24);
            tabPageSearch.Margin = new Padding(4, 3, 4, 3);
            tabPageSearch.Name = "tabPageSearch";
            tabPageSearch.Size = new System.Drawing.Size(925, 488);
            tabPageSearch.TabIndex = 2;
            tabPageSearch.Text = "Suchen";
            tabPageSearch.UseVisualStyleBackColor = true;
            // 
            // SearchTab_dataGridViewSearch
            // 
            SearchTab_dataGridViewSearch.AllowUserToAddRows = false;
            SearchTab_dataGridViewSearch.AllowUserToDeleteRows = false;
            SearchTab_dataGridViewSearch.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            SearchTab_dataGridViewSearch.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            SearchTab_dataGridViewSearch.Location = new System.Drawing.Point(12, 168);
            SearchTab_dataGridViewSearch.Margin = new Padding(4, 3, 4, 3);
            SearchTab_dataGridViewSearch.Name = "SearchTab_dataGridViewSearch";
            SearchTab_dataGridViewSearch.ReadOnly = true;
            SearchTab_dataGridViewSearch.Size = new System.Drawing.Size(898, 268);
            SearchTab_dataGridViewSearch.TabIndex = 0;
            // 
            // SearchTab_groupBoxInput
            // 
            SearchTab_groupBoxInput.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            SearchTab_groupBoxInput.Controls.Add(SearchTab_chkBoxUseDates);
            SearchTab_groupBoxInput.Controls.Add(SearchTab_cmbBoxSeries);
            SearchTab_groupBoxInput.Controls.Add(SearchTab_chkBoxOnlyShowFirstAuthor);
            SearchTab_groupBoxInput.Controls.Add(SearchTab_chkBoxShowLent);
            SearchTab_groupBoxInput.Controls.Add(SearchTab_dtpTo);
            SearchTab_groupBoxInput.Controls.Add(SearchTab_labelPublishedTo);
            SearchTab_groupBoxInput.Controls.Add(SearchTab_dtpFrom);
            SearchTab_groupBoxInput.Controls.Add(SearchTab_labelPublishedFrom);
            SearchTab_groupBoxInput.Controls.Add(SearchTab_labelFormat);
            SearchTab_groupBoxInput.Controls.Add(SearchTab_cmbBoxFormat);
            SearchTab_groupBoxInput.Controls.Add(SearchTab_labelSeries);
            SearchTab_groupBoxInput.Controls.Add(SearchTab_radioBtnWOutSeries);
            SearchTab_groupBoxInput.Controls.Add(SearchTab_radioBtnWSeries);
            SearchTab_groupBoxInput.Controls.Add(SearchTab_txtBoxSubTitle);
            SearchTab_groupBoxInput.Controls.Add(SearchTab_labelSubTitle);
            SearchTab_groupBoxInput.Controls.Add(SearchTab_txtBoxAuthorSurName);
            SearchTab_groupBoxInput.Controls.Add(SearchTab_labelAuthorSurName);
            SearchTab_groupBoxInput.Controls.Add(SearchTab_labelAuthorPreName);
            SearchTab_groupBoxInput.Controls.Add(SearchTab_txtBoxAuthorPreName);
            SearchTab_groupBoxInput.Controls.Add(SearchTab_txtBoxTitle);
            SearchTab_groupBoxInput.Controls.Add(SearchTab_labelTitle);
            SearchTab_groupBoxInput.Controls.Add(SearchTab_txtBoxISBN);
            SearchTab_groupBoxInput.Controls.Add(SearchTab_labelISBN);
            SearchTab_groupBoxInput.Location = new System.Drawing.Point(12, 3);
            SearchTab_groupBoxInput.Margin = new Padding(4, 3, 4, 3);
            SearchTab_groupBoxInput.Name = "SearchTab_groupBoxInput";
            SearchTab_groupBoxInput.Padding = new Padding(4, 3, 4, 3);
            SearchTab_groupBoxInput.Size = new System.Drawing.Size(898, 158);
            SearchTab_groupBoxInput.TabIndex = 1;
            SearchTab_groupBoxInput.TabStop = false;
            // 
            // SearchTab_chkBoxUseDates
            // 
            SearchTab_chkBoxUseDates.AutoSize = true;
            SearchTab_chkBoxUseDates.Location = new System.Drawing.Point(758, 102);
            SearchTab_chkBoxUseDates.Name = "SearchTab_chkBoxUseDates";
            SearchTab_chkBoxUseDates.Size = new System.Drawing.Size(96, 19);
            SearchTab_chkBoxUseDates.TabIndex = 23;
            SearchTab_chkBoxUseDates.Text = "Daten nutzen";
            SearchTab_chkBoxUseDates.UseVisualStyleBackColor = true;
            SearchTab_chkBoxUseDates.CheckedChanged += SearchTab_chkBoxUseDates_CheckedChanged;
            // 
            // SearchTab_cmbBoxSeries
            // 
            SearchTab_cmbBoxSeries.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            SearchTab_cmbBoxSeries.FormattingEnabled = true;
            SearchTab_cmbBoxSeries.Location = new System.Drawing.Point(434, 40);
            SearchTab_cmbBoxSeries.Margin = new Padding(4, 3, 4, 3);
            SearchTab_cmbBoxSeries.Name = "SearchTab_cmbBoxSeries";
            SearchTab_cmbBoxSeries.Size = new System.Drawing.Size(317, 23);
            SearchTab_cmbBoxSeries.TabIndex = 22;
            // 
            // SearchTab_chkBoxOnlyShowFirstAuthor
            // 
            SearchTab_chkBoxOnlyShowFirstAuthor.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            SearchTab_chkBoxOnlyShowFirstAuthor.AutoSize = true;
            SearchTab_chkBoxOnlyShowFirstAuthor.Checked = true;
            SearchTab_chkBoxOnlyShowFirstAuthor.CheckState = CheckState.Checked;
            SearchTab_chkBoxOnlyShowFirstAuthor.Location = new System.Drawing.Point(556, 133);
            SearchTab_chkBoxOnlyShowFirstAuthor.Margin = new Padding(4, 3, 4, 3);
            SearchTab_chkBoxOnlyShowFirstAuthor.Name = "SearchTab_chkBoxOnlyShowFirstAuthor";
            SearchTab_chkBoxOnlyShowFirstAuthor.Size = new System.Drawing.Size(184, 19);
            SearchTab_chkBoxOnlyShowFirstAuthor.TabIndex = 21;
            SearchTab_chkBoxOnlyShowFirstAuthor.Text = "Nur erste_n Autor_in anzeigen";
            SearchTab_chkBoxOnlyShowFirstAuthor.UseVisualStyleBackColor = true;
            // 
            // SearchTab_chkBoxShowLent
            // 
            SearchTab_chkBoxShowLent.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            SearchTab_chkBoxShowLent.AutoSize = true;
            SearchTab_chkBoxShowLent.Checked = true;
            SearchTab_chkBoxShowLent.CheckState = CheckState.Checked;
            SearchTab_chkBoxShowLent.Location = new System.Drawing.Point(391, 133);
            SearchTab_chkBoxShowLent.Margin = new Padding(4, 3, 4, 3);
            SearchTab_chkBoxShowLent.Name = "SearchTab_chkBoxShowLent";
            SearchTab_chkBoxShowLent.Size = new System.Drawing.Size(151, 19);
            SearchTab_chkBoxShowLent.TabIndex = 20;
            SearchTab_chkBoxShowLent.Text = "Verliehene mit anzeigen";
            SearchTab_chkBoxShowLent.UseVisualStyleBackColor = true;
            // 
            // SearchTab_dtpTo
            // 
            SearchTab_dtpTo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            SearchTab_dtpTo.CustomFormat = "yyyy";
            SearchTab_dtpTo.Format = DateTimePickerFormat.Custom;
            SearchTab_dtpTo.Location = new System.Drawing.Point(643, 100);
            SearchTab_dtpTo.Margin = new Padding(4, 3, 4, 3);
            SearchTab_dtpTo.Name = "SearchTab_dtpTo";
            SearchTab_dtpTo.ShowUpDown = true;
            SearchTab_dtpTo.Size = new System.Drawing.Size(108, 23);
            SearchTab_dtpTo.TabIndex = 19;
            SearchTab_dtpTo.Enabled = false;
            // 
            // SearchTab_labelPublishedTo
            // 
            SearchTab_labelPublishedTo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            SearchTab_labelPublishedTo.AutoSize = true;
            SearchTab_labelPublishedTo.Location = new System.Drawing.Point(609, 104);
            SearchTab_labelPublishedTo.Margin = new Padding(4, 0, 4, 0);
            SearchTab_labelPublishedTo.Name = "SearchTab_labelPublishedTo";
            SearchTab_labelPublishedTo.Size = new System.Drawing.Size(25, 15);
            SearchTab_labelPublishedTo.TabIndex = 18;
            SearchTab_labelPublishedTo.Text = "bis:";
            // 
            // SearchTab_dtpFrom
            // 
            SearchTab_dtpFrom.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            SearchTab_dtpFrom.CustomFormat = "yyyy";
            SearchTab_dtpFrom.Format = DateTimePickerFormat.Custom;
            SearchTab_dtpFrom.Location = new System.Drawing.Point(493, 100);
            SearchTab_dtpFrom.Margin = new Padding(4, 3, 4, 3);
            SearchTab_dtpFrom.Name = "SearchTab_dtpFrom";
            SearchTab_dtpFrom.ShowUpDown = true;
            SearchTab_dtpFrom.Size = new System.Drawing.Size(108, 23);
            SearchTab_dtpFrom.TabIndex = 17;
            SearchTab_dtpFrom.Enabled = false;
            // 
            // SearchTab_labelPublishedFrom
            // 
            SearchTab_labelPublishedFrom.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            SearchTab_labelPublishedFrom.AutoSize = true;
            SearchTab_labelPublishedFrom.Location = new System.Drawing.Point(378, 104);
            SearchTab_labelPublishedFrom.Margin = new Padding(4, 0, 4, 0);
            SearchTab_labelPublishedFrom.Name = "SearchTab_labelPublishedFrom";
            SearchTab_labelPublishedFrom.Size = new System.Drawing.Size(104, 15);
            SearchTab_labelPublishedFrom.TabIndex = 16;
            SearchTab_labelPublishedFrom.Text = "Veröffentlicht von:";
            // 
            // SearchTab_labelFormat
            // 
            SearchTab_labelFormat.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            SearchTab_labelFormat.AutoSize = true;
            SearchTab_labelFormat.Location = new System.Drawing.Point(378, 74);
            SearchTab_labelFormat.Margin = new Padding(4, 0, 4, 0);
            SearchTab_labelFormat.Name = "SearchTab_labelFormat";
            SearchTab_labelFormat.Size = new System.Drawing.Size(48, 15);
            SearchTab_labelFormat.TabIndex = 14;
            SearchTab_labelFormat.Text = "Format:";
            // 
            // SearchTab_cmbBoxFormat
            // 
            SearchTab_cmbBoxFormat.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            SearchTab_cmbBoxFormat.FormattingEnabled = true;
            SearchTab_cmbBoxFormat.Location = new System.Drawing.Point(434, 70);
            SearchTab_cmbBoxFormat.Margin = new Padding(4, 3, 4, 3);
            SearchTab_cmbBoxFormat.Name = "SearchTab_cmbBoxFormat";
            SearchTab_cmbBoxFormat.Size = new System.Drawing.Size(317, 23);
            SearchTab_cmbBoxFormat.TabIndex = 15;
            // 
            // SearchTab_labelSeries
            // 
            SearchTab_labelSeries.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            SearchTab_labelSeries.AutoSize = true;
            SearchTab_labelSeries.Location = new System.Drawing.Point(378, 44);
            SearchTab_labelSeries.Margin = new Padding(4, 0, 4, 0);
            SearchTab_labelSeries.Name = "SearchTab_labelSeries";
            SearchTab_labelSeries.Size = new System.Drawing.Size(35, 15);
            SearchTab_labelSeries.TabIndex = 12;
            SearchTab_labelSeries.Text = "Serie:";
            // 
            // SearchTab_radioBtnWOutSeries
            // 
            SearchTab_radioBtnWOutSeries.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            SearchTab_radioBtnWOutSeries.AutoSize = true;
            SearchTab_radioBtnWOutSeries.Location = new System.Drawing.Point(482, 12);
            SearchTab_radioBtnWOutSeries.Margin = new Padding(4, 3, 4, 3);
            SearchTab_radioBtnWOutSeries.Name = "SearchTab_radioBtnWOutSeries";
            SearchTab_radioBtnWOutSeries.Size = new System.Drawing.Size(89, 19);
            SearchTab_radioBtnWOutSeries.TabIndex = 11;
            SearchTab_radioBtnWOutSeries.Text = "Ohne Serien";
            SearchTab_radioBtnWOutSeries.UseVisualStyleBackColor = true;
            // 
            // SearchTab_radioBtnWSeries
            // 
            SearchTab_radioBtnWSeries.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            SearchTab_radioBtnWSeries.AutoSize = true;
            SearchTab_radioBtnWSeries.Checked = true;
            SearchTab_radioBtnWSeries.Location = new System.Drawing.Point(387, 12);
            SearchTab_radioBtnWSeries.Margin = new Padding(4, 3, 4, 3);
            SearchTab_radioBtnWSeries.Name = "SearchTab_radioBtnWSeries";
            SearchTab_radioBtnWSeries.Size = new System.Drawing.Size(78, 19);
            SearchTab_radioBtnWSeries.TabIndex = 10;
            SearchTab_radioBtnWSeries.TabStop = true;
            SearchTab_radioBtnWSeries.Text = "Mit Serien";
            SearchTab_radioBtnWSeries.UseVisualStyleBackColor = true;
            // 
            // SearchTab_txtBoxSubTitle
            // 
            SearchTab_txtBoxSubTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            SearchTab_txtBoxSubTitle.Location = new System.Drawing.Point(135, 70);
            SearchTab_txtBoxSubTitle.Margin = new Padding(4, 3, 4, 3);
            SearchTab_txtBoxSubTitle.Name = "SearchTab_txtBoxSubTitle";
            SearchTab_txtBoxSubTitle.Size = new System.Drawing.Size(214, 23);
            SearchTab_txtBoxSubTitle.TabIndex = 5;
            // 
            // SearchTab_labelSubTitle
            // 
            SearchTab_labelSubTitle.AutoSize = true;
            SearchTab_labelSubTitle.Location = new System.Drawing.Point(7, 74);
            SearchTab_labelSubTitle.Margin = new Padding(4, 0, 4, 0);
            SearchTab_labelSubTitle.Name = "SearchTab_labelSubTitle";
            SearchTab_labelSubTitle.Size = new System.Drawing.Size(59, 15);
            SearchTab_labelSubTitle.TabIndex = 4;
            SearchTab_labelSubTitle.Text = "Untertitel:";
            // 
            // SearchTab_txtBoxAuthorSurName
            // 
            SearchTab_txtBoxAuthorSurName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            SearchTab_txtBoxAuthorSurName.Location = new System.Drawing.Point(135, 130);
            SearchTab_txtBoxAuthorSurName.Margin = new Padding(4, 3, 4, 3);
            SearchTab_txtBoxAuthorSurName.Name = "SearchTab_txtBoxAuthorSurName";
            SearchTab_txtBoxAuthorSurName.Size = new System.Drawing.Size(214, 23);
            SearchTab_txtBoxAuthorSurName.TabIndex = 9;
            // 
            // SearchTab_labelAuthorSurName
            // 
            SearchTab_labelAuthorSurName.AutoSize = true;
            SearchTab_labelAuthorSurName.Location = new System.Drawing.Point(7, 134);
            SearchTab_labelAuthorSurName.Margin = new Padding(4, 0, 4, 0);
            SearchTab_labelAuthorSurName.Name = "SearchTab_labelAuthorSurName";
            SearchTab_labelAuthorSurName.Size = new System.Drawing.Size(116, 15);
            SearchTab_labelAuthorSurName.TabIndex = 8;
            SearchTab_labelAuthorSurName.Text = "Autor_in Nachname:";
            // 
            // SearchTab_labelAuthorPreName
            // 
            SearchTab_labelAuthorPreName.AutoSize = true;
            SearchTab_labelAuthorPreName.Location = new System.Drawing.Point(7, 104);
            SearchTab_labelAuthorPreName.Margin = new Padding(4, 0, 4, 0);
            SearchTab_labelAuthorPreName.Name = "SearchTab_labelAuthorPreName";
            SearchTab_labelAuthorPreName.Size = new System.Drawing.Size(105, 15);
            SearchTab_labelAuthorPreName.TabIndex = 6;
            SearchTab_labelAuthorPreName.Text = "Autor_in Vorname:";
            // 
            // SearchTab_txtBoxAuthorPreName
            // 
            SearchTab_txtBoxAuthorPreName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            SearchTab_txtBoxAuthorPreName.Location = new System.Drawing.Point(135, 100);
            SearchTab_txtBoxAuthorPreName.Margin = new Padding(4, 3, 4, 3);
            SearchTab_txtBoxAuthorPreName.Name = "SearchTab_txtBoxAuthorPreName";
            SearchTab_txtBoxAuthorPreName.Size = new System.Drawing.Size(214, 23);
            SearchTab_txtBoxAuthorPreName.TabIndex = 7;
            // 
            // SearchTab_txtBoxTitle
            // 
            SearchTab_txtBoxTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            SearchTab_txtBoxTitle.Location = new System.Drawing.Point(135, 40);
            SearchTab_txtBoxTitle.Margin = new Padding(4, 3, 4, 3);
            SearchTab_txtBoxTitle.Name = "SearchTab_txtBoxTitle";
            SearchTab_txtBoxTitle.Size = new System.Drawing.Size(214, 23);
            SearchTab_txtBoxTitle.TabIndex = 3;
            // 
            // SearchTab_labelTitle
            // 
            SearchTab_labelTitle.AutoSize = true;
            SearchTab_labelTitle.Location = new System.Drawing.Point(7, 44);
            SearchTab_labelTitle.Margin = new Padding(4, 0, 4, 0);
            SearchTab_labelTitle.Name = "SearchTab_labelTitle";
            SearchTab_labelTitle.Size = new System.Drawing.Size(32, 15);
            SearchTab_labelTitle.TabIndex = 2;
            SearchTab_labelTitle.Text = "Titel:";
            // 
            // SearchTab_txtBoxISBN
            // 
            SearchTab_txtBoxISBN.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            SearchTab_txtBoxISBN.Location = new System.Drawing.Point(135, 10);
            SearchTab_txtBoxISBN.Margin = new Padding(4, 3, 4, 3);
            SearchTab_txtBoxISBN.Name = "SearchTab_txtBoxISBN";
            SearchTab_txtBoxISBN.Size = new System.Drawing.Size(214, 23);
            SearchTab_txtBoxISBN.TabIndex = 1;
            // 
            // SearchTab_labelISBN
            // 
            SearchTab_labelISBN.AutoSize = true;
            SearchTab_labelISBN.Location = new System.Drawing.Point(7, 14);
            SearchTab_labelISBN.Margin = new Padding(4, 0, 4, 0);
            SearchTab_labelISBN.Name = "SearchTab_labelISBN";
            SearchTab_labelISBN.Size = new System.Drawing.Size(35, 15);
            SearchTab_labelISBN.TabIndex = 0;
            SearchTab_labelISBN.Text = "ISBN:";
            // 
            // SearchTab_btnSearch
            // 
            SearchTab_btnSearch.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            SearchTab_btnSearch.Location = new System.Drawing.Point(826, 455);
            SearchTab_btnSearch.Margin = new Padding(4, 3, 4, 3);
            SearchTab_btnSearch.Name = "SearchTab_btnSearch";
            SearchTab_btnSearch.Size = new System.Drawing.Size(88, 27);
            SearchTab_btnSearch.TabIndex = 0;
            SearchTab_btnSearch.Text = "Suchen";
            SearchTab_btnSearch.UseVisualStyleBackColor = true;
            SearchTab_btnSearch.Click += SearchTab_btnSearch_Click;
            // 
            // SearchTab_btnCorrection
            // 
            SearchTab_btnCorrection.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            SearchTab_btnCorrection.Enabled = false;
            SearchTab_btnCorrection.Location = new System.Drawing.Point(732, 455);
            SearchTab_btnCorrection.Margin = new Padding(4, 3, 4, 3);
            SearchTab_btnCorrection.Name = "SearchTab_btnCorrection";
            SearchTab_btnCorrection.Size = new System.Drawing.Size(88, 27);
            SearchTab_btnCorrection.TabIndex = 0;
            SearchTab_btnCorrection.Text = "Korrigieren";
            SearchTab_btnCorrection.UseVisualStyleBackColor = true;
            SearchTab_btnCorrection.Click += SearchTab_btnCorrection_Click;
            // 
            // tabPageWrite
            // 
            tabPageWrite.Controls.Add(WriteTab_WorkInProgressLabel);
            tabPageWrite.Controls.Add(WriteTab_txtBoxISBN_1);
            tabPageWrite.Controls.Add(WriteTab_groupBoxAuthor);
            tabPageWrite.Controls.Add(WriteTab_btnRegisterWOutISBN);
            tabPageWrite.Controls.Add(WriteTab_groupBoxBook);
            tabPageWrite.Controls.Add(WriteTab_ISBNLabel_1);
            tabPageWrite.Location = new System.Drawing.Point(4, 24);
            tabPageWrite.Margin = new Padding(4, 3, 4, 3);
            tabPageWrite.Name = "tabPageWrite";
            tabPageWrite.Size = new System.Drawing.Size(925, 488);
            tabPageWrite.TabIndex = 3;
            tabPageWrite.Text = "Eintragen";
            tabPageWrite.UseVisualStyleBackColor = true;
            // 
            // WriteTab_WorkInProgressLabel
            // 
            WriteTab_WorkInProgressLabel.AutoSize = true;
            WriteTab_WorkInProgressLabel.Location = new System.Drawing.Point(330, 8);
            WriteTab_WorkInProgressLabel.Margin = new Padding(4, 0, 4, 0);
            WriteTab_WorkInProgressLabel.Name = "WriteTab_WorkInProgressLabel";
            WriteTab_WorkInProgressLabel.Size = new System.Drawing.Size(105, 15);
            WriteTab_WorkInProgressLabel.TabIndex = 6;
            WriteTab_WorkInProgressLabel.Text = "Work in progress...";
            // 
            // WriteTab_txtBoxISBN_1
            // 
            WriteTab_txtBoxISBN_1.Location = new System.Drawing.Point(61, 5);
            WriteTab_txtBoxISBN_1.Margin = new Padding(4, 3, 4, 3);
            WriteTab_txtBoxISBN_1.Name = "WriteTab_txtBoxISBN_1";
            WriteTab_txtBoxISBN_1.Size = new System.Drawing.Size(116, 23);
            WriteTab_txtBoxISBN_1.TabIndex = 1;
            WriteTab_txtBoxISBN_1.TextChanged += WriteTab_txtBoxISBN_1_TextChanged;
            // 
            // WriteTab_groupBoxAuthor
            // 
            WriteTab_groupBoxAuthor.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            WriteTab_groupBoxAuthor.Controls.Add(WriteTab_Author_dataGridViewAuthor);
            WriteTab_groupBoxAuthor.Controls.Add(WriteTab_btnOK);
            WriteTab_groupBoxAuthor.Controls.Add(WriteTab_btnCancel);
            WriteTab_groupBoxAuthor.Location = new System.Drawing.Point(6, 245);
            WriteTab_groupBoxAuthor.Margin = new Padding(4, 3, 4, 3);
            WriteTab_groupBoxAuthor.Name = "WriteTab_groupBoxAuthor";
            WriteTab_groupBoxAuthor.Padding = new Padding(4, 3, 4, 3);
            WriteTab_groupBoxAuthor.Size = new System.Drawing.Size(912, 237);
            WriteTab_groupBoxAuthor.TabIndex = 5;
            WriteTab_groupBoxAuthor.TabStop = false;
            WriteTab_groupBoxAuthor.Text = "Autor_in";
            // 
            // WriteTab_Author_dataGridViewAuthor
            // 
            WriteTab_Author_dataGridViewAuthor.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            WriteTab_Author_dataGridViewAuthor.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
            WriteTab_Author_dataGridViewAuthor.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            WriteTab_Author_dataGridViewAuthor.Location = new System.Drawing.Point(7, 22);
            WriteTab_Author_dataGridViewAuthor.Margin = new Padding(4, 3, 4, 3);
            WriteTab_Author_dataGridViewAuthor.MultiSelect = false;
            WriteTab_Author_dataGridViewAuthor.Name = "WriteTab_Author_dataGridViewAuthor";
            WriteTab_Author_dataGridViewAuthor.Size = new System.Drawing.Size(891, 174);
            WriteTab_Author_dataGridViewAuthor.TabIndex = 16;
            WriteTab_Author_dataGridViewAuthor.RowsAdded += WriteTab_Author_dataGridViewAuthor_RowAdded;
            // 
            // WriteTab_btnRegisterWOutISBN
            // 
            WriteTab_btnRegisterWOutISBN.Location = new System.Drawing.Point(184, 2);
            WriteTab_btnRegisterWOutISBN.Margin = new Padding(4, 3, 4, 3);
            WriteTab_btnRegisterWOutISBN.Name = "WriteTab_btnRegisterWOutISBN";
            WriteTab_btnRegisterWOutISBN.Size = new System.Drawing.Size(139, 27);
            WriteTab_btnRegisterWOutISBN.TabIndex = 2;
            WriteTab_btnRegisterWOutISBN.Text = "Eintragen ohne ISBN";
            WriteTab_btnRegisterWOutISBN.UseVisualStyleBackColor = true;
            WriteTab_btnRegisterWOutISBN.Click += WriteTab_btnRegisterWOutISBN_Click;
            // 
            // WriteTab_groupBoxBook
            // 
            WriteTab_groupBoxBook.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            WriteTab_groupBoxBook.Controls.Add(WriteTab_Book_cmbBoxFormat);
            WriteTab_groupBoxBook.Controls.Add(WriteTab_Book_btnCalculateISBN13);
            WriteTab_groupBoxBook.Controls.Add(WriteTab_Book_btnCalculateISBN10);
            WriteTab_groupBoxBook.Controls.Add(WriteTab_Book_labelMaxNoCount);
            WriteTab_groupBoxBook.Controls.Add(WriteTab_Book_labelMaxNo);
            WriteTab_groupBoxBook.Controls.Add(WriteTab_Book_txtBoxNoInSeries);
            WriteTab_groupBoxBook.Controls.Add(WriteTab_Book_labelNoInSeries);
            WriteTab_groupBoxBook.Controls.Add(WriteTab_Book_cmbBoxSeries);
            WriteTab_groupBoxBook.Controls.Add(WriteTab_Book_txtBoxNewSeriesName);
            WriteTab_groupBoxBook.Controls.Add(WriteTab_Book_chkBoxIsNewSeries);
            WriteTab_groupBoxBook.Controls.Add(WriteTab_Book_chkBoxIsPartOfSeries);
            WriteTab_groupBoxBook.Controls.Add(WriteTab_Book_labelISBN13);
            WriteTab_groupBoxBook.Controls.Add(WriteTab_Book_txtBoxISBN13);
            WriteTab_groupBoxBook.Controls.Add(WriteTab_Book_labelISBN10);
            WriteTab_groupBoxBook.Controls.Add(WriteTab_Book_txtBoxISBN10);
            WriteTab_groupBoxBook.Controls.Add(WriteTab_Book_labelFormat);
            WriteTab_groupBoxBook.Controls.Add(WriteTab_Book_labelPublishingDate);
            WriteTab_groupBoxBook.Controls.Add(WriteTab_Book_txtBoxPublishingDate);
            WriteTab_groupBoxBook.Controls.Add(WriteTab_Book_labelSubTitle);
            WriteTab_groupBoxBook.Controls.Add(WriteTab_Book_txtBoxSubTitle);
            WriteTab_groupBoxBook.Controls.Add(WriteTab_Book_txtBoxTitle);
            WriteTab_groupBoxBook.Controls.Add(WriteTab_Book_labelTitle);
            WriteTab_groupBoxBook.Location = new System.Drawing.Point(6, 31);
            WriteTab_groupBoxBook.Margin = new Padding(4, 3, 4, 3);
            WriteTab_groupBoxBook.Name = "WriteTab_groupBoxBook";
            WriteTab_groupBoxBook.Padding = new Padding(4, 3, 4, 3);
            WriteTab_groupBoxBook.Size = new System.Drawing.Size(912, 210);
            WriteTab_groupBoxBook.TabIndex = 4;
            WriteTab_groupBoxBook.TabStop = false;
            WriteTab_groupBoxBook.Text = "Buch";
            // 
            // WriteTab_Book_cmbBoxFormat
            // 
            WriteTab_Book_cmbBoxFormat.FormattingEnabled = true;
            WriteTab_Book_cmbBoxFormat.Location = new System.Drawing.Point(98, 112);
            WriteTab_Book_cmbBoxFormat.Margin = new Padding(4, 3, 4, 3);
            WriteTab_Book_cmbBoxFormat.Name = "WriteTab_Book_cmbBoxFormat";
            WriteTab_Book_cmbBoxFormat.Size = new System.Drawing.Size(258, 23);
            WriteTab_Book_cmbBoxFormat.TabIndex = 6;
            WriteTab_Book_cmbBoxFormat.EnabledChanged += WriteTab_Book_cmdBoxFormat_EnabledChanged;
            // 
            // WriteTab_Book_btnCalculateISBN13
            // 
            WriteTab_Book_btnCalculateISBN13.Location = new System.Drawing.Point(270, 170);
            WriteTab_Book_btnCalculateISBN13.Margin = new Padding(4, 3, 4, 3);
            WriteTab_Book_btnCalculateISBN13.Name = "WriteTab_Book_btnCalculateISBN13";
            WriteTab_Book_btnCalculateISBN13.Size = new System.Drawing.Size(88, 27);
            WriteTab_Book_btnCalculateISBN13.TabIndex = 10;
            WriteTab_Book_btnCalculateISBN13.Text = "Berechnen";
            WriteTab_Book_btnCalculateISBN13.UseVisualStyleBackColor = true;
            WriteTab_Book_btnCalculateISBN13.Click += WriteTab_Book_btnCalculateISBN13_Click;
            // 
            // WriteTab_Book_btnCalculateISBN10
            // 
            WriteTab_Book_btnCalculateISBN10.Location = new System.Drawing.Point(270, 140);
            WriteTab_Book_btnCalculateISBN10.Margin = new Padding(4, 3, 4, 3);
            WriteTab_Book_btnCalculateISBN10.Name = "WriteTab_Book_btnCalculateISBN10";
            WriteTab_Book_btnCalculateISBN10.Size = new System.Drawing.Size(88, 27);
            WriteTab_Book_btnCalculateISBN10.TabIndex = 9;
            WriteTab_Book_btnCalculateISBN10.Text = "Berechnen";
            WriteTab_Book_btnCalculateISBN10.UseVisualStyleBackColor = true;
            WriteTab_Book_btnCalculateISBN10.Click += WriteTab_Book_btnCalculateISBN10_Click;
            // 
            // WriteTab_Book_labelMaxNoCount
            // 
            WriteTab_Book_labelMaxNoCount.AutoSize = true;
            WriteTab_Book_labelMaxNoCount.Location = new System.Drawing.Point(785, 85);
            WriteTab_Book_labelMaxNoCount.Margin = new Padding(4, 0, 4, 0);
            WriteTab_Book_labelMaxNoCount.Name = "WriteTab_Book_labelMaxNoCount";
            WriteTab_Book_labelMaxNoCount.Size = new System.Drawing.Size(0, 15);
            WriteTab_Book_labelMaxNoCount.TabIndex = 18;
            // 
            // WriteTab_Book_labelMaxNo
            // 
            WriteTab_Book_labelMaxNo.AutoSize = true;
            WriteTab_Book_labelMaxNo.Location = new System.Drawing.Point(720, 85);
            WriteTab_Book_labelMaxNo.Margin = new Padding(4, 0, 4, 0);
            WriteTab_Book_labelMaxNo.Name = "WriteTab_Book_labelMaxNo";
            WriteTab_Book_labelMaxNo.Size = new System.Drawing.Size(55, 15);
            WriteTab_Book_labelMaxNo.TabIndex = 17;
            WriteTab_Book_labelMaxNo.Text = "Max No.:";
            // 
            // WriteTab_Book_txtBoxNoInSeries
            // 
            WriteTab_Book_txtBoxNoInSeries.Location = new System.Drawing.Point(596, 82);
            WriteTab_Book_txtBoxNoInSeries.Margin = new Padding(4, 3, 4, 3);
            WriteTab_Book_txtBoxNoInSeries.Name = "WriteTab_Book_txtBoxNoInSeries";
            WriteTab_Book_txtBoxNoInSeries.Size = new System.Drawing.Size(116, 23);
            WriteTab_Book_txtBoxNoInSeries.TabIndex = 13;
            // 
            // WriteTab_Book_labelNoInSeries
            // 
            WriteTab_Book_labelNoInSeries.AutoSize = true;
            WriteTab_Book_labelNoInSeries.Location = new System.Drawing.Point(488, 85);
            WriteTab_Book_labelNoInSeries.Margin = new Padding(4, 0, 4, 0);
            WriteTab_Book_labelNoInSeries.Name = "WriteTab_Book_labelNoInSeries";
            WriteTab_Book_labelNoInSeries.Size = new System.Drawing.Size(99, 15);
            WriteTab_Book_labelNoInSeries.TabIndex = 16;
            WriteTab_Book_labelNoInSeries.Text = "Nummer in Serie:";
            // 
            // WriteTab_Book_cmbBoxSeries
            // 
            WriteTab_Book_cmbBoxSeries.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            WriteTab_Book_cmbBoxSeries.DropDownStyle = ComboBoxStyle.DropDownList;
            WriteTab_Book_cmbBoxSeries.FormattingEnabled = true;
            WriteTab_Book_cmbBoxSeries.Location = new System.Drawing.Point(365, 112);
            WriteTab_Book_cmbBoxSeries.Margin = new Padding(4, 3, 4, 3);
            WriteTab_Book_cmbBoxSeries.Name = "WriteTab_Book_cmbBoxSeries";
            WriteTab_Book_cmbBoxSeries.Size = new System.Drawing.Size(532, 23);
            WriteTab_Book_cmbBoxSeries.TabIndex = 12;
            WriteTab_Book_cmbBoxSeries.SelectedIndexChanged += WriteTab_Book_cmbBoxSeries_SelectedIndexChanged;
            // 
            // WriteTab_Book_txtBoxNewSeriesName
            // 
            WriteTab_Book_txtBoxNewSeriesName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            WriteTab_Book_txtBoxNewSeriesName.Location = new System.Drawing.Point(365, 172);
            WriteTab_Book_txtBoxNewSeriesName.Margin = new Padding(4, 3, 4, 3);
            WriteTab_Book_txtBoxNewSeriesName.Name = "WriteTab_Book_txtBoxNewSeriesName";
            WriteTab_Book_txtBoxNewSeriesName.Size = new System.Drawing.Size(532, 23);
            WriteTab_Book_txtBoxNewSeriesName.TabIndex = 15;
            // 
            // WriteTab_Book_chkBoxIsNewSeries
            // 
            WriteTab_Book_chkBoxIsNewSeries.AutoSize = true;
            WriteTab_Book_chkBoxIsNewSeries.Location = new System.Drawing.Point(365, 144);
            WriteTab_Book_chkBoxIsNewSeries.Margin = new Padding(4, 3, 4, 3);
            WriteTab_Book_chkBoxIsNewSeries.Name = "WriteTab_Book_chkBoxIsNewSeries";
            WriteTab_Book_chkBoxIsNewSeries.Size = new System.Drawing.Size(82, 19);
            WriteTab_Book_chkBoxIsNewSeries.TabIndex = 14;
            WriteTab_Book_chkBoxIsNewSeries.Text = "Neue Serie";
            WriteTab_Book_chkBoxIsNewSeries.UseVisualStyleBackColor = true;
            WriteTab_Book_chkBoxIsNewSeries.CheckedChanged += WriteTab_Book_chkBoxIsNewSeries_CheckedChanged;
            // 
            // WriteTab_Book_chkBoxIsPartOfSeries
            // 
            WriteTab_Book_chkBoxIsPartOfSeries.AutoSize = true;
            WriteTab_Book_chkBoxIsPartOfSeries.Location = new System.Drawing.Point(365, 84);
            WriteTab_Book_chkBoxIsPartOfSeries.Margin = new Padding(4, 3, 4, 3);
            WriteTab_Book_chkBoxIsPartOfSeries.Name = "WriteTab_Book_chkBoxIsPartOfSeries";
            WriteTab_Book_chkBoxIsPartOfSeries.Size = new System.Drawing.Size(105, 19);
            WriteTab_Book_chkBoxIsPartOfSeries.TabIndex = 11;
            WriteTab_Book_chkBoxIsPartOfSeries.Text = "Gehört zu Serie";
            WriteTab_Book_chkBoxIsPartOfSeries.UseVisualStyleBackColor = true;
            WriteTab_Book_chkBoxIsPartOfSeries.CheckedChanged += WriteTab_Book_chkBoxIsPartOfSeries_CheckedChanged;
            // 
            // WriteTab_Book_labelISBN13
            // 
            WriteTab_Book_labelISBN13.AutoSize = true;
            WriteTab_Book_labelISBN13.Location = new System.Drawing.Point(7, 175);
            WriteTab_Book_labelISBN13.Margin = new Padding(4, 0, 4, 0);
            WriteTab_Book_labelISBN13.Name = "WriteTab_Book_labelISBN13";
            WriteTab_Book_labelISBN13.Size = new System.Drawing.Size(50, 15);
            WriteTab_Book_labelISBN13.TabIndex = 11;
            WriteTab_Book_labelISBN13.Text = "ISBN 13:";
            // 
            // WriteTab_Book_txtBoxISBN13
            // 
            WriteTab_Book_txtBoxISBN13.Location = new System.Drawing.Point(98, 172);
            WriteTab_Book_txtBoxISBN13.Margin = new Padding(4, 3, 4, 3);
            WriteTab_Book_txtBoxISBN13.Name = "WriteTab_Book_txtBoxISBN13";
            WriteTab_Book_txtBoxISBN13.Size = new System.Drawing.Size(164, 23);
            WriteTab_Book_txtBoxISBN13.TabIndex = 8;
            WriteTab_Book_txtBoxISBN13.TextChanged += WriteTab_Book_txtBoxISBN13_TextChanged;
            // 
            // WriteTab_Book_labelISBN10
            // 
            WriteTab_Book_labelISBN10.AutoSize = true;
            WriteTab_Book_labelISBN10.Location = new System.Drawing.Point(7, 145);
            WriteTab_Book_labelISBN10.Margin = new Padding(4, 0, 4, 0);
            WriteTab_Book_labelISBN10.Name = "WriteTab_Book_labelISBN10";
            WriteTab_Book_labelISBN10.Size = new System.Drawing.Size(50, 15);
            WriteTab_Book_labelISBN10.TabIndex = 9;
            WriteTab_Book_labelISBN10.Text = "ISBN 10:";
            // 
            // WriteTab_Book_txtBoxISBN10
            // 
            WriteTab_Book_txtBoxISBN10.Location = new System.Drawing.Point(98, 142);
            WriteTab_Book_txtBoxISBN10.Margin = new Padding(4, 3, 4, 3);
            WriteTab_Book_txtBoxISBN10.Name = "WriteTab_Book_txtBoxISBN10";
            WriteTab_Book_txtBoxISBN10.Size = new System.Drawing.Size(164, 23);
            WriteTab_Book_txtBoxISBN10.TabIndex = 7;
            WriteTab_Book_txtBoxISBN10.TextChanged += WriteTab_Book_txtBoxISBN10_TextChanged;
            // 
            // WriteTab_Book_labelFormat
            // 
            WriteTab_Book_labelFormat.AutoSize = true;
            WriteTab_Book_labelFormat.Location = new System.Drawing.Point(7, 115);
            WriteTab_Book_labelFormat.Margin = new Padding(4, 0, 4, 0);
            WriteTab_Book_labelFormat.Name = "WriteTab_Book_labelFormat";
            WriteTab_Book_labelFormat.Size = new System.Drawing.Size(48, 15);
            WriteTab_Book_labelFormat.TabIndex = 7;
            WriteTab_Book_labelFormat.Text = "Format:";
            // 
            // WriteTab_Book_labelPublishingDate
            // 
            WriteTab_Book_labelPublishingDate.AutoSize = true;
            WriteTab_Book_labelPublishingDate.Location = new System.Drawing.Point(7, 85);
            WriteTab_Book_labelPublishingDate.Margin = new Padding(4, 0, 4, 0);
            WriteTab_Book_labelPublishingDate.Name = "WriteTab_Book_labelPublishingDate";
            WriteTab_Book_labelPublishingDate.Size = new System.Drawing.Size(81, 15);
            WriteTab_Book_labelPublishingDate.TabIndex = 5;
            WriteTab_Book_labelPublishingDate.Text = "Veröffentlicht:";
            // 
            // WriteTab_Book_txtBoxPublishingDate
            // 
            WriteTab_Book_txtBoxPublishingDate.Location = new System.Drawing.Point(98, 82);
            WriteTab_Book_txtBoxPublishingDate.Margin = new Padding(4, 3, 4, 3);
            WriteTab_Book_txtBoxPublishingDate.Name = "WriteTab_Book_txtBoxPublishingDate";
            WriteTab_Book_txtBoxPublishingDate.Size = new System.Drawing.Size(258, 23);
            WriteTab_Book_txtBoxPublishingDate.TabIndex = 5;
            // 
            // WriteTab_Book_labelSubTitle
            // 
            WriteTab_Book_labelSubTitle.AutoSize = true;
            WriteTab_Book_labelSubTitle.Location = new System.Drawing.Point(7, 55);
            WriteTab_Book_labelSubTitle.Margin = new Padding(4, 0, 4, 0);
            WriteTab_Book_labelSubTitle.Name = "WriteTab_Book_labelSubTitle";
            WriteTab_Book_labelSubTitle.Size = new System.Drawing.Size(59, 15);
            WriteTab_Book_labelSubTitle.TabIndex = 3;
            WriteTab_Book_labelSubTitle.Text = "Untertitel:";
            // 
            // WriteTab_Book_txtBoxSubTitle
            // 
            WriteTab_Book_txtBoxSubTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            WriteTab_Book_txtBoxSubTitle.Location = new System.Drawing.Point(98, 47);
            WriteTab_Book_txtBoxSubTitle.Margin = new Padding(4, 3, 4, 3);
            WriteTab_Book_txtBoxSubTitle.Name = "WriteTab_Book_txtBoxSubTitle";
            WriteTab_Book_txtBoxSubTitle.Size = new System.Drawing.Size(807, 23);
            WriteTab_Book_txtBoxSubTitle.TabIndex = 4;
            // 
            // WriteTab_Book_txtBoxTitle
            // 
            WriteTab_Book_txtBoxTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            WriteTab_Book_txtBoxTitle.Location = new System.Drawing.Point(98, 17);
            WriteTab_Book_txtBoxTitle.Margin = new Padding(4, 3, 4, 3);
            WriteTab_Book_txtBoxTitle.Name = "WriteTab_Book_txtBoxTitle";
            WriteTab_Book_txtBoxTitle.Size = new System.Drawing.Size(807, 23);
            WriteTab_Book_txtBoxTitle.TabIndex = 3;
            // 
            // WriteTab_Book_labelTitle
            // 
            WriteTab_Book_labelTitle.AutoSize = true;
            WriteTab_Book_labelTitle.Location = new System.Drawing.Point(7, 25);
            WriteTab_Book_labelTitle.Margin = new Padding(4, 0, 4, 0);
            WriteTab_Book_labelTitle.Name = "WriteTab_Book_labelTitle";
            WriteTab_Book_labelTitle.Size = new System.Drawing.Size(32, 15);
            WriteTab_Book_labelTitle.TabIndex = 0;
            WriteTab_Book_labelTitle.Text = "Titel:";
            // 
            // WriteTab_ISBNLabel_1
            // 
            WriteTab_ISBNLabel_1.AutoSize = true;
            WriteTab_ISBNLabel_1.Location = new System.Drawing.Point(6, 9);
            WriteTab_ISBNLabel_1.Margin = new Padding(4, 0, 4, 0);
            WriteTab_ISBNLabel_1.Name = "WriteTab_ISBNLabel_1";
            WriteTab_ISBNLabel_1.Size = new System.Drawing.Size(35, 15);
            WriteTab_ISBNLabel_1.TabIndex = 0;
            WriteTab_ISBNLabel_1.Text = "ISBN:";
            // 
            // tabPageLent
            // 
            tabPageLent.Controls.Add(LentTab_groupBoxVerleihen);
            tabPageLent.Controls.Add(LentTab_groupBoxSuchen);
            tabPageLent.Location = new System.Drawing.Point(4, 24);
            tabPageLent.Margin = new Padding(4, 3, 4, 3);
            tabPageLent.Name = "tabPageLent";
            tabPageLent.Padding = new Padding(4, 3, 4, 3);
            tabPageLent.Size = new System.Drawing.Size(925, 488);
            tabPageLent.TabIndex = 0;
            tabPageLent.Text = "Verleihen";
            tabPageLent.UseVisualStyleBackColor = true;
            // 
            // LentTab_groupBoxVerleihen
            // 
            LentTab_groupBoxVerleihen.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            LentTab_groupBoxVerleihen.Controls.Add(LentTab_btnRemove);
            LentTab_groupBoxVerleihen.Controls.Add(LentTab_btnLent);
            LentTab_groupBoxVerleihen.Controls.Add(LentTab_dataGridViewLent);
            LentTab_groupBoxVerleihen.Location = new System.Drawing.Point(6, 263);
            LentTab_groupBoxVerleihen.Margin = new Padding(4, 3, 4, 3);
            LentTab_groupBoxVerleihen.Name = "LentTab_groupBoxVerleihen";
            LentTab_groupBoxVerleihen.Padding = new Padding(4, 3, 4, 3);
            LentTab_groupBoxVerleihen.Size = new System.Drawing.Size(905, 217);
            LentTab_groupBoxVerleihen.TabIndex = 1;
            LentTab_groupBoxVerleihen.TabStop = false;
            LentTab_groupBoxVerleihen.Text = "Verleihen";
            // 
            // LentTab_btnRemove
            // 
            LentTab_btnRemove.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            LentTab_btnRemove.Enabled = false;
            LentTab_btnRemove.Location = new System.Drawing.Point(799, 181);
            LentTab_btnRemove.Margin = new Padding(4, 3, 4, 3);
            LentTab_btnRemove.Name = "LentTab_btnRemove";
            LentTab_btnRemove.Size = new System.Drawing.Size(99, 27);
            LentTab_btnRemove.TabIndex = 18;
            LentTab_btnRemove.Text = "Entfernen";
            LentTab_btnRemove.UseVisualStyleBackColor = true;
            LentTab_btnRemove.Click += LentTab_btnRemove_Click;
            // 
            // LentTab_btnLent
            // 
            LentTab_btnLent.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            LentTab_btnLent.Enabled = false;
            LentTab_btnLent.Location = new System.Drawing.Point(693, 181);
            LentTab_btnLent.Margin = new Padding(4, 3, 4, 3);
            LentTab_btnLent.Name = "LentTab_btnLent";
            LentTab_btnLent.Size = new System.Drawing.Size(99, 27);
            LentTab_btnLent.TabIndex = 17;
            LentTab_btnLent.Text = "Verleihen";
            LentTab_btnLent.UseVisualStyleBackColor = true;
            LentTab_btnLent.Click += LentTab_btnLent_Click;
            // 
            // LentTab_dataGridViewLent
            // 
            LentTab_dataGridViewLent.AllowUserToAddRows = false;
            LentTab_dataGridViewLent.AllowUserToDeleteRows = false;
            LentTab_dataGridViewLent.AllowUserToResizeColumns = false;
            LentTab_dataGridViewLent.AllowUserToResizeRows = false;
            LentTab_dataGridViewLent.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            LentTab_dataGridViewLent.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            LentTab_dataGridViewLent.Location = new System.Drawing.Point(10, 22);
            LentTab_dataGridViewLent.Margin = new Padding(4, 3, 4, 3);
            LentTab_dataGridViewLent.Name = "LentTab_dataGridViewLent";
            LentTab_dataGridViewLent.Size = new System.Drawing.Size(888, 152);
            LentTab_dataGridViewLent.TabIndex = 16;
            // 
            // LentTab_groupBoxSuchen
            // 
            LentTab_groupBoxSuchen.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            LentTab_groupBoxSuchen.Controls.Add(LentTab_cmbBoxSubTitle);
            LentTab_groupBoxSuchen.Controls.Add(LentTab_cmbBoxTitle);
            LentTab_groupBoxSuchen.Controls.Add(LentTab_cmbBoxISBN);
            LentTab_groupBoxSuchen.Controls.Add(LentTab_btnPull);
            LentTab_groupBoxSuchen.Controls.Add(LentTab_btnSearch);
            LentTab_groupBoxSuchen.Controls.Add(LentTab_dataGridViewSearch);
            LentTab_groupBoxSuchen.Controls.Add(LentTab_txtBoxAuthorSurName);
            LentTab_groupBoxSuchen.Controls.Add(LentTab_txtBoxAuthorPreName);
            LentTab_groupBoxSuchen.Controls.Add(LentTab_txtBoxSubTitle);
            LentTab_groupBoxSuchen.Controls.Add(LentTab_txtBoxTitle);
            LentTab_groupBoxSuchen.Controls.Add(LentTab_txtBoxISBN);
            LentTab_groupBoxSuchen.Controls.Add(LentTab_labelAuthorSurName);
            LentTab_groupBoxSuchen.Controls.Add(LentTab_labelAuthorPreName);
            LentTab_groupBoxSuchen.Controls.Add(LentTab_labelSubTitle);
            LentTab_groupBoxSuchen.Controls.Add(LentTab_labelTitle);
            LentTab_groupBoxSuchen.Controls.Add(LentTab_labelISBN);
            LentTab_groupBoxSuchen.Location = new System.Drawing.Point(7, 7);
            LentTab_groupBoxSuchen.Margin = new Padding(4, 3, 4, 3);
            LentTab_groupBoxSuchen.Name = "LentTab_groupBoxSuchen";
            LentTab_groupBoxSuchen.Padding = new Padding(4, 3, 4, 3);
            LentTab_groupBoxSuchen.Size = new System.Drawing.Size(904, 249);
            LentTab_groupBoxSuchen.TabIndex = 0;
            LentTab_groupBoxSuchen.TabStop = false;
            LentTab_groupBoxSuchen.Text = "Suchen";
            // 
            // LentTab_cmbBoxSubTitle
            // 
            LentTab_cmbBoxSubTitle.DropDownStyle = ComboBoxStyle.DropDownList;
            LentTab_cmbBoxSubTitle.FormattingEnabled = true;
            LentTab_cmbBoxSubTitle.Items.AddRange(new object[] { "und", "oder" });
            LentTab_cmbBoxSubTitle.Location = new System.Drawing.Point(320, 50);
            LentTab_cmbBoxSubTitle.Margin = new Padding(4, 3, 4, 3);
            LentTab_cmbBoxSubTitle.Name = "LentTab_cmbBoxSubTitle";
            LentTab_cmbBoxSubTitle.Size = new System.Drawing.Size(89, 23);
            LentTab_cmbBoxSubTitle.TabIndex = 10;
            // 
            // LentTab_cmbBoxTitle
            // 
            LentTab_cmbBoxTitle.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            LentTab_cmbBoxTitle.DropDownStyle = ComboBoxStyle.DropDownList;
            LentTab_cmbBoxTitle.FormattingEnabled = true;
            LentTab_cmbBoxTitle.Items.AddRange(new object[] { "und", "oder" });
            LentTab_cmbBoxTitle.Location = new System.Drawing.Point(705, 18);
            LentTab_cmbBoxTitle.Margin = new Padding(4, 3, 4, 3);
            LentTab_cmbBoxTitle.Name = "LentTab_cmbBoxTitle";
            LentTab_cmbBoxTitle.Size = new System.Drawing.Size(89, 23);
            LentTab_cmbBoxTitle.TabIndex = 8;
            // 
            // LentTab_cmbBoxISBN
            // 
            LentTab_cmbBoxISBN.DropDownStyle = ComboBoxStyle.DropDownList;
            LentTab_cmbBoxISBN.FormattingEnabled = true;
            LentTab_cmbBoxISBN.Items.AddRange(new object[] { "und", "oder" });
            LentTab_cmbBoxISBN.Location = new System.Drawing.Point(320, 18);
            LentTab_cmbBoxISBN.Margin = new Padding(4, 3, 4, 3);
            LentTab_cmbBoxISBN.Name = "LentTab_cmbBoxISBN";
            LentTab_cmbBoxISBN.Size = new System.Drawing.Size(89, 23);
            LentTab_cmbBoxISBN.TabIndex = 6;
            // 
            // LentTab_btnPull
            // 
            LentTab_btnPull.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            LentTab_btnPull.Enabled = false;
            LentTab_btnPull.Location = new System.Drawing.Point(798, 216);
            LentTab_btnPull.Margin = new Padding(4, 3, 4, 3);
            LentTab_btnPull.Name = "LentTab_btnPull";
            LentTab_btnPull.Size = new System.Drawing.Size(99, 27);
            LentTab_btnPull.TabIndex = 15;
            LentTab_btnPull.Text = "Übernehmen";
            LentTab_btnPull.UseVisualStyleBackColor = true;
            LentTab_btnPull.Click += LentTab_btnPull_Click;
            // 
            // LentTab_btnSearch
            // 
            LentTab_btnSearch.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            LentTab_btnSearch.Location = new System.Drawing.Point(798, 182);
            LentTab_btnSearch.Margin = new Padding(4, 3, 4, 3);
            LentTab_btnSearch.Name = "LentTab_btnSearch";
            LentTab_btnSearch.Size = new System.Drawing.Size(99, 27);
            LentTab_btnSearch.TabIndex = 14;
            LentTab_btnSearch.Text = "Suche";
            LentTab_btnSearch.UseVisualStyleBackColor = true;
            LentTab_btnSearch.Click += LentTab_btnSearch_Click;
            // 
            // LentTab_dataGridViewSearch
            // 
            LentTab_dataGridViewSearch.AllowUserToAddRows = false;
            LentTab_dataGridViewSearch.AllowUserToDeleteRows = false;
            LentTab_dataGridViewSearch.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            LentTab_dataGridViewSearch.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            LentTab_dataGridViewSearch.Location = new System.Drawing.Point(9, 110);
            LentTab_dataGridViewSearch.Margin = new Padding(4, 3, 4, 3);
            LentTab_dataGridViewSearch.Name = "LentTab_dataGridViewSearch";
            LentTab_dataGridViewSearch.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            LentTab_dataGridViewSearch.Size = new System.Drawing.Size(782, 133);
            LentTab_dataGridViewSearch.TabIndex = 13;
            // 
            // LentTab_txtBoxAuthorSurName
            // 
            LentTab_txtBoxAuthorSurName.Location = new System.Drawing.Point(450, 80);
            LentTab_txtBoxAuthorSurName.Margin = new Padding(4, 3, 4, 3);
            LentTab_txtBoxAuthorSurName.Name = "LentTab_txtBoxAuthorSurName";
            LentTab_txtBoxAuthorSurName.Size = new System.Drawing.Size(238, 23);
            LentTab_txtBoxAuthorSurName.TabIndex = 12;
            // 
            // LentTab_txtBoxAuthorPreName
            // 
            LentTab_txtBoxAuthorPreName.Location = new System.Drawing.Point(125, 80);
            LentTab_txtBoxAuthorPreName.Margin = new Padding(4, 3, 4, 3);
            LentTab_txtBoxAuthorPreName.Name = "LentTab_txtBoxAuthorPreName";
            LentTab_txtBoxAuthorPreName.Size = new System.Drawing.Size(238, 23);
            LentTab_txtBoxAuthorPreName.TabIndex = 11;
            // 
            // LentTab_txtBoxSubTitle
            // 
            LentTab_txtBoxSubTitle.Location = new System.Drawing.Point(74, 50);
            LentTab_txtBoxSubTitle.Margin = new Padding(4, 3, 4, 3);
            LentTab_txtBoxSubTitle.Name = "LentTab_txtBoxSubTitle";
            LentTab_txtBoxSubTitle.Size = new System.Drawing.Size(238, 23);
            LentTab_txtBoxSubTitle.TabIndex = 9;
            // 
            // LentTab_txtBoxTitle
            // 
            LentTab_txtBoxTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            LentTab_txtBoxTitle.Location = new System.Drawing.Point(458, 20);
            LentTab_txtBoxTitle.Margin = new Padding(4, 3, 4, 3);
            LentTab_txtBoxTitle.Name = "LentTab_txtBoxTitle";
            LentTab_txtBoxTitle.Size = new System.Drawing.Size(238, 23);
            LentTab_txtBoxTitle.TabIndex = 7;
            // 
            // LentTab_txtBoxISBN
            // 
            LentTab_txtBoxISBN.Location = new System.Drawing.Point(74, 20);
            LentTab_txtBoxISBN.Margin = new Padding(4, 3, 4, 3);
            LentTab_txtBoxISBN.Name = "LentTab_txtBoxISBN";
            LentTab_txtBoxISBN.Size = new System.Drawing.Size(238, 23);
            LentTab_txtBoxISBN.TabIndex = 5;
            // 
            // LentTab_labelAuthorSurName
            // 
            LentTab_labelAuthorSurName.AutoSize = true;
            LentTab_labelAuthorSurName.Location = new System.Drawing.Point(371, 83);
            LentTab_labelAuthorSurName.Margin = new Padding(4, 0, 4, 0);
            LentTab_labelAuthorSurName.Name = "LentTab_labelAuthorSurName";
            LentTab_labelAuthorSurName.Size = new System.Drawing.Size(68, 15);
            LentTab_labelAuthorSurName.TabIndex = 4;
            LentTab_labelAuthorSurName.Text = "Nachname:";
            // 
            // LentTab_labelAuthorPreName
            // 
            LentTab_labelAuthorPreName.AutoSize = true;
            LentTab_labelAuthorPreName.Location = new System.Drawing.Point(8, 83);
            LentTab_labelAuthorPreName.Margin = new Padding(4, 0, 4, 0);
            LentTab_labelAuthorPreName.Name = "LentTab_labelAuthorPreName";
            LentTab_labelAuthorPreName.Size = new System.Drawing.Size(105, 15);
            LentTab_labelAuthorPreName.TabIndex = 3;
            LentTab_labelAuthorPreName.Text = "Autor_in Vorname:";
            // 
            // LentTab_labelSubTitle
            // 
            LentTab_labelSubTitle.AutoSize = true;
            LentTab_labelSubTitle.Location = new System.Drawing.Point(8, 53);
            LentTab_labelSubTitle.Margin = new Padding(4, 0, 4, 0);
            LentTab_labelSubTitle.Name = "LentTab_labelSubTitle";
            LentTab_labelSubTitle.Size = new System.Drawing.Size(59, 15);
            LentTab_labelSubTitle.TabIndex = 2;
            LentTab_labelSubTitle.Text = "Untertitel:";
            // 
            // LentTab_labelTitle
            // 
            LentTab_labelTitle.AutoSize = true;
            LentTab_labelTitle.Location = new System.Drawing.Point(416, 22);
            LentTab_labelTitle.Margin = new Padding(4, 0, 4, 0);
            LentTab_labelTitle.Name = "LentTab_labelTitle";
            LentTab_labelTitle.Size = new System.Drawing.Size(32, 15);
            LentTab_labelTitle.TabIndex = 1;
            LentTab_labelTitle.Text = "Titel:";
            // 
            // LentTab_labelISBN
            // 
            LentTab_labelISBN.AutoSize = true;
            LentTab_labelISBN.Location = new System.Drawing.Point(8, 23);
            LentTab_labelISBN.Margin = new Padding(4, 0, 4, 0);
            LentTab_labelISBN.Name = "LentTab_labelISBN";
            LentTab_labelISBN.Size = new System.Drawing.Size(35, 15);
            LentTab_labelISBN.TabIndex = 0;
            LentTab_labelISBN.Text = "ISBN:";
            // 
            // tabPageReturn
            // 
            tabPageReturn.Controls.Add(ReturnTab_GroupBoxGetBack);
            tabPageReturn.Controls.Add(ReturnTab_GroupBoxSearch);
            tabPageReturn.Controls.Add(ReturnTab_chkBoxIgnoreIsActive);
            tabPageReturn.Controls.Add(ReturnTab_btnShowAll);
            tabPageReturn.Location = new System.Drawing.Point(4, 24);
            tabPageReturn.Margin = new Padding(4, 3, 4, 3);
            tabPageReturn.Name = "tabPageReturn";
            tabPageReturn.Padding = new Padding(4, 3, 4, 3);
            tabPageReturn.Size = new System.Drawing.Size(925, 488);
            tabPageReturn.TabIndex = 1;
            tabPageReturn.Text = "Zurücknehmen";
            tabPageReturn.UseVisualStyleBackColor = true;
            // 
            // ReturnTab_GroupBoxGetBack
            // 
            ReturnTab_GroupBoxGetBack.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ReturnTab_GroupBoxGetBack.Controls.Add(ReturnTab_btnReturn);
            ReturnTab_GroupBoxGetBack.Controls.Add(ReturnTab_dataGridViewReturn);
            ReturnTab_GroupBoxGetBack.Location = new System.Drawing.Point(12, 168);
            ReturnTab_GroupBoxGetBack.Margin = new Padding(4, 3, 4, 3);
            ReturnTab_GroupBoxGetBack.Name = "ReturnTab_GroupBoxGetBack";
            ReturnTab_GroupBoxGetBack.Padding = new Padding(4, 3, 4, 3);
            ReturnTab_GroupBoxGetBack.Size = new System.Drawing.Size(898, 310);
            ReturnTab_GroupBoxGetBack.TabIndex = 0;
            ReturnTab_GroupBoxGetBack.TabStop = false;
            ReturnTab_GroupBoxGetBack.Text = "Rückgabe";
            // 
            // ReturnTab_btnReturn
            // 
            ReturnTab_btnReturn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            ReturnTab_btnReturn.Location = new System.Drawing.Point(789, 277);
            ReturnTab_btnReturn.Margin = new Padding(4, 3, 4, 3);
            ReturnTab_btnReturn.Name = "ReturnTab_btnReturn";
            ReturnTab_btnReturn.Size = new System.Drawing.Size(103, 27);
            ReturnTab_btnReturn.TabIndex = 1;
            ReturnTab_btnReturn.Text = "Zurücknehmen";
            ReturnTab_btnReturn.UseVisualStyleBackColor = true;
            ReturnTab_btnReturn.Click += ReturnTab_btnReturn_Click;
            // 
            // ReturnTab_dataGridViewReturn
            // 
            ReturnTab_dataGridViewReturn.AllowUserToAddRows = false;
            ReturnTab_dataGridViewReturn.AllowUserToDeleteRows = false;
            ReturnTab_dataGridViewReturn.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ReturnTab_dataGridViewReturn.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ReturnTab_dataGridViewReturn.Location = new System.Drawing.Point(7, 22);
            ReturnTab_dataGridViewReturn.Margin = new Padding(4, 3, 4, 3);
            ReturnTab_dataGridViewReturn.Name = "ReturnTab_dataGridViewReturn";
            ReturnTab_dataGridViewReturn.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            ReturnTab_dataGridViewReturn.Size = new System.Drawing.Size(884, 248);
            ReturnTab_dataGridViewReturn.TabIndex = 0;
            // 
            // ReturnTab_GroupBoxSearch
            // 
            ReturnTab_GroupBoxSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ReturnTab_GroupBoxSearch.Controls.Add(ReturnTab_txtBoxBookTitle);
            ReturnTab_GroupBoxSearch.Controls.Add(ReturnTab_labelBookTitle);
            ReturnTab_GroupBoxSearch.Controls.Add(ReturnTab_btnSearch);
            ReturnTab_GroupBoxSearch.Controls.Add(ReturnTab_txtBoxSurName);
            ReturnTab_GroupBoxSearch.Controls.Add(ReturnTab_txtBoxPreName);
            ReturnTab_GroupBoxSearch.Controls.Add(ReturnTab_txtBoxISBN);
            ReturnTab_GroupBoxSearch.Controls.Add(ReturnTab_labelOptional);
            ReturnTab_GroupBoxSearch.Controls.Add(ReturnTab_labelSurName);
            ReturnTab_GroupBoxSearch.Controls.Add(ReturnTab_labelPreName);
            ReturnTab_GroupBoxSearch.Controls.Add(ReturnTab_labelLentTo);
            ReturnTab_GroupBoxSearch.Controls.Add(ReturnTab_labelISBN);
            ReturnTab_GroupBoxSearch.Location = new System.Drawing.Point(12, 40);
            ReturnTab_GroupBoxSearch.Margin = new Padding(4, 3, 4, 3);
            ReturnTab_GroupBoxSearch.Name = "ReturnTab_GroupBoxSearch";
            ReturnTab_GroupBoxSearch.Padding = new Padding(4, 3, 4, 3);
            ReturnTab_GroupBoxSearch.Size = new System.Drawing.Size(898, 121);
            ReturnTab_GroupBoxSearch.TabIndex = 0;
            ReturnTab_GroupBoxSearch.TabStop = false;
            ReturnTab_GroupBoxSearch.Text = "Suchen";
            // 
            // ReturnTab_txtBoxBookTitle
            // 
            ReturnTab_txtBoxBookTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ReturnTab_txtBoxBookTitle.Location = new System.Drawing.Point(264, 22);
            ReturnTab_txtBoxBookTitle.Margin = new Padding(4, 3, 4, 3);
            ReturnTab_txtBoxBookTitle.Name = "ReturnTab_txtBoxBookTitle";
            ReturnTab_txtBoxBookTitle.Size = new System.Drawing.Size(116, 23);
            ReturnTab_txtBoxBookTitle.TabIndex = 6;
            // 
            // ReturnTab_labelBookTitle
            // 
            ReturnTab_labelBookTitle.AutoSize = true;
            ReturnTab_labelBookTitle.Location = new System.Drawing.Point(191, 25);
            ReturnTab_labelBookTitle.Margin = new Padding(4, 0, 4, 0);
            ReturnTab_labelBookTitle.Name = "ReturnTab_labelBookTitle";
            ReturnTab_labelBookTitle.Size = new System.Drawing.Size(57, 15);
            ReturnTab_labelBookTitle.TabIndex = 9;
            ReturnTab_labelBookTitle.Text = "Buchtitel:";
            // 
            // ReturnTab_btnSearch
            // 
            ReturnTab_btnSearch.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            ReturnTab_btnSearch.Location = new System.Drawing.Point(804, 88);
            ReturnTab_btnSearch.Margin = new Padding(4, 3, 4, 3);
            ReturnTab_btnSearch.Name = "ReturnTab_btnSearch";
            ReturnTab_btnSearch.Size = new System.Drawing.Size(88, 27);
            ReturnTab_btnSearch.TabIndex = 9;
            ReturnTab_btnSearch.Text = "Suche";
            ReturnTab_btnSearch.UseVisualStyleBackColor = true;
            ReturnTab_btnSearch.Click += ReturnTab_btnSearch_Click;
            // 
            // ReturnTab_txtBoxSurName
            // 
            ReturnTab_txtBoxSurName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ReturnTab_txtBoxSurName.Location = new System.Drawing.Point(264, 82);
            ReturnTab_txtBoxSurName.Margin = new Padding(4, 3, 4, 3);
            ReturnTab_txtBoxSurName.Name = "ReturnTab_txtBoxSurName";
            ReturnTab_txtBoxSurName.Size = new System.Drawing.Size(116, 23);
            ReturnTab_txtBoxSurName.TabIndex = 8;
            // 
            // ReturnTab_txtBoxPreName
            // 
            ReturnTab_txtBoxPreName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ReturnTab_txtBoxPreName.Location = new System.Drawing.Point(68, 82);
            ReturnTab_txtBoxPreName.Margin = new Padding(4, 3, 4, 3);
            ReturnTab_txtBoxPreName.Name = "ReturnTab_txtBoxPreName";
            ReturnTab_txtBoxPreName.Size = new System.Drawing.Size(116, 23);
            ReturnTab_txtBoxPreName.TabIndex = 7;
            // 
            // ReturnTab_txtBoxISBN
            // 
            ReturnTab_txtBoxISBN.Location = new System.Drawing.Point(68, 22);
            ReturnTab_txtBoxISBN.Margin = new Padding(4, 3, 4, 3);
            ReturnTab_txtBoxISBN.Name = "ReturnTab_txtBoxISBN";
            ReturnTab_txtBoxISBN.Size = new System.Drawing.Size(116, 23);
            ReturnTab_txtBoxISBN.TabIndex = 5;
            // 
            // ReturnTab_labelOptional
            // 
            ReturnTab_labelOptional.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ReturnTab_labelOptional.AutoSize = true;
            ReturnTab_labelOptional.Location = new System.Drawing.Point(379, 85);
            ReturnTab_labelOptional.Margin = new Padding(4, 0, 4, 0);
            ReturnTab_labelOptional.Name = "ReturnTab_labelOptional";
            ReturnTab_labelOptional.Size = new System.Drawing.Size(59, 15);
            ReturnTab_labelOptional.TabIndex = 4;
            ReturnTab_labelOptional.Text = "(optional)";
            // 
            // ReturnTab_labelSurName
            // 
            ReturnTab_labelSurName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ReturnTab_labelSurName.AutoSize = true;
            ReturnTab_labelSurName.Location = new System.Drawing.Point(191, 85);
            ReturnTab_labelSurName.Margin = new Padding(4, 0, 4, 0);
            ReturnTab_labelSurName.Name = "ReturnTab_labelSurName";
            ReturnTab_labelSurName.Size = new System.Drawing.Size(68, 15);
            ReturnTab_labelSurName.TabIndex = 3;
            ReturnTab_labelSurName.Text = "Nachname:";
            // 
            // ReturnTab_labelPreName
            // 
            ReturnTab_labelPreName.AutoSize = true;
            ReturnTab_labelPreName.Location = new System.Drawing.Point(7, 85);
            ReturnTab_labelPreName.Margin = new Padding(4, 0, 4, 0);
            ReturnTab_labelPreName.Name = "ReturnTab_labelPreName";
            ReturnTab_labelPreName.Size = new System.Drawing.Size(57, 15);
            ReturnTab_labelPreName.TabIndex = 2;
            ReturnTab_labelPreName.Text = "Vorname:";
            // 
            // ReturnTab_labelLentTo
            // 
            ReturnTab_labelLentTo.AutoSize = true;
            ReturnTab_labelLentTo.Location = new System.Drawing.Point(7, 60);
            ReturnTab_labelLentTo.Margin = new Padding(4, 0, 4, 0);
            ReturnTab_labelLentTo.Name = "ReturnTab_labelLentTo";
            ReturnTab_labelLentTo.Size = new System.Drawing.Size(74, 15);
            ReturnTab_labelLentTo.TabIndex = 1;
            ReturnTab_labelLentTo.Text = "Verliehen an:";
            // 
            // ReturnTab_labelISBN
            // 
            ReturnTab_labelISBN.AutoSize = true;
            ReturnTab_labelISBN.Location = new System.Drawing.Point(7, 25);
            ReturnTab_labelISBN.Margin = new Padding(4, 0, 4, 0);
            ReturnTab_labelISBN.Name = "ReturnTab_labelISBN";
            ReturnTab_labelISBN.Size = new System.Drawing.Size(35, 15);
            ReturnTab_labelISBN.TabIndex = 0;
            ReturnTab_labelISBN.Text = "ISBN:";
            // 
            // ReturnTab_chkBoxIgnoreIsActive
            // 
            ReturnTab_chkBoxIgnoreIsActive.AutoSize = true;
            ReturnTab_chkBoxIgnoreIsActive.Location = new System.Drawing.Point(108, 12);
            ReturnTab_chkBoxIgnoreIsActive.Margin = new Padding(4, 3, 4, 3);
            ReturnTab_chkBoxIgnoreIsActive.Name = "ReturnTab_chkBoxIgnoreIsActive";
            ReturnTab_chkBoxIgnoreIsActive.Size = new System.Drawing.Size(116, 19);
            ReturnTab_chkBoxIgnoreIsActive.TabIndex = 2;
            ReturnTab_chkBoxIgnoreIsActive.Text = "Active ignorieren";
            ReturnTab_chkBoxIgnoreIsActive.UseVisualStyleBackColor = true;
            // 
            // ReturnTab_btnShowAll
            // 
            ReturnTab_btnShowAll.Location = new System.Drawing.Point(8, 7);
            ReturnTab_btnShowAll.Margin = new Padding(4, 3, 4, 3);
            ReturnTab_btnShowAll.Name = "ReturnTab_btnShowAll";
            ReturnTab_btnShowAll.Size = new System.Drawing.Size(93, 27);
            ReturnTab_btnShowAll.TabIndex = 0;
            ReturnTab_btnShowAll.Text = "Alle anzeigen";
            ReturnTab_btnShowAll.UseVisualStyleBackColor = true;
            ReturnTab_btnShowAll.Click += ReturnTab_btnShowAll_Click;
            // 
            // cmbBoxLanguage
            // 
            cmbBoxLanguage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmbBoxLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbBoxLanguage.FormattingEnabled = true;
            cmbBoxLanguage.Location = new System.Drawing.Point(869, -1);
            cmbBoxLanguage.Margin = new Padding(4, 3, 4, 3);
            cmbBoxLanguage.Name = "cmbBoxLanguage";
            cmbBoxLanguage.Size = new System.Drawing.Size(65, 23);
            cmbBoxLanguage.TabIndex = 1;
            cmbBoxLanguage.SelectedIndexChanged += LanguageChanged;
            // 
            // cmbBoxColorMode
            // 
            cmbBoxColorMode.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmbBoxColorMode.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbBoxColorMode.FormattingEnabled = true;
            cmbBoxColorMode.Location = new System.Drawing.Point(779, -1);
            cmbBoxColorMode.Margin = new Padding(4, 3, 4, 3);
            cmbBoxColorMode.Name = "cmbBoxColorMode";
            cmbBoxColorMode.Size = new System.Drawing.Size(82, 23);
            cmbBoxColorMode.TabIndex = 2;
            cmbBoxColorMode.SelectedIndexChanged += ColorModeChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(933, 519);
            Controls.Add(cmbBoxColorMode);
            Controls.Add(cmbBoxLanguage);
            Controls.Add(tabControl);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            MinimumSize = new System.Drawing.Size(949, 558);
            Name = "Form1";
            Text = "ISBN Caller";
            Load += Form1_Load;
            tabControl.ResumeLayout(false);
            tabPageSearch.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)SearchTab_dataGridViewSearch).EndInit();
            SearchTab_groupBoxInput.ResumeLayout(false);
            SearchTab_groupBoxInput.PerformLayout();
            tabPageWrite.ResumeLayout(false);
            tabPageWrite.PerformLayout();
            WriteTab_groupBoxAuthor.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)WriteTab_Author_dataGridViewAuthor).EndInit();
            WriteTab_groupBoxBook.ResumeLayout(false);
            WriteTab_groupBoxBook.PerformLayout();
            tabPageLent.ResumeLayout(false);
            LentTab_groupBoxVerleihen.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)LentTab_dataGridViewLent).EndInit();
            LentTab_groupBoxSuchen.ResumeLayout(false);
            LentTab_groupBoxSuchen.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)LentTab_dataGridViewSearch).EndInit();
            tabPageReturn.ResumeLayout(false);
            tabPageReturn.PerformLayout();
            ReturnTab_GroupBoxGetBack.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)ReturnTab_dataGridViewReturn).EndInit();
            ReturnTab_GroupBoxSearch.ResumeLayout(false);
            ReturnTab_GroupBoxSearch.PerformLayout();
            ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button WriteTab_btnOK;
        private System.Windows.Forms.Button WriteTab_btnCancel;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageWrite;
        private System.Windows.Forms.TextBox WriteTab_txtBoxISBN_1;
        private System.Windows.Forms.GroupBox WriteTab_groupBoxAuthor;
        private System.Windows.Forms.DataGridView WriteTab_Author_dataGridViewAuthor;
        private System.Windows.Forms.Button WriteTab_btnRegisterWOutISBN;
        private System.Windows.Forms.GroupBox WriteTab_groupBoxBook;
        private System.Windows.Forms.Button WriteTab_Book_btnCalculateISBN13;
        private System.Windows.Forms.Button WriteTab_Book_btnCalculateISBN10;
        private System.Windows.Forms.Label WriteTab_Book_labelMaxNoCount;
        private System.Windows.Forms.Label WriteTab_Book_labelMaxNo;
        private System.Windows.Forms.TextBox WriteTab_Book_txtBoxNoInSeries;
        private System.Windows.Forms.Label WriteTab_Book_labelNoInSeries;
        private System.Windows.Forms.ComboBox WriteTab_Book_cmbBoxSeries;
        private System.Windows.Forms.TextBox WriteTab_Book_txtBoxNewSeriesName;
        private System.Windows.Forms.CheckBox WriteTab_Book_chkBoxIsNewSeries;
        private System.Windows.Forms.CheckBox WriteTab_Book_chkBoxIsPartOfSeries;
        private System.Windows.Forms.Label WriteTab_Book_labelISBN13;
        private System.Windows.Forms.TextBox WriteTab_Book_txtBoxISBN13;
        private System.Windows.Forms.Label WriteTab_Book_labelISBN10;
        private System.Windows.Forms.TextBox WriteTab_Book_txtBoxISBN10;
        private System.Windows.Forms.Label WriteTab_Book_labelFormat;
        private System.Windows.Forms.Label WriteTab_Book_labelPublishingDate;
        private System.Windows.Forms.TextBox WriteTab_Book_txtBoxPublishingDate;
        private System.Windows.Forms.Label WriteTab_Book_labelSubTitle;
        private System.Windows.Forms.TextBox WriteTab_Book_txtBoxSubTitle;
        private System.Windows.Forms.TextBox WriteTab_Book_txtBoxTitle;
        private System.Windows.Forms.Label WriteTab_Book_labelTitle;
        private System.Windows.Forms.Label WriteTab_ISBNLabel_1;
        private System.Windows.Forms.TabPage tabPageSearch;
        private System.Windows.Forms.TabPage tabPageLent;
        private System.Windows.Forms.GroupBox LentTab_groupBoxVerleihen;
        private System.Windows.Forms.Button LentTab_btnLent;
        private System.Windows.Forms.DataGridView LentTab_dataGridViewLent;
        private System.Windows.Forms.GroupBox LentTab_groupBoxSuchen;
        private System.Windows.Forms.Button LentTab_btnPull;
        private System.Windows.Forms.Button LentTab_btnSearch;
        private System.Windows.Forms.DataGridView LentTab_dataGridViewSearch;
        private System.Windows.Forms.TextBox LentTab_txtBoxAuthorSurName;
        private System.Windows.Forms.TextBox LentTab_txtBoxAuthorPreName;
        private System.Windows.Forms.TextBox LentTab_txtBoxSubTitle;
        private System.Windows.Forms.TextBox LentTab_txtBoxTitle;
        private System.Windows.Forms.TextBox LentTab_txtBoxISBN;
        private System.Windows.Forms.Label LentTab_labelAuthorSurName;
        private System.Windows.Forms.Label LentTab_labelAuthorPreName;
        private System.Windows.Forms.Label LentTab_labelSubTitle;
        private System.Windows.Forms.Label LentTab_labelTitle;
        private System.Windows.Forms.Label LentTab_labelISBN;
        private System.Windows.Forms.TabPage tabPageReturn;
        private System.Windows.Forms.ComboBox LentTab_cmbBoxTitle;
        private System.Windows.Forms.ComboBox LentTab_cmbBoxISBN;
        public System.Windows.Forms.ComboBox LentTab_cmbBoxSubTitle;
        private System.Windows.Forms.Button LentTab_btnRemove;
        private System.Windows.Forms.Button ReturnTab_btnShowAll;
        private System.Windows.Forms.GroupBox ReturnTab_GroupBoxSearch;
        private System.Windows.Forms.GroupBox ReturnTab_GroupBoxGetBack;
        private System.Windows.Forms.Button ReturnTab_btnSearch;
        private System.Windows.Forms.TextBox ReturnTab_txtBoxSurName;
        private System.Windows.Forms.TextBox ReturnTab_txtBoxPreName;
        private System.Windows.Forms.TextBox ReturnTab_txtBoxISBN;
        private System.Windows.Forms.Label ReturnTab_labelOptional;
        private System.Windows.Forms.Label ReturnTab_labelSurName;
        private System.Windows.Forms.Label ReturnTab_labelPreName;
        private System.Windows.Forms.Label ReturnTab_labelLentTo;
        private System.Windows.Forms.Label ReturnTab_labelISBN;
        private System.Windows.Forms.DataGridView ReturnTab_dataGridViewReturn;
        private System.Windows.Forms.Button ReturnTab_btnReturn;
        private System.Windows.Forms.TextBox ReturnTab_txtBoxBookTitle;
        private System.Windows.Forms.Label ReturnTab_labelBookTitle;
        private System.Windows.Forms.CheckBox ReturnTab_chkBoxIgnoreIsActive;
        private System.Windows.Forms.GroupBox SearchTab_groupBoxInput;
        private System.Windows.Forms.Button SearchTab_btnSearch;
#if DEBUG
        private System.Windows.Forms.Button SearchTab_btnCorrection;
#endif
        private System.Windows.Forms.DateTimePicker SearchTab_dtpFrom;
        private System.Windows.Forms.Label SearchTab_labelPublishedFrom;
        private System.Windows.Forms.Label SearchTab_labelFormat;
        private System.Windows.Forms.ComboBox SearchTab_cmbBoxFormat;
        private System.Windows.Forms.Label SearchTab_labelSeries;
        private System.Windows.Forms.RadioButton SearchTab_radioBtnWOutSeries;
        private System.Windows.Forms.RadioButton SearchTab_radioBtnWSeries;
        private System.Windows.Forms.TextBox SearchTab_txtBoxSubTitle;
        private System.Windows.Forms.Label SearchTab_labelSubTitle;
        private System.Windows.Forms.TextBox SearchTab_txtBoxAuthorSurName;
        private System.Windows.Forms.Label SearchTab_labelAuthorSurName;
        private System.Windows.Forms.Label SearchTab_labelAuthorPreName;
        private System.Windows.Forms.TextBox SearchTab_txtBoxAuthorPreName;
        private System.Windows.Forms.TextBox SearchTab_txtBoxTitle;
        private System.Windows.Forms.Label SearchTab_labelTitle;
        private System.Windows.Forms.TextBox SearchTab_txtBoxISBN;
        private System.Windows.Forms.Label SearchTab_labelISBN;
        private System.Windows.Forms.DateTimePicker SearchTab_dtpTo;
        private System.Windows.Forms.Label SearchTab_labelPublishedTo;
        private System.Windows.Forms.CheckBox SearchTab_chkBoxShowLent;
        private System.Windows.Forms.DataGridView SearchTab_dataGridViewSearch;
        private System.Windows.Forms.CheckBox SearchTab_chkBoxOnlyShowFirstAuthor;
        private System.Windows.Forms.ComboBox SearchTab_cmbBoxSeries;
        private System.Windows.Forms.Label WriteTab_WorkInProgressLabel;
        private System.Windows.Forms.ComboBox WriteTab_Book_cmbBoxFormat;
        private System.Windows.Forms.ComboBox cmbBoxLanguage;
        private System.Windows.Forms.ComboBox cmbBoxColorMode;
        private CheckBox SearchTab_chkBoxUseDates;
    }
}

