using Npgsql;
using System;
using System.Collections.Generic;

namespace ISBNCaller_Lib
{
    public class DBWriter
    {
#if DEBUG || WITHOUTLANGUAGESELECTION_DEBUG
        private const string c_connection = "Host=localhost;Username=postgres;Password=aur7eh;Database=BooksDB_Test";
#else
        private const string c_connection = "Host=localhost;Username=postgres;Password=aur7eh;Database=BooksDB";
#endif

        #region Author

        public bool WriteAuthor(ISBNWorker.DBAuthorStruct author)
        {
            string columns = "Name";
            string values = $"'{author.Name}'";
            if (author.PreName != null)
            {
                columns += ", PreName";
                values += $", '{author.PreName}'";
            }

            string cmd = $"INSERT INTO Authors ({columns}) VALUES ({values})";
            bool ok = WriteToDB(cmd);

            return ok;
        }

        public bool WriteAuthors(List<ISBNWorker.DBAuthorStruct> authors)
        {
            string columns = "Name, PreName";
            List<string> values = new List<string>();
            foreach (ISBNWorker.DBAuthorStruct author in authors)
            {
                values.Add(author.PreName == null ? $"('{author.Name}', null)" : $"('{author.Name}', '{author.PreName}')");
            } // foreach

            string cmd = $"INSERT INTO Authors ({columns}) VALUES {string.Join(", ", values)}";
            bool ok = WriteToDB(cmd);

            return ok;
        }

        #endregion
        #region Book

        public bool WriteBook(ISBNWorker.DBBookStruct book)
        {
            string columns = "Title, IsPartOfSeries";
            string istPartOfSeries = book.IsPartOfSeries ? "true" : "false";
            string values = $"'{book.Title}', {istPartOfSeries}";
            if (book.SubTitle != null)
            {
                columns += ", SubTitle";
                values += $", '{book.SubTitle}'";
            }

            if (book.PublishingDate != null)
            {
                columns += ", PublishingDate";
                values += $", '{book.PublishingDate}'";
            }

            if (book.Format != null)
            {
                columns += ", Format";
                values += $", '{book.Format}'";
            }

            if (book.ISBN10 != null)
            {
                columns += ", ISBN10";
                values += $", '{book.ISBN10}'";
            }

            if (book.ISBN13 != null)
            {
                columns += ", ISBN13";
                values += $", '{book.ISBN13}'";
            }

            string cmd = $"INSERT INTO Books ({columns}) VALUES ({values})";
            bool ok = WriteToDB(cmd);

            return ok;
        }

        public bool WriteBooks(List<ISBNWorker.DBBookStruct> books)
        {
            string columns = "Title, SubTitle, PublishingDate, Format, ISBN10, ISBN13, IsPartOfSeries";
            List<string> values = new List<string>();
            foreach (ISBNWorker.DBBookStruct book in books)
            {
                string subTitle = book.SubTitle != null ? $"'{book.SubTitle}'" : "null";
                string publishingDate = book.PublishingDate != null ? $", '{book.PublishingDate}'" : "null";
                string format = book.Format != null ? $", '{book.Format}'" : "null";
                string isbn10 = book.ISBN10 != null ? $", '{book.ISBN10}'" : "null";
                string isbn13 = book.ISBN13 != null ? $", '{book.ISBN13}'" : "null";
                string isPartOfSeries = book.IsPartOfSeries ? "true" : "false";
                values.Add($"('{book.Title}', {subTitle}, {publishingDate}, {format}, {isbn10}, {isbn13}, {isPartOfSeries})");
            } // foreach

            string cmd = $"INSERT INTO Books ({columns}) VALUES {string.Join(", ", values)}";
            bool ok = WriteToDB(cmd);

            return ok;
        }

        #endregion
        #region BookAuthor

        public bool WriteBookAuthor(int bookID, int authorID)
        {
            string cmd = $"INSERT INTO BookAuthor (BookID, AuthorID) VALUES ({bookID}, {authorID})";
            bool ok = WriteToDB(cmd);

            return ok;
        }

