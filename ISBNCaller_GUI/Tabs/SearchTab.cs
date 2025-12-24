using ISBNCaller_Lib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ISBNCaller_GUI
{
    internal class SearchTab
    {
        #region Variables

        internal TextBox mTxtBoxISBN { get; set; }
        internal TextBox mTxtBoxTitle { get; set; }
        internal TextBox mTxtBoxSubTitle { get; set; }
        internal TextBox mTxtBoxAuthorPreName { get; set; }
        internal TextBox mTxtBoxAuthorSurName { get; set; }
        internal ComboBox mCmbBoxSeries { get; set; }
        internal RadioButton mRadioBtnWSeries { get; set; }
        internal RadioButton mRadioBtnWOutSeries { get; set; }
        internal ComboBox mCmbBoxFormat { get; set; }
        internal DateTimePicker mDtpFrom { get; set; }
        internal DateTimePicker mDtpTo { get; set; }
        internal CheckBox mChkBoxShowLent { get; set; }
        internal CheckBox mChkBoxOnlyShowFirstAuthor { get; set; }
        internal DataGridView mDataGridViewSearch { get; set; }
        internal CheckBox mChkBoxUseDates { get; set; }
        private Form1 mForm1 { get; set; }
        private List<DBReader.SearchStruct> mSearched { get; set; }

        #endregion
        #region Structs
        #endregion
        #region Constructor

        public SearchTab(Form1 form1)
        {
            mForm1 = form1;
        }

        #endregion
        #region Methods
        #region Events

        internal void btnSearchClick()
        {
            try
            {
                string cmd = CreateSQLCmd();
                DBReader dbReader = new DBReader();
                mSearched = dbReader.GetSearched(cmd);
                FillDGV();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        internal void btnCorrectionClick()
        {
            DataGridViewSelectedRowCollection selectedRows = mDataGridViewSearch.SelectedRows;
            List<string> isbns = new List<string>();
            if (selectedRows.Count == 0)
            {
                DataGridViewRowCollection rows = new DataGridViewRowCollection(mDataGridViewSearch);
                for (int index = 0; index < mDataGridViewSearch.SelectedCells.Count; index++)
                {
                    int rowindex = mDataGridViewSearch.SelectedCells[index].RowIndex;
                    DataGridViewRow row = mDataGridViewSearch.Rows[rowindex];
                    isbns.Add(row.Cells[7].Value.ToString());
                } // for
            } // if
            else
            {
                foreach (DataGridViewRow row in selectedRows)
                {
                    isbns.Add(row.Cells[7].Value.ToString());
                }
            } // else

            List<DBReader.SearchStruct> toCorrectList = new List<DBReader.SearchStruct>();
            foreach (var searched in mSearched)
            {
                if (isbns.Contains(searched.ISBN13) || isbns.Contains(searched.ISBN10))
                {
                    toCorrectList.Add(searched);
                }
            } // foreach

            Correction correction = new Correction(mForm1, toCorrectList);
            mForm1.Enabled = false;
            correction.ShowDialog();
        }

        #endregion

        private string CreateSQLCmd()
        {
            List<string> wheres = new List<string>();
            if (mTxtBoxISBN.Text != "")
            {
                switch (mTxtBoxISBN.Text.Length)
                {
                    case 10: wheres.Add($"b.ISBN10 = '{mTxtBoxISBN.Text}'"); break;
                    case 13: wheres.Add($"b.ISBN13 = '{mTxtBoxISBN.Text}'"); break;
                    default: throw new Exception("Die ISBN ist nicht korrekt.");
                } // switch
            } // if

            if (mTxtBoxTitle.Text != "")
            {
                wheres.Add($"b.Title LIKE '%{mTxtBoxTitle.Text}%'");
            }

            if (mTxtBoxSubTitle.Text != "")
            {
                wheres.Add($"b.SubTitle LIKE '%{mTxtBoxSubTitle.Text}%'");
            }

            if (mTxtBoxAuthorPreName.Text != "")
            {
                wheres.Add($"a.PreName = '{mTxtBoxAuthorPreName.Text}'");
            }

            if (mTxtBoxAuthorSurName.Text != "")
            {
                wheres.Add($"a.Name = '{mTxtBoxAuthorSurName.Text}'");
            }

            if (mCmbBoxSeries.Text != "" && mRadioBtnWSeries.Checked)
            {
                int seriesID = (int)mCmbBoxSeries.SelectedValue;
                wheres.Add($"s.SeriesID = {seriesID}");
            }

            List<string> wheres2 = new List<string>();
            if (mRadioBtnWOutSeries.Checked)
            {
                wheres2.Add($"bs.BookID IS NULL");
            }

            if (mCmbBoxFormat.Text != "")
            {
                wheres2.Add($"b.Format = '{mCmbBoxFormat.Text}'");
            }

            DBReader dbReader = new DBReader();
            if (mChkBoxUseDates.Checked)
            {
                string from = $"{mDtpFrom.Value.Year}";
                string to = $"{mDtpTo.Value.Year}";
                List<string> bookIDs = dbReader.GetBooksFromDateRange(from, to);
                wheres2.Add($"b.BookID IN ({string.Join(", ", bookIDs)})");
            } // if

            if (!mChkBoxShowLent.Checked)
            {
                wheres2.Add($"(l.Active IS NULL OR l.Active IS FALSE)");
            }


            string whereClause = "";
            if (wheres.Count > 0)
            {
                whereClause = $"({string.Join(" OR ", wheres)})";
            }

            if (wheres2.Count > 0)
            {
                whereClause += whereClause == "" ? $"{string.Join(" AND", wheres2)}" : $" AND {string.Join(" AND", wheres2)}";
            }

            if (whereClause != "")
            {
                whereClause = $"WHERE {whereClause}";
            }

            string cmd = $"SELECT b.Title, b.SubTitle, bs.NoInSeries, s.Name, a.PreName, a.Name, b.PublishingDate, b.Format, b.ISBN10, b.ISBN13, l.Active, b.BookID " +
                $"FROM Books b " +
                $"LEFT JOIN BookAuthor ba ON b.BookID = ba.BookID " +
                $"LEFT JOIN Authors a ON ba.AuthorID = a.AuthorID " +
                $"LEFT JOIN BookSeries bs ON b.BookID = bs.BookID " +
                $"LEFT JOIN Series s ON bs.SeriesID = s.SeriesID " +
                $"LEFT JOIN Lent l ON b.BookID = l.BookID " +
                $"{whereClause} " +
                $"ORDER BY s.Name ASC, bs.NoInSeries ASC, b.Title ASC";

            return cmd;
        }

        internal void InitializeDGV()
        {
            mDataGridViewSearch.ColumnCount = 8;
            mDataGridViewSearch.Columns[0].Name = "Titel";
            mDataGridViewSearch.Columns[1].Name = "Untertitel";
            mDataGridViewSearch.Columns[2].Name = "Serie";
            mDataGridViewSearch.Columns[3].Name = "Nr.";
            mDataGridViewSearch.Columns[4].Name = "Autor_in";
            mDataGridViewSearch.Columns[5].Name = "Veröffentlicht";
            mDataGridViewSearch.Columns[6].Name = "Format";
            mDataGridViewSearch.Columns[7].Name = "ISBN";
            mDataGridViewSearch.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            mDataGridViewSearch.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        private void FillDGV()
        {

            if (mDataGridViewSearch.Rows.Count > 0)
            {
                mDataGridViewSearch.Rows.Clear();
                mDataGridViewSearch.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            else
            {
                mDataGridViewSearch.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            }

            List<int> bookIDs = new List<int>();
            foreach (DBReader.SearchStruct search in mSearched)
            {
                if (!mChkBoxOnlyShowFirstAuthor.Checked || !bookIDs.Contains(search.BookID))
                {
                    DataGridViewRow dgvRow = CreateDGVRow(search);
                    mDataGridViewSearch.Rows.Add(dgvRow);
                    bookIDs.Add(search.BookID);
                } // if
            } // foreach
        }

        private DataGridViewRow CreateDGVRow(DBReader.SearchStruct searched)
        {
            DataGridViewRow dgvRow = new DataGridViewRow();
            DataGridViewCell dgvCellTitle = new DataGridViewTextBoxCell();
            dgvCellTitle.Value = searched.Title;
            dgvRow.Cells.Add(dgvCellTitle);
            DataGridViewCell dgvCellSubTitle = new DataGridViewTextBoxCell();
            dgvCellSubTitle.Value = searched.SubTitle;
            dgvRow.Cells.Add(dgvCellSubTitle);
            DataGridViewCell dgvCellSeries = new DataGridViewTextBoxCell();
            dgvCellSeries.Value = searched.Series;
            dgvRow.Cells.Add(dgvCellSeries);
            DataGridViewCell dgvCellNoInSeries = new DataGridViewTextBoxCell();
            dgvCellNoInSeries.Value = searched.NoInSeries;
            dgvRow.Cells.Add(dgvCellNoInSeries);
            DataGridViewCell dgvCellAuthor = new DataGridViewTextBoxCell();
            dgvCellAuthor.Value = $"{searched.AuthorPreName} {searched.AuthorSurName}";
            dgvRow.Cells.Add(dgvCellAuthor);
            DataGridViewCell dgvCellPublishingDate = new DataGridViewTextBoxCell();
            dgvCellPublishingDate.Value = searched.PublishingDate;
            dgvRow.Cells.Add(dgvCellPublishingDate);
            DataGridViewCell dgvCellFormet = new DataGridViewTextBoxCell();
            dgvCellFormet.Value = searched.Format;
            dgvRow.Cells.Add(dgvCellFormet);
            DataGridViewCell dgvCellISBN = new DataGridViewTextBoxCell();
            dgvCellISBN.Value = searched.ISBN13 != "" ? searched.ISBN13 : searched.ISBN10;
            dgvRow.Cells.Add(dgvCellISBN);
            if (searched.IsLent)
            {
                dgvRow.DefaultCellStyle.BackColor = Color.LightYellow;
            }

            return dgvRow;
        }

        internal void InitializeCmbBoxFormat()
        {
            DBReader dbReader = new DBReader();
            List<string> formats = dbReader.GetAllFormats();
            formats.Insert(0, "");
            mCmbBoxFormat.Items.AddRange(formats.ToArray());
        }

        #endregion
    }
}
