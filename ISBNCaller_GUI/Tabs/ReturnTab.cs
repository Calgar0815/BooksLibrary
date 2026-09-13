

using ISBNCaller.DBWorker;
using ISBNCaller_Lib;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ISBNCaller_GUI
{
    internal class ReturnTab
    {
        #region Variables

        internal Button mBtnShowAll { get; set; }
        internal Button mBtnSearch { get; set; }
        internal Button mBtnReturn { get; set; }
        internal DataGridView mDataGridViewReturn { get; set; }
        internal TextBox mTxtBoxISBN { get; set; }
        internal TextBox mTxtBoxPreName { get; set; }
        internal TextBox mTxtBoxSurName { get; set; }
        internal TextBox mTxtBoxTitle { get; set; }
        internal CheckBox mChkBoxIgnoreIsActive { get; set; }
        private string mDBConnection { get; set; }
        private BooksDB mBooksDB { get; set; }
        private DBReader mDBReader { get; set; }

        #endregion
        #region Structs

        private struct LentStruct
        {
            public int LentID;
            public string Title;
            public string SubTitle;
            public string Series;
            public string Author;
            public string LentDate;
            public string PreName;
            public string SurName;
        }

        #endregion
        #region Constructors

        internal ReturnTab(string dbConnection, BooksDB booksDB)
        {
            mDBConnection = dbConnection;
            mBooksDB = booksDB;
            mDBReader = new DBReader(dbConnection, mBooksDB);
        }

        #endregion
        #region Methods

        internal void InitializeDGVs()
        {
            mDataGridViewReturn.ColumnCount = 8;
            mDataGridViewReturn.Columns[0].Name = "LentID";
            mDataGridViewReturn.Columns[1].Name = "Titel";
            mDataGridViewReturn.Columns[2].Name = "Untertitel";
            mDataGridViewReturn.Columns[3].Name = "Serie";
            mDataGridViewReturn.Columns[4].Name = "Autor_in";
            mDataGridViewReturn.Columns[5].Name = "Verleihdatum";
            mDataGridViewReturn.Columns[6].Name = "Vorname";
            mDataGridViewReturn.Columns[7].Name = "Nachname";
            mDataGridViewReturn.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            mDataGridViewReturn.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            mDataGridViewReturn.Columns[0].Visible = false;
        }

        private void FillDGV(List<LentStruct> lents)
        {
            if (mDataGridViewReturn.Rows.Count > 0)
            {
                mDataGridViewReturn.Rows.Clear();
                mDataGridViewReturn.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }

            foreach (LentStruct lent in lents)
            {
                DataGridViewRow dgvRow = CreateDGVReturnRow(lent);
                mDataGridViewReturn.Rows.Add(dgvRow);
                mDataGridViewReturn.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            } // foreach
        }

        private DataGridViewRow CreateDGVReturnRow(LentStruct lent)
        {
            DataGridViewRow dgvRow = new DataGridViewRow();
            DataGridViewCell dgvCellLentID = new DataGridViewTextBoxCell();
            dgvCellLentID.Value = lent.LentID.ToString();
            dgvRow.Cells.Add(dgvCellLentID);
            DataGridViewCell dgvCellTitle = new DataGridViewTextBoxCell();
            dgvCellTitle.Value = lent.Title;
            dgvRow.Cells.Add(dgvCellTitle);
            DataGridViewCell dgvCellSubTitle = new DataGridViewTextBoxCell();
            dgvCellSubTitle.Value = lent.SubTitle;
            dgvRow.Cells.Add(dgvCellSubTitle);
            DataGridViewCell dgvCellSeries = new DataGridViewTextBoxCell();
            dgvCellSeries.Value = lent.Series;
            dgvRow.Cells.Add(dgvCellSeries);
            DataGridViewCell dgvCellAuthor = new DataGridViewTextBoxCell();
            dgvCellAuthor.Value = lent.Author;
            dgvRow.Cells.Add(dgvCellAuthor);
            DataGridViewCell dgvCellLentDate = new DataGridViewTextBoxCell();
            dgvCellLentDate.Value = lent.LentDate;
            dgvRow.Cells.Add(dgvCellLentDate);
            DataGridViewCell dgvCellPreName = new DataGridViewTextBoxCell();
            dgvCellPreName.Value = lent.PreName;
            dgvRow.Cells.Add(dgvCellPreName);
            DataGridViewCell dgvCellSurName = new DataGridViewTextBoxCell();
            dgvCellSurName.Value = lent.SurName;
            dgvRow.Cells.Add(dgvCellSurName);

            return dgvRow;
        }

        private List<LentStruct> GetAllLentStructs(List<ISBNWorker.DBLentStruct> lents)
        {
            List<LentStruct> lentList = new List<LentStruct>();
            foreach (ISBNWorker.DBLentStruct lent in lents)
            {
                LentStruct lented = new LentStruct();
                lented.LentID = lent.LentID;
                lented.LentDate = lent.LentDate.ToShortDateString();
                lented.PreName = lent.PreName;
                lented.SurName = lent.SurName;
                ISBNWorker.DBBookStruct book = mDBReader.GetBookByBookID(lent.BookID);
                lented.Title = book.Title;
                lented.SubTitle = book.SubTitle;
                List<ISBNWorker.DBAuthorStruct> authors = mDBReader.GetAuthorsByBookID(lent.BookID);
                lented.Author = authors.Count == 0 ? "" : $"{authors[0].PreName} {authors[0].Name}";
                if (book.IsPartOfSeries)
                {
                    Dictionary<int, string> seriesKVP = mDBReader.GetSeriesByBookID(lent.BookID);
                    foreach (KeyValuePair<int, string> entry in seriesKVP)
                    {
                        lented.Series = entry.Value;
                    }
                } // if
                else
                {
                    lented.Series = "";
                }

                lentList.Add(lented);
            } // foreach

            return lentList;
        }

        private void ShowLents(List<ISBNWorker.DBLentStruct> lents)
        {
            List<LentStruct> lentStructs = GetAllLentStructs(lents);
            FillDGV(lentStructs);
        }

        private void CleanUpView(bool allRowsSelected)
        {
            if (allRowsSelected)
            {
                mTxtBoxISBN.Text = "";
                mTxtBoxPreName.Text = "";
                mTxtBoxSurName.Text = "";
                mTxtBoxTitle.Text = "";
                mDataGridViewReturn.Rows.Clear();
                mChkBoxIgnoreIsActive.Checked = false;
                mBtnReturn.Enabled = false;
            } // if
            else
            {
                DataGridViewSelectedRowCollection rows = mDataGridViewReturn.SelectedRows;
                foreach (DataGridViewRow row in rows)
                {
                    mDataGridViewReturn.Rows.Remove(row);
                }
            } // else
        }

        #region Events

        internal void btnShowAllClick()
        {
            bool ignoreIsActive = mChkBoxIgnoreIsActive.Checked;
            List<ISBNWorker.DBLentStruct> lents = mDBReader.GetAllLents(ignoreIsActive);
            ShowLents(lents);
            if (ignoreIsActive)
            {
                mBtnReturn.Enabled = false;
            }
            else if (lents.Count > 0)
            {
                mBtnReturn.Enabled = true;
            }
        }

        internal void btnSearch_Click()
        {
            List<ISBNWorker.DBLentStruct> lents = new List<ISBNWorker.DBLentStruct>();
            if (mTxtBoxTitle.Text != "")
            {
                lents.AddRange(mDBReader.GetLentByBook(mTxtBoxTitle.Text));
            }

            if (mTxtBoxISBN.Text != "")
            {
                List<ISBNWorker.DBLentStruct> nextLents = mDBReader.GetLentByISBN(mTxtBoxISBN.Text);
                foreach (ISBNWorker.DBLentStruct nextLent in nextLents)
                {
                    if (!lents.Contains(nextLent))
                    {
                        lents.Add(nextLent);
                    }
                } // foreach
            } // if

            if (mTxtBoxPreName.Text != "")
            {
                List<ISBNWorker.DBLentStruct> nextLents = mTxtBoxSurName.Text == "" ? mDBReader.GetLentByPreName(mTxtBoxPreName.Text) : mDBReader.GetLentByName(mTxtBoxPreName.Text, mTxtBoxSurName.Text);
                foreach (ISBNWorker.DBLentStruct nextLent in nextLents)
                {
                    if (!lents.Contains(nextLent))
                    {
                        lents.Add(nextLent);
                    }
                } // foreach
            } // if

            ShowLents(lents);
            if (lents.Count > 0)
            {
                mBtnReturn.Enabled = true;
            }
        }

        internal void btnReturn_Click()
        {
            try
            {
                bool ok = false;
                DBWriter dbWriter = new DBWriter(mDBConnection);
                DataGridViewSelectedRowCollection rows = mDataGridViewReturn.SelectedRows;
                bool allRowsSelected = rows.Count == mDataGridViewReturn.Rows.Count;
                List<string> lentIDs = new List<string>();
                foreach (DataGridViewRow row in rows)
                {
                    lentIDs.Add(row.Cells[0].Value.ToString());
                }

                ok = dbWriter.DeleteLents(lentIDs);
                if (ok)
                {
                    MessageBox.Show("Die Rückgabe(n) war(en) erfolgreich.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CleanUpView(allRowsSelected);
                }
                else
                {
                    MessageBox.Show("Die Rückgabe(n) war(en) nicht erfolgreich.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } // try
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fehler", MessageBoxButtons.OK);
            }
        }

        #endregion
        #endregion
    }
}
