namespace ISBNCaller_GUI.Correction
{
    partial class CorrectionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            CorrectionForm_labelTitle = new System.Windows.Forms.Label();
            CorrectionForm_labelSubTitle = new System.Windows.Forms.Label();
            CorrectionForm_labelSeries = new System.Windows.Forms.Label();
            CorrectionForm_labelNoInSeries = new System.Windows.Forms.Label();
            CorrectionForm_labelPublished = new System.Windows.Forms.Label();
            CorrectionForm_labelFormat = new System.Windows.Forms.Label();
            CorrectionForm_labelISBN13 = new System.Windows.Forms.Label();
            CorrectionForm_labelISBN10 = new System.Windows.Forms.Label();
            CorrectionForm_labelAuthor = new System.Windows.Forms.Label();
            CorrectionForm_txtBoxTitle = new System.Windows.Forms.TextBox();
            CorrectionForm_txtBoxSubTitle = new System.Windows.Forms.TextBox();
            CorrectionForm_cmbBoxSeries = new System.Windows.Forms.ComboBox();
            CorrectionForm_txtBoxNoInSeries = new System.Windows.Forms.TextBox();
            CorrectionForm_txtBoxPublished = new System.Windows.Forms.TextBox();
            CorrectionForm_cmbBoxFormat = new System.Windows.Forms.ComboBox();
            CorrectionForm_txtBoxISBN13 = new System.Windows.Forms.TextBox();
            CorrectionForm_txtBoxISBN10 = new System.Windows.Forms.TextBox();
            CorrectionForm_dgvAuthors = new System.Windows.Forms.DataGridView();
            CorrectionForm_btnCalculateISBN13 = new System.Windows.Forms.Button();
            CorrectionForm_btnCalculateISBN10 = new System.Windows.Forms.Button();
            CorrectionForm_btnStartCorrection = new System.Windows.Forms.Button();
            CorrectionForm_btnClose = new System.Windows.Forms.Button();
            CorrectionForm_labelCurrentSeries = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)CorrectionForm_dgvAuthors).BeginInit();
            SuspendLayout();
            // 
            // CorrectionForm_labelTitle
            // 
            CorrectionForm_labelTitle.AutoSize = true;
            CorrectionForm_labelTitle.Location = new System.Drawing.Point(12, 15);
            CorrectionForm_labelTitle.Name = "CorrectionForm_labelTitle";
            CorrectionForm_labelTitle.Size = new System.Drawing.Size(32, 15);
            CorrectionForm_labelTitle.TabIndex = 0;
            CorrectionForm_labelTitle.Text = "Titel:";
            // 
            // CorrectionForm_labelSubTitle
            // 
            CorrectionForm_labelSubTitle.AutoSize = true;
            CorrectionForm_labelSubTitle.Location = new System.Drawing.Point(12, 44);
            CorrectionForm_labelSubTitle.Name = "CorrectionForm_labelSubTitle";
            CorrectionForm_labelSubTitle.Size = new System.Drawing.Size(59, 15);
            CorrectionForm_labelSubTitle.TabIndex = 1;
            CorrectionForm_labelSubTitle.Text = "Untertitel:";
            // 
            // CorrectionForm_labelSeries
            // 
            CorrectionForm_labelSeries.AutoSize = true;
            CorrectionForm_labelSeries.Location = new System.Drawing.Point(12, 73);
            CorrectionForm_labelSeries.Name = "CorrectionForm_labelSeries";
            CorrectionForm_labelSeries.Size = new System.Drawing.Size(35, 15);
            CorrectionForm_labelSeries.TabIndex = 2;
            CorrectionForm_labelSeries.Text = "Serie:";
            // 
            // CorrectionForm_labelNoInSeries
            // 
            CorrectionForm_labelNoInSeries.AutoSize = true;
            CorrectionForm_labelNoInSeries.Location = new System.Drawing.Point(12, 102);
            CorrectionForm_labelNoInSeries.Name = "CorrectionForm_labelNoInSeries";
            CorrectionForm_labelNoInSeries.Size = new System.Drawing.Size(88, 15);
            CorrectionForm_labelNoInSeries.TabIndex = 3;
            CorrectionForm_labelNoInSeries.Text = "Seriennummer:";
            // 
            // CorrectionForm_labelPublished
            // 
            CorrectionForm_labelPublished.AutoSize = true;
            CorrectionForm_labelPublished.Location = new System.Drawing.Point(12, 131);
            CorrectionForm_labelPublished.Name = "CorrectionForm_labelPublished";
            CorrectionForm_labelPublished.Size = new System.Drawing.Size(81, 15);
            CorrectionForm_labelPublished.TabIndex = 4;
            CorrectionForm_labelPublished.Text = "Veröffentlicht:";
            // 
            // CorrectionForm_labelFormat
            // 
            CorrectionForm_labelFormat.AutoSize = true;
            CorrectionForm_labelFormat.Location = new System.Drawing.Point(12, 160);
            CorrectionForm_labelFormat.Name = "CorrectionForm_labelFormat";
            CorrectionForm_labelFormat.Size = new System.Drawing.Size(48, 15);
            CorrectionForm_labelFormat.TabIndex = 5;
            CorrectionForm_labelFormat.Text = "Format:";
            // 
            // CorrectionForm_labelISBN13
            // 
            CorrectionForm_labelISBN13.AutoSize = true;
            CorrectionForm_labelISBN13.Location = new System.Drawing.Point(12, 189);
            CorrectionForm_labelISBN13.Name = "CorrectionForm_labelISBN13";
            CorrectionForm_labelISBN13.Size = new System.Drawing.Size(50, 15);
            CorrectionForm_labelISBN13.TabIndex = 6;
            CorrectionForm_labelISBN13.Text = "ISBN 13:";
            // 
            // CorrectionForm_labelISBN10
            // 
            CorrectionForm_labelISBN10.AutoSize = true;
            CorrectionForm_labelISBN10.Location = new System.Drawing.Point(12, 218);
            CorrectionForm_labelISBN10.Name = "CorrectionForm_labelISBN10";
            CorrectionForm_labelISBN10.Size = new System.Drawing.Size(50, 15);
            CorrectionForm_labelISBN10.TabIndex = 7;
            CorrectionForm_labelISBN10.Text = "ISBN 10:";
            // 
            // CorrectionForm_labelAuthor
            // 
            CorrectionForm_labelAuthor.AutoSize = true;
            CorrectionForm_labelAuthor.Location = new System.Drawing.Point(12, 247);
            CorrectionForm_labelAuthor.Name = "CorrectionForm_labelAuthor";
            CorrectionForm_labelAuthor.Size = new System.Drawing.Size(75, 15);
            CorrectionForm_labelAuthor.TabIndex = 8;
            CorrectionForm_labelAuthor.Text = "Autor_innen:";
            // 
            // CorrectionForm_txtBoxTitle
            // 
            CorrectionForm_txtBoxTitle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            CorrectionForm_txtBoxTitle.Location = new System.Drawing.Point(121, 12);
            CorrectionForm_txtBoxTitle.Name = "CorrectionForm_txtBoxTitle";
            CorrectionForm_txtBoxTitle.Size = new System.Drawing.Size(667, 23);
            CorrectionForm_txtBoxTitle.TabIndex = 9;
            // 
            // CorrectionForm_txtBoxSubTitle
            // 
            CorrectionForm_txtBoxSubTitle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            CorrectionForm_txtBoxSubTitle.Location = new System.Drawing.Point(121, 41);
            CorrectionForm_txtBoxSubTitle.Name = "CorrectionForm_txtBoxSubTitle";
            CorrectionForm_txtBoxSubTitle.Size = new System.Drawing.Size(667, 23);
            CorrectionForm_txtBoxSubTitle.TabIndex = 10;
            // 
            // CorrectionForm_cmbBoxSeries
            // 
            CorrectionForm_cmbBoxSeries.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            CorrectionForm_cmbBoxSeries.FormattingEnabled = true;
            CorrectionForm_cmbBoxSeries.Location = new System.Drawing.Point(121, 70);
            CorrectionForm_cmbBoxSeries.Name = "CorrectionForm_cmbBoxSeries";
            CorrectionForm_cmbBoxSeries.Size = new System.Drawing.Size(371, 23);
            CorrectionForm_cmbBoxSeries.TabIndex = 11;
            CorrectionForm_cmbBoxSeries.SelectedIndexChanged += CorrectionForm_cmbBoxSeries_SelectedIndexChanged;
            // 
            // CorrectionForm_txtBoxNoInSeries
            // 
            CorrectionForm_txtBoxNoInSeries.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            CorrectionForm_txtBoxNoInSeries.Location = new System.Drawing.Point(121, 99);
            CorrectionForm_txtBoxNoInSeries.Name = "CorrectionForm_txtBoxNoInSeries";
            CorrectionForm_txtBoxNoInSeries.Size = new System.Drawing.Size(667, 23);
            CorrectionForm_txtBoxNoInSeries.TabIndex = 12;
            // 
            // CorrectionForm_txtBoxPublished
            // 
            CorrectionForm_txtBoxPublished.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            CorrectionForm_txtBoxPublished.Location = new System.Drawing.Point(121, 128);
            CorrectionForm_txtBoxPublished.Name = "CorrectionForm_txtBoxPublished";
            CorrectionForm_txtBoxPublished.Size = new System.Drawing.Size(667, 23);
            CorrectionForm_txtBoxPublished.TabIndex = 13;
            // 
            // CorrectionForm_cmbBoxFormat
            // 
            CorrectionForm_cmbBoxFormat.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            CorrectionForm_cmbBoxFormat.FormattingEnabled = true;
            CorrectionForm_cmbBoxFormat.Location = new System.Drawing.Point(121, 157);
            CorrectionForm_cmbBoxFormat.Name = "CorrectionForm_cmbBoxFormat";
            CorrectionForm_cmbBoxFormat.Size = new System.Drawing.Size(667, 23);
            CorrectionForm_cmbBoxFormat.TabIndex = 14;
            // 
            // CorrectionForm_txtBoxISBN13
            // 
            CorrectionForm_txtBoxISBN13.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            CorrectionForm_txtBoxISBN13.Location = new System.Drawing.Point(121, 186);
            CorrectionForm_txtBoxISBN13.Name = "CorrectionForm_txtBoxISBN13";
            CorrectionForm_txtBoxISBN13.Size = new System.Drawing.Size(576, 23);
            CorrectionForm_txtBoxISBN13.TabIndex = 15;
            CorrectionForm_txtBoxISBN13.TextChanged += CorrectionForm_txtBoxISBN13_TextChanged;
            // 
            // CorrectionForm_txtBoxISBN10
            // 
            CorrectionForm_txtBoxISBN10.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            CorrectionForm_txtBoxISBN10.Location = new System.Drawing.Point(121, 215);
            CorrectionForm_txtBoxISBN10.Name = "CorrectionForm_txtBoxISBN10";
            CorrectionForm_txtBoxISBN10.Size = new System.Drawing.Size(576, 23);
            CorrectionForm_txtBoxISBN10.TabIndex = 16;
            CorrectionForm_txtBoxISBN10.TextChanged += CorrectionForm_txtBoxISBN10_TextChanged;
            // 
            // CorrectionForm_dgvAuthors
            // 
            CorrectionForm_dgvAuthors.AllowUserToDeleteRows = false;
            CorrectionForm_dgvAuthors.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            CorrectionForm_dgvAuthors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            CorrectionForm_dgvAuthors.Location = new System.Drawing.Point(121, 244);
            CorrectionForm_dgvAuthors.Name = "CorrectionForm_dgvAuthors";
            CorrectionForm_dgvAuthors.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            CorrectionForm_dgvAuthors.Size = new System.Drawing.Size(667, 165);
            CorrectionForm_dgvAuthors.TabIndex = 17;
            CorrectionForm_dgvAuthors.RowsAdded += CorrectionForm_dgvAuthors_RowsAdded;
            // 
            // CorrectionForm_btnCalculateISBN13
            // 
            CorrectionForm_btnCalculateISBN13.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            CorrectionForm_btnCalculateISBN13.Enabled = false;
            CorrectionForm_btnCalculateISBN13.Location = new System.Drawing.Point(703, 186);
            CorrectionForm_btnCalculateISBN13.Name = "CorrectionForm_btnCalculateISBN13";
            CorrectionForm_btnCalculateISBN13.Size = new System.Drawing.Size(85, 23);
            CorrectionForm_btnCalculateISBN13.TabIndex = 18;
            CorrectionForm_btnCalculateISBN13.Text = "Berechnen";
            CorrectionForm_btnCalculateISBN13.UseVisualStyleBackColor = true;
            CorrectionForm_btnCalculateISBN13.Click += CorrectionForm_btnCalculateISBN13_OnClick;
            // 
            // CorrectionForm_btnCalculateISBN10
            // 
            CorrectionForm_btnCalculateISBN10.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            CorrectionForm_btnCalculateISBN10.Enabled = false;
            CorrectionForm_btnCalculateISBN10.Location = new System.Drawing.Point(703, 215);
            CorrectionForm_btnCalculateISBN10.Name = "CorrectionForm_btnCalculateISBN10";
            CorrectionForm_btnCalculateISBN10.Size = new System.Drawing.Size(85, 23);
            CorrectionForm_btnCalculateISBN10.TabIndex = 19;
            CorrectionForm_btnCalculateISBN10.Text = "Berechnen";
            CorrectionForm_btnCalculateISBN10.UseVisualStyleBackColor = true;
            CorrectionForm_btnCalculateISBN10.Click += CorrectionForm_btnCalculateISBN10_OnClick;
            // 
            // CorrectionForm_btnStartCorrection
            // 
            CorrectionForm_btnStartCorrection.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            CorrectionForm_btnStartCorrection.Location = new System.Drawing.Point(612, 415);
            CorrectionForm_btnStartCorrection.Name = "CorrectionForm_btnStartCorrection";
            CorrectionForm_btnStartCorrection.Size = new System.Drawing.Size(85, 23);
            CorrectionForm_btnStartCorrection.TabIndex = 20;
            CorrectionForm_btnStartCorrection.Text = "Korrigieren";
            CorrectionForm_btnStartCorrection.UseVisualStyleBackColor = true;
            CorrectionForm_btnStartCorrection.Click += CorrectionForm_btnStartCorrection_OnClick;
            // 
            // CorrectionForm_btnClose
            // 
            CorrectionForm_btnClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            CorrectionForm_btnClose.Location = new System.Drawing.Point(703, 415);
            CorrectionForm_btnClose.Name = "CorrectionForm_btnClose";
            CorrectionForm_btnClose.Size = new System.Drawing.Size(85, 23);
            CorrectionForm_btnClose.TabIndex = 21;
            CorrectionForm_btnClose.Text = "Schließen";
            CorrectionForm_btnClose.UseVisualStyleBackColor = true;
            CorrectionForm_btnClose.Click += CorrectionForm_btnClose_OnClick;
            // 
            // CorrectionForm_labelCurrentSeries
            // 
            CorrectionForm_labelCurrentSeries.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            CorrectionForm_labelCurrentSeries.AutoSize = true;
            CorrectionForm_labelCurrentSeries.Location = new System.Drawing.Point(498, 73);
            CorrectionForm_labelCurrentSeries.Name = "CorrectionForm_labelCurrentSeries";
            CorrectionForm_labelCurrentSeries.Size = new System.Drawing.Size(38, 15);
            CorrectionForm_labelCurrentSeries.TabIndex = 22;
            CorrectionForm_labelCurrentSeries.Text = "label1";
            // 
            // CorrectionForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 450);
            Controls.Add(CorrectionForm_labelCurrentSeries);
            Controls.Add(CorrectionForm_btnClose);
            Controls.Add(CorrectionForm_btnStartCorrection);
            Controls.Add(CorrectionForm_btnCalculateISBN10);
            Controls.Add(CorrectionForm_btnCalculateISBN13);
            Controls.Add(CorrectionForm_dgvAuthors);
            Controls.Add(CorrectionForm_txtBoxISBN10);
            Controls.Add(CorrectionForm_txtBoxISBN13);
            Controls.Add(CorrectionForm_cmbBoxFormat);
            Controls.Add(CorrectionForm_txtBoxPublished);
            Controls.Add(CorrectionForm_txtBoxNoInSeries);
            Controls.Add(CorrectionForm_cmbBoxSeries);
            Controls.Add(CorrectionForm_txtBoxSubTitle);
            Controls.Add(CorrectionForm_txtBoxTitle);
            Controls.Add(CorrectionForm_labelAuthor);
            Controls.Add(CorrectionForm_labelISBN10);
            Controls.Add(CorrectionForm_labelISBN13);
            Controls.Add(CorrectionForm_labelFormat);
            Controls.Add(CorrectionForm_labelPublished);
            Controls.Add(CorrectionForm_labelNoInSeries);
            Controls.Add(CorrectionForm_labelSeries);
            Controls.Add(CorrectionForm_labelSubTitle);
            Controls.Add(CorrectionForm_labelTitle);
            Name = "CorrectionForm";
            Text = "CorrectionForm";
            ((System.ComponentModel.ISupportInitialize)CorrectionForm_dgvAuthors).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label CorrectionForm_labelTitle;
        private System.Windows.Forms.Label CorrectionForm_labelSubTitle;
        private System.Windows.Forms.Label CorrectionForm_labelSeries;
        private System.Windows.Forms.Label CorrectionForm_labelNoInSeries;
        private System.Windows.Forms.Label CorrectionForm_labelPublished;
        private System.Windows.Forms.Label CorrectionForm_labelFormat;
        private System.Windows.Forms.Label CorrectionForm_labelISBN13;
        private System.Windows.Forms.Label CorrectionForm_labelISBN10;
        private System.Windows.Forms.Label CorrectionForm_labelAuthor;
        private System.Windows.Forms.TextBox CorrectionForm_txtBoxTitle;
        private System.Windows.Forms.TextBox CorrectionForm_txtBoxSubTitle;
        private System.Windows.Forms.ComboBox CorrectionForm_cmbBoxSeries;
        private System.Windows.Forms.TextBox CorrectionForm_txtBoxNoInSeries;
        private System.Windows.Forms.TextBox CorrectionForm_txtBoxPublished;
        private System.Windows.Forms.ComboBox CorrectionForm_cmbBoxFormat;
        private System.Windows.Forms.TextBox CorrectionForm_txtBoxISBN13;
        private System.Windows.Forms.TextBox CorrectionForm_txtBoxISBN10;
        private System.Windows.Forms.DataGridView CorrectionForm_dgvAuthors;
        private System.Windows.Forms.Button CorrectionForm_btnCalculateISBN13;
        private System.Windows.Forms.Button CorrectionForm_btnCalculateISBN10;
        private System.Windows.Forms.Button CorrectionForm_btnStartCorrection;
        private System.Windows.Forms.Button CorrectionForm_btnClose;
        private System.Windows.Forms.Label CorrectionForm_labelCurrentSeries;
    }
}