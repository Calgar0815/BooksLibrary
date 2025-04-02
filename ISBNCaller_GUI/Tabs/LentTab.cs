using ISBNCaller_Lib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ISBNCaller_GUI
{
    internal class LentTab
    {
        #region Variables

        internal TextBox mTxtBoxISBN { get; set; }
        internal ComboBox mCmbBoxISBN { get; set; }
        internal TextBox mTxtBoxTitle { get; set; }
        internal ComboBox mCmbBoxTitle { get; set; }
        internal TextBox mTxtBoxSubTitle { get; set; }
        internal ComboBox mCmbBoxSubTitle { get; set; }
        internal TextBox mTxtBoxAuthorPreName { get; set; }
        internal TextBox mTxtBoxAuthorSurName { get; set; }
        internal DataGridView mDataGridViewSearch { get; set; }
        internal Button mBtnSearch { get; set; }
        internal Button mBtnPull { get; set; }
        internal DataGridView mDataGridViewLent { get; set; }
        internal Button mBtnLent { get; set; }
        internal Button mBtnRemove { get; set; }

        private DateTimePicker mDateTimePicker;

        #endregion
        #region Structs

        private struct SearchBooksStruct
        {
            public ISBNWorker.DBBookStruct BookStruct;
            public ISBNWorker.DBAuthorStruct AuthorStruct;
            public Dictionary<int, string> Series;
        }

        #endregion
        #region Methods
        #region Events

        internal void btnSearch_Click()
        {
            DBReader dbReader = new DBReader();
            string cmd = CreateSQLCmd(dbReader);
            if (cmd == "")
            {
                return;
            }

            List<ISBNWorker.DBBookStruct> books = dbReader.GetBooksByCmd(cmd);
            List<SearchBooksStruct> searchBooksStructs = GetSeriesForBooks(books, dbReader);
            searchBooksStructs = GetAuthorForBooks(searchBooksStructs, dbReader);
            if (books.Count > 0)
            {
                FillDGVSearch(searchBooksStructs);
                mBtnPull.Enabled = true;
            }
            else
            {
                MessageBox.Show("Nix gefunden...");
            }
        }

        internal void btnPull_Click()
        {
            DataGridViewSelectedRowCollection rows = mDataGridViewSearch.SelectedRows;
            MoveToDGVLent(rows);
        }

        internal void btnLent_Click()
        {
            try
            {
                bool ok = false;
                DBWriter dbWriter = new DBWriter();
                DataGridViewSelectedRowCollection rows = mDataGridViewLent.SelectedRows;
                bool allRowsSelected = false;
                if (rows.Count == 1 && mDataGridViewLent.Rows.Count == 1)
                {
                    allRowsSelected = true;
                    ISBNWorker.DBLentStruct lent = CreateLentStruct(rows[0]);
                    ok = dbWriter.WriteLent(lent);
                } // if
                else if (rows.Count > 1)
                {
                    List<ISBNWorker.DBLentStruct> lents = new List<ISBNWorker.DBLentStruct>();
                    foreach (DataGridViewRow row in rows)
                    {
                        ISBNWorker.DBLentStruct lent = CreateLentStruct(row);
                        lents.Add(lent);
                    }

                    ok = dbWriter.WriteLents(lents);
                    allRowsSelected = lents.Count == mDataGridViewLent.Rows.Count;
                } // else if
                else if (rows.Count == 0 && mDataGridViewLent.Rows.Count > 0)
                {
                    List<ISBNWorker.DBLentStruct> lents = new List<ISBNWorker.DBLentStruct>();
                    foreach (DataGridViewRow row in mDataGridViewLent.Rows)
                    {
                        ISBNWorker.DBLentStruct lent = CreateLentStruct(row);
                        lents.Add(lent);
                    }

                    ok = dbWriter.WriteLents(lents);
                    allRowsSelected = lents.Count == mDataGridViewLent.Rows.Count;
                } // else if

                if (ok)
                {
                    MessageBox.Show("Alle selektierten Bücher wurden erfolgreich ausgeliehen.", "Erfolg!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CleanUpView(allRowsSelected);
                }
            } // try
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fehler", MessageBoxButtons.OK);
            }
        }

        internal void btnRemove_Click()
        {
            DataGridViewSelectedRowCollection rows = mDataGridViewLent.SelectedRows;
            foreach (DataGridViewRow row in rows)
            {
                mDataGridViewLent.Rows.Remove(row);
                SetRowVisibleOnDGVSearch(row);
            } // foreach

            if (rows.Count == 0)
            {
                DataGridViewSelectedCellCollection cells = mDataGridViewLent.SelectedCells;
                foreach (DataGridViewCell cell in cells)
                {
                    DataGridViewRow row = mDataGridViewLent.Rows[cell.RowIndex];
                    mDataGridViewLent.Rows.Remove(row);
                    SetRowVisibleOnDGVSearch(row);
                } // foreach
            } // if
        }

        private void mDateTimePicker_OnTextChange(object sender, EventArgs e)
        {
            mDataGridViewLent.CurrentCell.Value = mDateTimePicker.Text.ToString();
        }

        private void mDateTimePicker_CloseUp(object sender, EventArgs e)
        {
            mDateTimePicker.Visible = false;
        }

        private void DGVLent_OnCellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Rectangle rect = new Rectangle();
            switch (e.ColumnIndex)
            {
                case 4:
                    #region DatetimePicker

                    mDateTimePicker = new DateTimePicker();
                    //Adding DateTimePicker control into DataGridView
                    mDataGridViewLent.Controls.Add(mDateTimePicker);
                    // Setting the format (i.e. 2014-10-10)  
                    mDateTimePicker.Format = DateTimePickerFormat.Short;
                    // It returns the retangular area that represents the Display area for a cell  
                    Rectangle rect = mDataGridViewLent.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                    //Setting area for DateTimePicker Control  
                    mDateTimePicker.Size = new Size(rect.Width, rect.Height);
                    // Setting Location  
                    mDateTimePicker.Location = new Point(rect.X, rect.Y);
                    // An event attached to dateTimePicker Control which is fired when DateTimeControl is closed  
                    mDateTimePicker.CloseUp += new EventHandler(mDateTimePicker_CloseUp);
                    // An event attached to dateTimePicker Control which is fired when any date is selected  
                    mDateTimePicker.TextChanged += new EventHandler(mDateTimePicker_OnTextChange);
                    // Now make it visible  
                    mDateTimePicker.Visible = true;

                    #endregion

                    break;
            }
        }

        private void DGVLent_OnAddRows(object sender, EventArgs e)
        {
            DGVLent_OnChangeRowsCount();
        }

        private void DGVLent_OnRemoveRows(object sender, EventArgs e)
        {
            DGVLent_OnChangeRowsCount();
        }

        private void DGVLent_OnChangeRowsCount()
        {
            if (mDataGridViewLent.Rows.Count == 0)
            {
                mBtnLent.Enabled = false;
                mBtnRemove.Enabled = false;
            }
            else
            {
                mBtnLent.Enabled = true;
                mBtnRemove.Enabled = true;
            }
        }

        #endregion

        private void SetRowVisibleOnDGVSearch(DataGridViewRow lentRow)
        {
            foreach (DataGridViewRow searchRow in mDataGridViewSearch.Rows)
            {
                if (lentRow.Cells[0].Value == searchRow.Cells[0].Value)
                {
                    searchRow.Visible = true;
                }
            } // foreach
        }

        private string CreateSQLCmd(DBReader dbReader)
        {
            string connectorISBN = mCmbBoxISBN.SelectedIndex == 0 ? "AND" : "OR";
            string connectorTitle = mCmbBoxTitle.SelectedIndex == 0 ? "AND" : "OR";
            string connectorSubTitle = mCmbBoxSubTitle.SelectedIndex == 0 ? "AND" : "OR";
            string whereClause = "";
            if (mTxtBoxISBN.Text != "")
            {
                string isbn = mTxtBoxISBN.Text;
                isbn = isbn.Replace("-", "");
                if (isbn.Length == 10)
                {
                    whereClause += $"ISBN10 = '{isbn}'";
                }
                else if (isbn.Length == 13)
                {
                    whereClause += $"ISBN13 = '{isbn}'";
                }
                else
                {
                    MessageBox.Show("Die eingegebene ISBN muss 10 oder 13 Stellen lang sein.");
                    return "";
                }

                if (mTxtBoxTitle.Text != "" || mTxtBoxSubTitle.Text != "" || mTxtBoxAuthorPreName.Text != "" || mTxtBoxAuthorSurName.Text != "")
                {
                    whereClause += $" {connectorISBN} ";
                }
            } // if

            if (mTxtBoxTitle.Text != "")
            {
                whereClause += $"Title like '%{mTxtBoxTitle.Text}%'";

                if (mTxtBoxSubTitle.Text != "" || mTxtBoxAuthorPreName.Text != "" || mTxtBoxAuthorSurName.Text != "")
                {
                    whereClause += $" {connectorISBN} ";
                }
            } // if

            if (mTxtBoxSubTitle.Text != "")
            {
                whereClause += $"SubTitle like '%{mTxtBoxSubTitle.Text}%'";

                if (mTxtBoxAuthorPreName.Text != "" || mTxtBoxAuthorSurName.Text != "")
                {
                    whereClause += $" {connectorISBN} ";
                }
            } // if

            if (mTxtBoxAuthorPreName.Text != "" || mTxtBoxAuthorSurName.Text != "")
            {
                List<ISBNWorker.DBBookStruct> authorBooks = new List<ISBNWorker.DBBookStruct>();
                if (mTxtBoxAuthorPreName.Text != "" && mTxtBoxAuthorSurName.Text != "")
                {
                    ISBNWorker.DBAuthorStruct author = dbReader.GetAuthor(mTxtBoxAuthorPreName.Text, mTxtBoxAuthorSurName.Text);
                    authorBooks = dbReader.GetBookByAuthorID(author.AuthorID);
                }
                else if (mTxtBoxAuthorPreName.Text != "")
                {
                    ISBNWorker.DBAuthorStruct author = dbReader.GetAuthorByPreName(mTxtBoxAuthorPreName.Text);
                    authorBooks = dbReader.GetBookByAuthorID(author.AuthorID);
                }
                else if (mTxtBoxAuthorSurName.Text != "")
                {
                    ISBNWorker.DBAuthorStruct author = dbReader.GetAuthorByName(mTxtBoxAuthorSurName.Text);
                    authorBooks = dbReader.GetBookByAuthorID(author.AuthorID);
                }

                string[] authorBookIDs = new string[authorBooks.Count];
                int index = 0;
                foreach (ISBNWorker.DBBookStruct authorBook in authorBooks)
                {
                    authorBookIDs[index++] = authorBook.BookID.ToString();
                }

                whereClause += $"BookID IN ({string.Join(",", authorBookIDs)})";
            } // if

            string cmd = $"SELECT b.BookID, b.Title, b.SubTitle, b.PublishingDate, b.Format, b.ISBN10, b.ISBN13, b.IsPartOfSeries FROM Books b WHERE ({whereClause}) AND b.BookID NOT IN (SELECT l.BookID FROM Lent l);";

            return cmd;
        }

        private ISBNWorker.DBLentStruct CreateLentStruct(DataGridViewRow lentDGVRow)
        {
            ISBNWorker.DBLentStruct lent = new ISBNWorker.DBLentStruct();
            if (lentDGVRow.Cells[5].Value == null)
            {
                throw new Exception("Es wurde kein Vorname angegeben. Bitte Vorname angeben.");
            }

            lent.BookID = int.Parse(lentDGVRow.Cells[0].Value.ToString());
            lent.LentDate = DateTime.Parse(lentDGVRow.Cells[4].Value.ToString());
            lent.PreName = lentDGVRow.Cells[5].Value.ToString();
            lent.SurName = lentDGVRow.Cells[6].Value == null ? null : lentDGVRow.Cells[6].Value.ToString();

            return lent;
        }

        internal void InitializeDGVs()
        {
            mDataGridViewSearch.ColumnCount = 5;
            mDataGridViewSearch.Columns[0].Name = "BookID";
            mDataGridViewSearch.Columns[1].Name = "Titel";
            mDataGridViewSearch.Columns[2].Name = "Untertitel";
            mDataGridViewSearch.Columns[3].Name = "Autor_in";
            mDataGridViewSearch.Columns[4].Name = "Serie";
            mDataGridViewSearch.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            mDataGridViewSearch.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            mDataGridViewSearch.Columns[0].Visible = false;

            mDataGridViewLent.ColumnCount = 7;
            mDataGridViewLent.Columns[0].Name = "BookID";
            mDataGridViewLent.Columns[1].Name = "Titel";
            mDataGridViewLent.Columns[2].Name = "Untertitel";
            mDataGridViewLent.Columns[3].Name = "Autor_in";
            mDataGridViewLent.Columns[4].Name = "Verleihdatum";
            mDataGridViewLent.Columns[5].Name = "Vorname";
            mDataGridViewLent.Columns[6].Name = "Nachname (optional)";
            mDataGridViewLent.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            mDataGridViewLent.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            mDataGridViewLent.Columns[0].Visible = false;
            mDataGridViewLent.CellClick += new DataGridViewCellEventHandler(DGVLent_OnCellClick);
            mDataGridViewLent.RowsAdded += new DataGridViewRowsAddedEventHandler(DGVLent_OnAddRows);
            mDataGridViewLent.RowsRemoved += new DataGridViewRowsRemovedEventHandler(DGVLent_OnRemoveRows);
        }

        private List<SearchBooksStruct> GetAuthorForBooks(List<SearchBooksStruct> searchBooksStructs, DBReader dbReader)
        {
            for (int index = 0; index < searchBooksStructs.Count; index++)
            {
                SearchBooksStruct book = searchBooksStructs[index];
                int bookID = book.BookStruct.BookID;
                List<ISBNWorker.DBAuthorStruct> authors = dbReader.GetAuthorsByBookID(bookID);
                var authorStruct = authors.Count > 0 ? authors[0] : new ISBNWorker.DBAuthorStruct();
                book.AuthorStruct = authorStruct;
                searchBooksStructs[index] = book;
            } // for

            return searchBooksStructs;
        }

        private List<SearchBooksStruct> GetSeriesForBooks(List<ISBNWorker.DBBookStruct> books, DBReader dbReader)
        {
            List<SearchBooksStruct> bookStructs = new List<SearchBooksStruct>();
            foreach (ISBNWorker.DBBookStruct book in books)
            {
                SearchBooksStruct bookStruct = new SearchBooksStruct();
                bookStruct.BookStruct = book;
                if (book.IsPartOfSeries)
                {
                    Dictionary<int, string> series = dbReader.GetSeriesByBookID(book.BookID, null);
                    bookStruct.Series = series;
                }

                bookStructs.Add(bookStruct);
            } // foreach

            return bookStructs;
        }

        private void FillDGVSearch(List<SearchBooksStruct> books)
        {
            if (mDataGridViewSearch.Rows.Count > 0)
            {
                mDataGridViewSearch.Rows.Clear();
            }

            foreach (SearchBooksStruct book in books)
            {
                DataGridViewRow dgvRow = CreateDGVSearchRow(book);
                mDataGridViewSearch.Rows.Add(dgvRow);
            } // foreach
        }

        private DataGridViewRow CreateDGVSearchRow(SearchBooksStruct book)
        {
            DataGridViewRow dgvRow = new DataGridViewRow();
            DataGridViewCell dgvCellBookID = new DataGridViewTextBoxCell();
            dgvCellBookID.Value = book.BookStruct.BookID.ToString();
            dgvRow.Cells.Add(dgvCellBookID);
            DataGridViewCell dgvCellTitle = new DataGridViewTextBoxCell();
            dgvCellTitle.Value = book.BookStruct.Title;
            dgvRow.Cells.Add(dgvCellTitle);
            DataGridViewCell dgvCellSubTitle = new DataGridViewTextBoxCell();
            dgvCellSubTitle.Value = book.BookStruct.SubTitle;
            dgvRow.Cells.Add(dgvCellSubTitle);
            DataGridViewCell dgvCellAuthor = new DataGridViewTextBoxCell();
            dgvCellAuthor.Value = $"{book.AuthorStruct.PreName} {book.AuthorStruct.Name}";
            dgvRow.Cells.Add(dgvCellAuthor);
            DataGridViewCell dgvCellSeries = new DataGridViewTextBoxCell();
            string seriesName = "";
            if (book.BookStruct.IsPartOfSeries)
            {
                foreach (KeyValuePair<int, string> entry in book.Series)
                {
                    seriesName = entry.Value;
                }
            } // if

            dgvCellSeries.Value = seriesName;
            dgvRow.Cells.Add(dgvCellSeries);

            return dgvRow;
        }

        private void MoveToDGVLent(DataGridViewSelectedRowCollection searchRows)
        {
            int index = mDataGridViewLent.RowCount;
            foreach (DataGridViewRow searchRow in searchRows)
            {
                DataGridViewRow dgvRow = CreateDGVLentRow(searchRow);
                mDataGridViewLent.Rows.Add(dgvRow);
                searchRow.Visible = false;
            } // foreach
        }

        private DataGridViewRow CreateDGVLentRow(DataGridViewRow searchRow)
        {
            DataGridViewRow dgvRow = new DataGridViewRow();
            DataGridViewCell dgvCellBookID = new DataGridViewTextBoxCell();
            dgvCellBookID.Value = searchRow.Cells[0].Value;
            dgvRow.Cells.Add(dgvCellBookID);
            DataGridViewCell dgvCellTitle = new DataGridViewTextBoxCell();
            dgvCellTitle.Value = searchRow.Cells[1].Value;
            dgvRow.Cells.Add(dgvCellTitle);
            DataGridViewCell dgvCellSubTitle = new DataGridViewTextBoxCell();
            dgvCellSubTitle.Value = searchRow.Cells[2].Value;
            dgvRow.Cells.Add(dgvCellSubTitle);
            DataGridViewCell dgvCellAuthor = new DataGridViewTextBoxCell();
            dgvCellAuthor.Value = searchRow.Cells[3].Value;
            dgvRow.Cells.Add(dgvCellAuthor);
            DataGridViewCell dgvLentDateCell = new DataGridViewTextBoxCell();
            dgvLentDateCell.Value = DateTime.Now.ToShortDateString();
            dgvRow.Cells.Add(dgvLentDateCell);
            DataGridViewCell dgvPreNameCell = new DataGridViewTextBoxCell();
            dgvRow.Cells.Add(dgvPreNameCell);
            DataGridViewCell dgvSurNameCell = new DataGridViewTextBoxCell();
            dgvRow.Cells.Add(dgvSurNameCell);

            return dgvRow;
        }

        private void CleanUpView(bool allRowsSelected)
        {
            if (allRowsSelected)
            {
                mDataGridViewLent.Rows.Clear();
                mDataGridViewSearch.Rows.Clear();
                mTxtBoxISBN.Text = "";
                mCmbBoxISBN.SelectedIndex = 0;
                mTxtBoxTitle.Text = "";
                mCmbBoxTitle.SelectedIndex = 0;
                mTxtBoxSubTitle.Text = "";
                mCmbBoxSubTitle.SelectedIndex = 0;
                mTxtBoxAuthorPreName.Text = "";
                mTxtBoxAuthorSurName.Text = "";
            } // if
            else
            {
                DataGridViewSelectedRowCollection rows = mDataGridViewLent.SelectedRows;
                foreach (DataGridViewRow row in rows)
                {
                    mDataGridViewLent.Rows.Remove(row);
                }
            } // else
        }

        #endregion
    }
}
