using Npgsql;
using System;
using System.Collections.Generic;

namespace ISBNCaller_Lib
{
    public class DBReader
    {
#if DEBUG || WITHOUTLANGUAGESELECTION_DEBUG
        private const string c_connection = "Host=localhost;Username=postgres;Password=aur7eh;Database=BooksDB_Test";
#else
        private const string c_connection = "Host=localhost;Username=postgres;Password=aur7eh;Database=BooksDB";
#endif

        #region Search

        public struct SearchStruct
        {
            public int BookID;
            public string Title;
            public string SubTitle;
            public string Series;
            public string NoInSeries;
            public string AuthorPreName;
            public string AuthorSurName;
            public string PublishingDate;
            public string Format;
            public string ISBN13;
            public string ISBN10;
            public bool IsLent;
        }

        public List<SearchStruct> GetSearched(string cmd)
        {
            List<SearchStruct> searched = ReadDBForSearch(cmd);

            return searched;
        }

        public List<string> GetBooksFromDateRange(string from, string to)
        {
            string cmd = $"SELECT * FROM Getalldates({from}, {to})";
            List<string> books = ReadDBForDatesSearch(cmd);

            return books;
        }

        private List<SearchStruct> ReadDBForSearch(string cmd)
        {
            List<SearchStruct> searchStructs = new List<SearchStruct>();
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(c_connection))
                {
                    conn.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand(cmd, conn))
                    {
                        NpgsqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                searchStructs.Add(FillSearchStruct(reader));
                            }
                        } // if
                    } // using
                } // using
            } // try
            catch (Exception ex)
            {

            }

            return searchStructs;
        }

        private List<string> ReadDBForDatesSearch(string cmd)
        {
            List<string> bookIDs = new List<string>();
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(c_connection))
                {
                    conn.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand(cmd, conn))
                    {
                        NpgsqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                bookIDs.Add(reader.GetFieldValue<int>(0).ToString());
                            }
                        } // if
                    } // using
                } // using
            } // try
            catch (Exception ex)
            {

            }

            return bookIDs;
        }

        private SearchStruct FillSearchStruct(NpgsqlDataReader reader)
        {
            SearchStruct search = new SearchStruct();
            search.Title = reader.GetFieldValue<string>(0);
            search.SubTitle = reader.IsDBNull(1) ? "" : reader.GetFieldValue<string>(1);
            search.NoInSeries = reader.IsDBNull(2) ? "" : reader.GetFieldValue<int>(2).ToString();
            search.Series = reader.IsDBNull(3) ? "" : reader.GetFieldValue<string>(3);
            search.AuthorPreName = reader.IsDBNull(4) ? "" : reader.GetFieldValue<string>(4);
            search.AuthorSurName = reader.IsDBNull(5) ? "" : reader.GetFieldValue<string>(5);
            search.PublishingDate = reader.IsDBNull(6) ? "" : reader.GetFieldValue<string>(6);
            search.Format = reader.IsDBNull(7) ? "" : reader.GetFieldValue<string>(7);
            search.ISBN10 = reader.IsDBNull(8) ? "" : reader.GetFieldValue<string>(8);
            search.ISBN13 = reader.IsDBNull(9) ? "" : reader.GetFieldValue<string>(9);
            search.IsLent = reader.IsDBNull(10) ? false : reader.GetFieldValue<bool>(10);
            search.BookID = reader.GetFieldValue<int>(11);
            
            return search;
        }

        #endregion
        #region Author

        public ISBNWorker.DBAuthorStruct GetAuthor(int authorID)
        {
            string cmd = $"SELECT AuthorID, PreName, Name FROM Authors WHERE AuthorID={authorID}";
            List<ISBNWorker.DBAuthorStruct> author = ReadDBAuthor(cmd);
            if (author.Count > 0)
            {
                return author[0];
            }

            return new ISBNWorker.DBAuthorStruct();
        }

        public ISBNWorker.DBAuthorStruct GetAuthorByName(string name)
        {
            string cmd = $"SELECT DISTINCT AuthorID, PreName, Name FROM Authors WHERE Name='{name}'";
            List<ISBNWorker.DBAuthorStruct> author = ReadDBAuthor(cmd);
            if (author.Count > 0)
            {
                return author[0];
            }

            return new ISBNWorker.DBAuthorStruct();
        }

        public ISBNWorker.DBAuthorStruct GetAuthorByPreName(string name)
        {
            string cmd = $"SELECT DISTINCT AuthorID, PreName, Name FROM Authors WHERE PreName='{name}'";
            List<ISBNWorker.DBAuthorStruct> author = ReadDBAuthor(cmd);
            if (author.Count > 0)
            {
                return author[0];
            }

            return new ISBNWorker.DBAuthorStruct();
        }

        public ISBNWorker.DBAuthorStruct GetAuthor(string preName, string name)
        {
            string cmd = $"SELECT AuthorID, PreName, Name FROM Authors WHERE Name='{name}' AND PreName='{preName}'";
            List<ISBNWorker.DBAuthorStruct> author = ReadDBAuthor(cmd);
            if (author.Count > 0)
            {
                return author[0];
            }

            return new ISBNWorker.DBAuthorStruct();
        }

        public List<ISBNWorker.DBAuthorStruct> GetAuthors(List<int> authorIDs)
        {
            string cmd = $"SELECT AuthorID, PreName, Name FROM Authors WHERE AuthorID IN ({string.Join(",", authorIDs)})";
            List<ISBNWorker.DBAuthorStruct> authors = ReadDBAuthor(cmd);

            return authors;
        }

        public List<ISBNWorker.DBAuthorStruct> GetAuthors(List<string> names)
        {
            string cmd = $"SELECT AuthorID, PreName, Name FROM Authors WHERE Name IN ('{string.Join("','", names)}')";
            List<ISBNWorker.DBAuthorStruct> authors = ReadDBAuthor(cmd);

            return authors;
        }

        public List<ISBNWorker.DBAuthorStruct> GetAuthors(List<KeyValuePair<string, string>> authorNames)
        {
            List<string> names = new List<string>();
            foreach (KeyValuePair<string, string> authorName in authorNames)
            {
                if (authorName.Key == "")
                {
                    names.Add($"(Name='{authorName.Value}')");
                }
                else
                {
                    names.Add($"(PreName='{authorName.Key}' AND Name='{authorName.Value}')");
                }
            }

            string cmd = $"SELECT AuthorID, PreName, Name FROM Authors WHERE {string.Join(" OR ", names)}";
            List<ISBNWorker.DBAuthorStruct> authors = ReadDBAuthor(cmd);

            return authors;
        }

        public List<ISBNWorker.DBAuthorStruct> GetAuthorsByBookID(int bookID)
        {
            string cmd = $"SELECT AuthorID, PreName, Name FROM Authors WHERE AuthorID IN (SELECT AuthorID FROM BookAuthor WHERE BookID = {bookID} ORDER BY AuthorID ASC);";
            List<ISBNWorker.DBAuthorStruct> authors = ReadDBAuthor(cmd);

            return authors;
        }

        private List<ISBNWorker.DBAuthorStruct> ReadDBAuthor(string cmd)
        {
            List<ISBNWorker.DBAuthorStruct> authorStructs = new List<ISBNWorker.DBAuthorStruct>();
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(c_connection))
                {
                    conn.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand(cmd, conn))
                    {
                        NpgsqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                authorStructs.Add(FillAuthorStruct(reader));
                            }
                        } // if
                    } // using
                } // using
            } // try
            catch (Exception ex)
            {

            }

            return authorStructs;
        }

        private ISBNWorker.DBAuthorStruct FillAuthorStruct(NpgsqlDataReader reader)
        {
            ISBNWorker.DBAuthorStruct author = new ISBNWorker.DBAuthorStruct();
            author.AuthorID = reader.GetFieldValue<int>(0);
            author.PreName = reader.IsDBNull(1) ? null : reader.GetFieldValue<string>(1);
            author.Name = reader.GetFieldValue<string>(2);

            return author;
        }

        #endregion
        #region Book

        public ISBNWorker.DBBookStruct GetBookByBookID(int bookID)
        {
            string cmd = $"SELECT BookID, Title, SubTitle, PublishingDate, Format, ISBN13, ISBN10, IsPartOfSeries FROM Books WHERE BookID={bookID}";
            List<ISBNWorker.DBBookStruct> books = ReadDBBook(cmd);
            if (books.Count > 0)
            {
                return books[0];
            }

            return new ISBNWorker.DBBookStruct();
        }

        public List<ISBNWorker.DBBookStruct> GetBookByBookIDs(List<int> bookIDs)
        {
            string cmd = $"SELECT BookID, Title, SubTitle, PublishingDate, Format, ISBN13, ISBN10, IsPartOfSeries FROM Books WHERE BookID IN ({string.Join(",", bookIDs)})";
            List<ISBNWorker.DBBookStruct> books = ReadDBBook(cmd);

            return books;
        }

        public List<ISBNWorker.DBBookStruct> GetBookByTitle(string title)
        {
            string cmd = $"SELECT BookID, Title, SubTitle, PublishingDate, Format, ISBN13, ISBN10, IsPartOfSeries FROM Books WHERE Title='{title}'";
            List<ISBNWorker.DBBookStruct> books = ReadDBBook(cmd);

            return books;

        }

        public ISBNWorker.DBBookStruct GetBookByFormat(string format)
        {
            string cmd = $"SELECT BookID, Title, SubTitle, PublishingDate, Format, ISBN13, ISBN10, IsPartOfSeries FROM Books WHERE Format='{format}'";
            List<ISBNWorker.DBBookStruct> books = ReadDBBook(cmd);
            if (books.Count > 0)
            {
                return books[0];
            }

            return new ISBNWorker.DBBookStruct();
        }

        public ISBNWorker.DBBookStruct GetBookByISBN(string isbn)
        {
            switch (isbn.Length)
            {
                case 10: isbn = $"ISBN10 = '{isbn}'"; break;
                case 13: isbn = $"ISBN13 = '{isbn}'"; break;
                default: throw new Exception($"Eine ISBN mit {isbn.Length.ToString()} Digits ist ungültig.");
            } // switch

            string cmd = $"SELECT BookID, Title, SubTitle, PublishingDate, Format, ISBN13, ISBN10, IsPartOfSeries FROM Books WHERE {isbn}";
            List<ISBNWorker.DBBookStruct> books = ReadDBBook(cmd);
            if (books.Count > 0)
            {
                return books[0];
            }

            return new ISBNWorker.DBBookStruct();
        }

        public List<ISBNWorker.DBBookStruct> GetBookByAuthorID(int authorID)
        {
            string cmd = $"SELECT b.BookID, Title, SubTitle, PublishingDate, Format, ISBN13, ISBN10, IsPartOfSeries FROM Books b INNER JOIN BookAuthor ba ON b.BookID=ba.BookID WHERE AuthorID={authorID}";
            List<ISBNWorker.DBBookStruct> books = ReadDBBook(cmd);

            return books;
        }

        public List<ISBNWorker.DBBookStruct> GetBooksByCmd(string cmd)
        {
            List<ISBNWorker.DBBookStruct> books = ReadDBBook(cmd);

            return books;
        }

        public List<string> GetAllFormats()
        {
            string cmd = "SELECT Format FROM Books WHERE Format IS NOT NULL GROUP BY Format";
            List<string> formats = ReadTextDBBook(cmd);
            
            return formats;
        }

        private List<ISBNWorker.DBBookStruct> ReadDBBook(string cmd)
        {
            List<ISBNWorker.DBBookStruct> bookStructs = new List<ISBNWorker.DBBookStruct>();
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(c_connection))
                {
                    conn.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand(cmd, conn))
                    {
                        NpgsqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                bookStructs.Add(FillBookStruct(reader));
                            }
                        } // if
                    } // using
                } // using
            } // try
            catch (Exception ex)
            {

            }

            return bookStructs;
        }

        private List<string> ReadTextDBBook(string cmd)
        {
            List<string> books = new List<string>();
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(c_connection))
                {
                    conn.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand(cmd, conn))
                    {
                        NpgsqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                books.Add(reader.GetFieldValue<string>(0));
                            }
                        } // if
                    } // using
                } // using
            } // try
            catch (Exception ex)
            {

            }

            return books;
        }

        private ISBNWorker.DBBookStruct FillBookStruct(NpgsqlDataReader reader)
        {
            ISBNWorker.DBBookStruct book = new ISBNWorker.DBBookStruct();
            book.BookID = reader.GetFieldValue<int>(0);
            book.Title = reader.GetFieldValue<string>(1);
            book.SubTitle = reader.IsDBNull(2) ? null : reader.GetFieldValue<string>(2);
            book.PublishingDate = reader.IsDBNull(3) ? null : reader.GetFieldValue<string>(3);
            book.Format = reader.IsDBNull(4) ? null : reader.GetFieldValue<string>(4);
            book.ISBN13 = reader.IsDBNull(5) ? null : reader.GetFieldValue<string>(5);
            book.ISBN10 = reader.IsDBNull(6) ? null : reader.GetFieldValue<string>(6);
            book.IsPartOfSeries = reader.GetBoolean(7);

            return book;
        }

        #endregion
        #region Lent

        public List<ISBNWorker.DBLentStruct> GetLentByBook(string title)
        {
            string cmd = $"SELECT LentID, BookID, PreName, SurName, LentDate FROM Lent WHERE BookID IN (SELECT BookID FROM Books WHERE Title LIKE '%{title}%' AND Active IS TRUE)";
            List<ISBNWorker.DBLentStruct> lent = ReadDBLent(cmd);
            
            return lent;
        }

        public List<ISBNWorker.DBLentStruct> GetLentByPreName(string preName)
        {
            string cmd = $"SELECT LentID, BookID, PreName, SurName, LentDate FROM Lent WHERE PreName = '{preName}' AND ACTIVE IS TRUE";
            List<ISBNWorker.DBLentStruct> lents = ReadDBLent(cmd);

            return lents;
        }

        public List<ISBNWorker.DBLentStruct> GetLentByISBN(string isbn)
        {
            string whereClause = "";
            switch(isbn.Length)
            {
                case 10: whereClause = $"ISBN10 = '{isbn}'"; break;
                case 13: whereClause = $"ISBN13 = '{isbn}'"; break;
                default: throw new Exception("Die ISBN muss 10 oder 13 Stellen lang sein.");
            }

            string cmd = $"SELECT LentID, BookID, PreName, SurName, LentDate FROM Lent WHERE BookID IN (SELECT BookID FROM Books WHERE {whereClause}) AND Active IS TRUE;";
            List<ISBNWorker.DBLentStruct> lents = ReadDBLent(cmd);

            return lents;
        }

        public List<ISBNWorker.DBLentStruct> GetLentByName(string preName, string name)
        {
            string cmd = $"SELECT LentID, BookID, PreName, SurName, LentDate FROM Lent WHERE PreName = '{preName}' AND SurName = '{name}' AND Active IS TRUE";
            List<ISBNWorker.DBLentStruct> lents = ReadDBLent(cmd);

            return lents;
        }

        public List<ISBNWorker.DBLentStruct> GetAllLents(bool ignoreIsActive = true)
        {
            string whereClause = ignoreIsActive ? "" : "WHERE Active IS TRUE";
            string cmd = $"SELECT LentID, BookID, PreName, SurName, LentDate FROM Lent {whereClause}";
            List<ISBNWorker.DBLentStruct> lents = ReadDBLent(cmd);

            return lents;
        }

        private List<ISBNWorker.DBLentStruct> ReadDBLent(string cmd)
        {
            List<ISBNWorker.DBLentStruct> lentStructs = new List<ISBNWorker.DBLentStruct>();
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(c_connection))
                {
                    conn.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand(cmd, conn))
                    {
                        NpgsqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                lentStructs.Add(FillLentStruct(reader));
                            }
                        } // if
                    } // using
                } // using
            } // try
            catch (Exception ex)
            {

            }

            return lentStructs;
        }

        private ISBNWorker.DBLentStruct FillLentStruct(NpgsqlDataReader reader)
        {
            ISBNWorker.DBLentStruct lent = new ISBNWorker.DBLentStruct();
            lent.LentID = reader.GetFieldValue<int>(0);
            lent.BookID = reader.GetFieldValue<int>(1);
            lent.PreName = reader.GetFieldValue<string>(2);
            lent.SurName = reader.IsDBNull(3) ? null : reader.GetFieldValue<string>(3);
            lent.LentDate = reader.GetFieldValue<DateTime>(4);

            return lent;
        }

        #endregion
        #region Series

        public Dictionary<int, string> GetAllSeries(Dictionary<int, string> series)
        {
            string cmd = "SELECT SeriesID, Name FROM Series ORDER BY Name ASC;";
            series = ReadDBSeries(cmd, series);

            return series;
        }

        public Dictionary<int, string> GetSeriesByBookID(int bookID, Dictionary<int, string> series)
        {
            string cmd = $"SELECT s.SeriesID, Name FROM Series s INNER JOIN BookSeries bs ON s.SeriesID=bs.SeriesID WHERE BookID={bookID}";
            series = ReadDBSeries(cmd, series);
            return series;
        }

        public int GetSeriesIDBySeriesName(string name, Dictionary<int, string> series)
        {
            string cmd = $"SELECT SeriesID, Name FROM Series WHERE Name ='{name}';";
            series = ReadDBSeries(cmd, series);
            int seriesID = -1;
            foreach(var serie in series)
            {
                seriesID = serie.Key;
            }

            return seriesID;
        }

        public int GetMaxNoInSeries(int seriesID)
        {
            string cmd = $"SELECT MAX(NoInSeries) FROM BookSeries WHERE SeriesID = {seriesID}";
            int maxNoInSeries = -1;
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(c_connection))
                {
                    conn.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand(cmd, conn))
                    {
                        NpgsqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                maxNoInSeries = reader.GetFieldValue<int>(0);
                            } // while
                        } // if
                    } // using
                } // using
            } // try
            catch (Exception ex)
            {

            }

            return maxNoInSeries;
        }

        private Dictionary<int, string> ReadDBSeries(string cmd, Dictionary<int, string> series)
        {
            if (series == null)
            {
                series = new Dictionary<int, string>();
            }

            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(c_connection))
                {
                    conn.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand(cmd, conn))
                    {
                        NpgsqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                series.Add(
                                    reader.GetFieldValue<int>(0),
                                    reader.GetFieldValue<string>(1)
                                );
                            } // while
                        } // if
                    } // using
                } // using
            } // try
            catch (Exception ex)
            {

            }

            return series;
        }

        #endregion
    }
}