        public bool WriteBookAuthors(int bookID, List<int> authorIDs)
        {
            List<string> values = new List<string>();
            foreach (int authorID in authorIDs)
            {
                values.Add($"({bookID}, {authorID})");
            }

            string cmd = $"INSERT INTO BookAuthor (BookID, AuthorID) VALUES {string.Join(", ", values)}";
            bool ok = WriteToDB(cmd);

            return ok;
        }

        #endregion
        #region Lent

        public bool WriteLent(ISBNWorker.DBLentStruct lent)
        {
            string columns = "BookID, PreName, LentDate";
            string values = $"{lent.BookID}, '{lent.PreName}', '{lent.LentDate.ToString("yyyy-MM-dd")}'";
            if (lent.SurName != null)
            {
                columns += ", SurName";
                values += $", '{lent.SurName}'";
            }

            string cmd = $"INSERT INTO Lent ({columns}) VALUES ({values})";
            bool ok = WriteToDB(cmd);

            return ok;
        }

        public bool WriteLents(List<ISBNWorker.DBLentStruct> lents)
        {
            string columns = "BookID, PreName, SurName, LentDate";
            List<string> values = new List<string>();
            foreach (ISBNWorker.DBLentStruct lent in lents)
            {
                values.Add(lent.SurName != null ?
                    $"({lent.BookID}, '{lent.PreName}', '{lent.SurName}', '{lent.LentDate.ToString("yyyy-MM-dd")}')" :
                    $"({lent.BookID}, '{lent.PreName}', null, '{lent.LentDate.ToString("yyyy-MM-dd")}')");
            } // foreach

            string cmd = $"INSERT INTO Lent ({columns}) VALUES {string.Join(", ", values)}";
            bool ok = WriteToDB(cmd);

            return ok;
        }

        public bool DeleteLent(ISBNWorker.DBLentStruct lent)
        {
            string cmd = $"UPDATE Lent SET Active=FALSE WHERE LendID={lent.LentID}";
            bool ok = DeleteFromDB(cmd);

            return ok;
        }

        public bool DeleteLents(List<ISBNWorker.DBLentStruct> lents)
        {
            List<int> lentIDs = new List<int>();
            foreach (ISBNWorker.DBLentStruct lent in lents)
            {
                lentIDs.Add(lent.LentID);
            }

            string cmd = $"UPDATE Lent SET Active=FALSE WHERE LentID IN ({string.Join(",", lentIDs)})";
            bool ok = DeleteFromDB(cmd);

            return ok;
        }

        public bool DeleteLents(List<string> lentIDs)
        {
            string cmd = $"UPDATE Lent SET Active=FALSE WHERE LentID IN ({string.Join(", ", lentIDs)})";
            bool ok = DeleteFromDB(cmd);

            return ok;
        }

        #endregion
        #region Series

        public bool WriteSeries(string name)
        {
            string cmd = $"INSERT INTO Series (Name) VALUES ('{name}');";
            bool ok = WriteToDB(cmd);

            return ok;
        }

        #endregion
        #region BookSeries

        public bool WriteBookSeries(int bookID, int seriesID)
        {
            string cmd = $"INSERT INTO BookSeries (BookID, SeriesID) VALUES ({bookID}, {seriesID});";
            bool ok = WriteToDB(cmd);

            return ok;
        }

        public bool WriteBookSeries(int bookID, int seriesID, int noInSeries)
        {
            string cmd = $"INSERT INTO BookSeries (BookID, SeriesID, NoInSeries) VALUES ({bookID}, {seriesID}, {noInSeries});";
            bool ok = WriteToDB(cmd);

            return ok;
        }

        #endregion
        #region Write to DB

        private bool WriteToDB(string cmd)
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(c_connection))
                {
                    conn.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand(cmd, conn))
                    {
                        command.ExecuteNonQuery();
                    } // using
                } // using
            } // try
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        #endregion
        #region Delete from DB

        private bool DeleteFromDB(string cmd)
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(c_connection))
                {
                    conn.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand(cmd, conn))
                    {
                        command.ExecuteNonQuery();
                    } // using
                } // using
            } // try
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}
