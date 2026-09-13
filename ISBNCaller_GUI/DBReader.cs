using ISBNCaller_Lib;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Xml.Linq;

namespace ISBNCaller.DBWorker
{
    public class DBReader
    {
        #region Variables

        private string mConnection { get; set; }
        private BooksDB mBooksDB { get; set; }

        #endregion
        #region Constructors

        internal DBReader(string connection, BooksDB booksDB)
        {
            mConnection = connection;
            mBooksDB = booksDB;
        }

        #endregion
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
                using (NpgsqlConnection conn = new NpgsqlConnection(mConnection))
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
                using (NpgsqlConnection conn = new NpgsqlConnection(mConnection))
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
            List<Authors> authors = mBooksDB.Authors.ToList();
            List<ISBNWorker.DBAuthorStruct> resAuthors = authors.Where(a => a.Authorid == authorID)
                .Select(x => new ISBNWorker.DBAuthorStruct()
                {
                    AuthorID = x.Authorid,
                    Name = x.Name,
                    PreName = x.Prename
                }).ToList();
            if (authors.Count > 0)
            {
                return resAuthors.First();
            } // if

            return new ISBNWorker.DBAuthorStruct();
        }

        public ISBNWorker.DBAuthorStruct GetAuthorByName(string name)
        {
            List<Authors> authors = mBooksDB.Authors.ToList();
            List<ISBNWorker.DBAuthorStruct> resAuthors = authors.Where(a => a.Name == name)
                .Select(x => new ISBNWorker.DBAuthorStruct()
                {
                    AuthorID = x.Authorid,
                    Name = x.Name,
                    PreName = x.Prename
                }).ToList();
            if (resAuthors.Count > 0)
            {
                return resAuthors.First();
            } // if

            return new ISBNWorker.DBAuthorStruct();
        }

        public List<ISBNWorker.DBAuthorStruct> GetAuthorsByName(string name)
        {
            List<Authors> authors = mBooksDB.Authors.ToList();
            List<ISBNWorker.DBAuthorStruct> resAuthors = authors.Where(a => a.Name == name)
                .Select(x => new ISBNWorker.DBAuthorStruct()
                {
                    AuthorID = x.Authorid,
                    Name = x.Name,
                    PreName = x.Prename
                }).ToList();
            if (resAuthors.Count > 0)
            {
                return resAuthors;
            } // if

            return new List<ISBNWorker.DBAuthorStruct>();
        }

        public List<ISBNWorker.DBAuthorStruct> GetAuthorsByPreName(string name)
        {
            List<Authors> authors = mBooksDB.Authors.ToList();
            List<ISBNWorker.DBAuthorStruct> resAuthors = authors.Where(a => a.Prename == name)
                .Select(x => new ISBNWorker.DBAuthorStruct()
                {
                    AuthorID = x.Authorid,
                    Name = x.Name,
                    PreName = x.Prename
                }).ToList();
            if (resAuthors.Count > 0)
            {
                return resAuthors;
            } // if

            return new List<ISBNWorker.DBAuthorStruct>();
        }

        public ISBNWorker.DBAuthorStruct GetAuthor(string preName, string name)
        {
            List<Authors> authors = mBooksDB.Authors.ToList();
            List<ISBNWorker.DBAuthorStruct> resAuthors = authors.Where(a => a.Name == name && a.Prename == preName)
                .Select(x => new ISBNWorker.DBAuthorStruct()
                {
                    AuthorID = x.Authorid,
                    Name = x.Name,
                    PreName = x.Prename
                }).ToList();
            if (resAuthors.Count > 0)
            {
                return resAuthors.First();
            } // if

            return new ISBNWorker.DBAuthorStruct();
        }

        public List<ISBNWorker.DBAuthorStruct> GetAuthors(List<int> authorIDs)
        {
            List<Authors> authors = mBooksDB.Authors.ToList();
            List<ISBNWorker.DBAuthorStruct> resAuthors = authors.Where(a => authorIDs.Contains(a.Authorid))
                .Select(x => new ISBNWorker.DBAuthorStruct()
                {
                    AuthorID = x.Authorid,
                    Name = x.Name,
                    PreName = x.Prename
                }).ToList();

            return resAuthors;
        }

        public List<ISBNWorker.DBAuthorStruct> GetAuthors(List<string> names)
        {
            List<Authors> authors = mBooksDB.Authors.ToList();
            List<ISBNWorker.DBAuthorStruct> resAuthors = authors.Where(a => names.Contains(a.Name))
                .Select(x => new ISBNWorker.DBAuthorStruct()
                {
                    AuthorID = x.Authorid,
                    Name = x.Name,
                    PreName = x.Prename
                }).ToList();

            return resAuthors;
        }

        public List<ISBNWorker.DBAuthorStruct> GetAuthors(List<KeyValuePair<string, string>> authorNames)
        {
            List<Authors> authors = mBooksDB.Authors.ToList();
            List<string> names = (from kvp in authorNames select kvp.Value).ToList();
            authors = authors.Where(a => names.Contains(a.Name)).ToList();
            List<ISBNWorker.DBAuthorStruct> auths = new List<ISBNWorker.DBAuthorStruct>();
            foreach (Authors author in authors)
            {
                List<KeyValuePair<string, string>> nameList = authorNames.Where(a => a.Value == author.Name).ToList();
                foreach (KeyValuePair<string, string> name in nameList)
                {
                    if (name.Key != "" && name.Key == author.Prename)
                    {
                        ISBNWorker.DBAuthorStruct auth = FillAuthor(author);
                        auths.Add(auth);
                    } // if
                } // foreach
            } // foreach

            return auths;
        }

        public List<ISBNWorker.DBAuthorStruct> GetAuthorsByBookID(int bookID)
        {
            List<ISBNWorker.DBAuthorStruct> authors = mBooksDB.Authors
                .Join(
                    mBooksDB.Bookauthor,
                    a => a.Authorid,
                    ba => ba.Authorid,
                    (a, ba) => new
                    {
                        id = a.Authorid,
                        prename = a.Prename,
                        name = a.Name,
                        deleted = a.Deleted,
                        bookID = ba.Bookid
                    })
                .Where(x => x.bookID == bookID)
                .Select(x => new ISBNWorker.DBAuthorStruct()
                {
                    AuthorID = x.id,
                    Name = x.name,
                    PreName = x.prename
                })
                .ToList();

            return authors;
        }

        private static ISBNWorker.DBAuthorStruct FillAuthor(Authors author)
        {
            ISBNWorker.DBAuthorStruct auth = new ISBNWorker.DBAuthorStruct();
            auth.AuthorID = author.Authorid;
            auth.PreName = author.Prename;
            auth.Name = author.Name;
            return auth;
        }

        #endregion
        #region Book

        public ISBNWorker.DBBookStruct GetBookByBookID(int bookID)
        {
            List<Books> books = mBooksDB.Books.ToList();
            List<ISBNWorker.DBBookStruct> resBooks = books.Where(b => b.Bookid == bookID)
                .Select(x => new ISBNWorker.DBBookStruct()
                {
                    BookID = x.Bookid,
                    PublishingDate = x.Publishingdate,
                    ISBN13 = x.Isbn13,
                    ISBN10 = x.Isbn10,
                    Title = x.Title,
                    Format = x.Format,
                    SubTitle = x.Subtitle,
                    IsPartOfSeries = x.Ispartofseries
                }).ToList();
            if (resBooks.Count > 0)
            {
                ISBNWorker.DBBookStruct b = resBooks.First();
                return b;
            } // if

            return new ISBNWorker.DBBookStruct();
        }

        public List<ISBNWorker.DBBookStruct> GetBooksByBookIDs(List<int> bookIDs)
        {
            List<Books> books = mBooksDB.Books.ToList();
            List<ISBNWorker.DBBookStruct> resBooks = books.Where(b => bookIDs.Contains(b.Bookid))
                .Select(x => new ISBNWorker.DBBookStruct()
                {
                    BookID = x.Bookid,
                    PublishingDate = x.Publishingdate,
                    ISBN13 = x.Isbn13,
                    ISBN10 = x.Isbn10,
                    Title = x.Title,
                    Format = x.Format,
                    SubTitle = x.Subtitle,
                    IsPartOfSeries = x.Ispartofseries
                }).ToList();

            return resBooks;
        }

        public List<ISBNWorker.DBBookStruct> GetBooksByTitle(string title)
        {
            List<Books> books = mBooksDB.Books.ToList();
            List<ISBNWorker.DBBookStruct> resBooks = books.Where(b => b.Title == title)
                .Select(x => new ISBNWorker.DBBookStruct()
                {
                    BookID = x.Bookid,
                    PublishingDate = x.Publishingdate,
                    ISBN13 = x.Isbn13,
                    ISBN10 = x.Isbn10,
                    Title = x.Title,
                    Format = x.Format,
                    SubTitle = x.Subtitle,
                    IsPartOfSeries = x.Ispartofseries
                }).ToList();

            return resBooks;
        }

        public List<ISBNWorker.DBBookStruct> GetBooksByFormat(string format)
        {
            List<Books> books = mBooksDB.Books.ToList();
            List<ISBNWorker.DBBookStruct> resBooks = books.Where(b => b.Format == format)
                .Select(x => new ISBNWorker.DBBookStruct()
                {
                    BookID = x.Bookid,
                    PublishingDate = x.Publishingdate,
                    ISBN13 = x.Isbn13,
                    ISBN10 = x.Isbn10,
                    Title = x.Title,
                    Format = x.Format,
                    SubTitle = x.Subtitle,
                    IsPartOfSeries = x.Ispartofseries
                }).ToList();

            return resBooks;
        }

        public ISBNWorker.DBBookStruct GetBookByISBN(string isbn)
        {
            List<Books> books = mBooksDB.Books.ToList();
            List<ISBNWorker.DBBookStruct> resBooks = new List<ISBNWorker.DBBookStruct>();
            switch (isbn.Length)
            {
                case 10:
                    resBooks = books.Where(b => b.Isbn10 == isbn)
                .Select(x => new ISBNWorker.DBBookStruct()
                {
                    BookID = x.Bookid,
                    PublishingDate = x.Publishingdate,
                    ISBN13 = x.Isbn13,
                    ISBN10 = x.Isbn10,
                    Title = x.Title,
                    Format = x.Format,
                    SubTitle = x.Subtitle,
                    IsPartOfSeries = x.Ispartofseries
                }).ToList(); break;
                case 13:
                    resBooks = books.Where(b => b.Isbn13 == isbn)
                .Select(x => new ISBNWorker.DBBookStruct()
                {
                    BookID = x.Bookid,
                    PublishingDate = x.Publishingdate,
                    ISBN13 = x.Isbn13,
                    ISBN10 = x.Isbn10,
                    Title = x.Title,
                    Format = x.Format,
                    SubTitle = x.Subtitle,
                    IsPartOfSeries = x.Ispartofseries
                }).ToList(); break;
                default: throw new Exception($"Eine ISBN mit {isbn.Length.ToString()} Digits ist ungültig.");
            } // switch

            if (resBooks.Count > 0)
            {
                return resBooks.First();
            }

            return new ISBNWorker.DBBookStruct();
        }

        public List<ISBNWorker.DBBookStruct> GetBooksByAuthorID(int authorID)
        {
            List<ISBNWorker.DBBookStruct> books = mBooksDB.Books
                .Join(
                    mBooksDB.Bookauthor,
                    b => b.Bookid,
                    ba => ba.Bookid,
                    (b, ba) => new
                    {
                        bookID = b.Bookid,
                        publishingDate = b.Publishingdate,
                        deleted = b.Deleted,
                        isbn13 = b.Isbn13,
                        isbn10 = b.Isbn10,
                        title = b.Title,
                        format = b.Format,
                        subTitle = b.Subtitle,
                        isPartOfSeries = b.Ispartofseries,
                        authorID = ba.Authorid
                    })
                .Where(x => x.authorID == authorID)
                .Select(x => new ISBNWorker.DBBookStruct()
                {
                    BookID = x.bookID,
                    PublishingDate = x.publishingDate,
                    ISBN13 = x.isbn13,
                    ISBN10 = x.isbn10,
                    Title = x.title,
                    Format = x.format,
                    SubTitle = x.subTitle,
                    IsPartOfSeries = x.isPartOfSeries
                })
                .ToList();


            return books;
        }

        public List<ISBNWorker.DBBookStruct> GetBooksByAuthorIDs(List<int> authorIDs)
        {
            List<ISBNWorker.DBBookStruct> books = mBooksDB.Books
                .Join(
                    mBooksDB.Bookauthor,
                    b => b.Bookid,
                    ba => ba.Bookid,
                    (b, ba) => new
                    {
                        bookID = b.Bookid,
                        publishingDate = b.Publishingdate,
                        deleted = b.Deleted,
                        isbn13 = b.Isbn13,
                        isbn10 = b.Isbn10,
                        title = b.Title,
                        format = b.Format,
                        subTitle = b.Subtitle,
                        isPartOfSeries = b.Ispartofseries,
                        authorID = ba.Authorid
                    })
                .Where(x => authorIDs.Contains(x.authorID))
                .Select(x => new ISBNWorker.DBBookStruct()
                {
                    BookID = x.bookID,
                    PublishingDate = x.publishingDate,
                    ISBN13 = x.isbn13,
                    ISBN10 = x.isbn10,
                    Title = x.title,
                    Format = x.format,
                    SubTitle = x.subTitle,
                    IsPartOfSeries = x.isPartOfSeries
                })
                .ToList();


            return books;
        }

        public List<ISBNWorker.DBBookStruct> GetBooksByCmd(string cmd)
        {
            List<ISBNWorker.DBBookStruct> books = ReadDBBook(cmd);

            return books;
        }

        public List<string> GetAllFormats()
        {
            List<Books> books = mBooksDB.Books.ToList();
            List<IGrouping<string, Books>> resBooks = books.Where(b => b.Format != null).GroupBy(b => b.Format).ToList();
            List<string> formats = new List<string>();
            foreach (IGrouping<string, Books> resBook in resBooks)
            {
                formats.Add(resBook.Key);
            }

            return formats;
        }

        private List<ISBNWorker.DBBookStruct> ReadDBBook(string cmd)
        {
            List<ISBNWorker.DBBookStruct> bookStructs = new List<ISBNWorker.DBBookStruct>();
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(mConnection))
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
            List<ISBNWorker.DBLentStruct> lents = mBooksDB.Lent
                .Join(
                mBooksDB.Books,
                l => l.Bookid,
                b => b.Bookid,
                (l, b) => new
                {
                    l.Lentid,
                    l.Bookid,
                    l.Prename,
                    l.Surname,
                    l.Lentdate,
                    b.Title,
                    l.Active
                })
                .Where(x => x.Title.Contains(title) && x.Active.Value)
                .Select(x => new ISBNWorker.DBLentStruct()
                {
                    BookID = x.Bookid,
                    LentID = x.Lentid,
                    PreName = x.Prename,
                    SurName = x.Surname,
                    LentDate = x.Lentdate
                })
                .ToList();

            return lents;
        }

        public List<ISBNWorker.DBLentStruct> GetLentByPreName(string preName)
        {
            List<ISBNWorker.DBLentStruct> lents = mBooksDB.Lent
                .Where(x => x.Prename == preName && x.Active.Value)
                .Select(x => new ISBNWorker.DBLentStruct()
                {
                    BookID = x.Bookid,
                    LentID = x.Lentid,
                    PreName = x.Prename,
                    SurName = x.Surname,
                    LentDate = x.Lentdate
                })
                .ToList();

            return lents;
        }

        public List<ISBNWorker.DBLentStruct> GetLentByISBN(string isbn)
        {
            List<ISBNWorker.DBLentStruct> lents = new List<ISBNWorker.DBLentStruct>();
            switch (isbn.Length)
            {
                case 10:
                    lents = mBooksDB.Lent
                        .Join(
                        mBooksDB.Books,
                        l => l.Bookid,
                        b => b.Bookid,
                        (l, b) => new
                        {
                            l.Lentid,
                            l.Bookid,
                            l.Prename,
                            l.Surname,
                            l.Lentdate,
                            b.Isbn10,
                            l.Active
                        })
                        .Where(x => x.Isbn10 == isbn && x.Active.Value)
                        .Select(x => new ISBNWorker.DBLentStruct()
                        {
                            BookID = x.Bookid,
                            LentID = x.Lentid,
                            PreName = x.Prename,
                            SurName = x.Surname,
                            LentDate = x.Lentdate
                        })
                        .ToList(); break;
                case 13:
                    lents = mBooksDB.Lent
                        .Join(
                        mBooksDB.Books,
                        l => l.Bookid,
                        b => b.Bookid,
                        (l, b) => new
                        {
                            l.Lentid,
                            l.Bookid,
                            l.Prename,
                            l.Surname,
                            l.Lentdate,
                            b.Isbn13,
                            l.Active
                        })
                        .Where(x => x.Isbn13 == isbn && x.Active.Value)
                        .Select(x => new ISBNWorker.DBLentStruct()
                        {
                            BookID = x.Bookid,
                            LentID = x.Lentid,
                            PreName = x.Prename,
                            SurName = x.Surname,
                            LentDate = x.Lentdate
                        })
                        .ToList(); break;
                default: throw new Exception("Die ISBN muss 10 oder 13 Stellen lang sein.");
            }

            return lents;
        }

        public List<ISBNWorker.DBLentStruct> GetLentByName(string preName, string name)
        {
            List<ISBNWorker.DBLentStruct> lents = mBooksDB.Lent
                .Where(x => x.Prename == preName && x.Surname == name && x.Active.Value)
                .Select(x => new ISBNWorker.DBLentStruct()
                {
                    BookID = x.Bookid,
                    LentID = x.Lentid,
                    PreName = x.Prename,
                    SurName = x.Surname,
                    LentDate = x.Lentdate
                })
                .ToList();

            return lents;
        }

        public List<ISBNWorker.DBLentStruct> GetAllLents(bool ignoreIsActive = true)
        {
            List<ISBNWorker.DBLentStruct> lents = new List<ISBNWorker.DBLentStruct>();
            if (ignoreIsActive)
            {
                 lents = mBooksDB.Lent
                    .Select(x => new ISBNWorker.DBLentStruct()
                    {
                        BookID = x.Bookid,
                        LentID = x.Lentid,
                        PreName = x.Prename,
                        SurName = x.Surname,
                        LentDate = x.Lentdate
                    })
                    .ToList();
            }
            else
            {
                lents = mBooksDB.Lent
                    .Where(x => x.Active.Value)
                    .Select(x => new ISBNWorker.DBLentStruct()
                    {
                        BookID = x.Bookid,
                        LentID = x.Lentid,
                        PreName = x.Prename,
                        SurName = x.Surname,
                        LentDate = x.Lentdate
                    })
                    .ToList();
            }

            return lents;
        }

        #endregion
        #region Series

        public Dictionary<int, string> GetAllSeries(Dictionary<int, string> series)
        {
            Dictionary<int, string> resSeries = mBooksDB.Series
            .Select(x => new KeyValuePair<int, string>((int)x.Seriesid, x.Name))
            .ToDictionary(x => x.Key, x => x.Value);
            foreach (KeyValuePair<int, string> kvp in resSeries)
            {
                series.Add(kvp.Key, kvp.Value);
            }

            return series;
        }

        public Dictionary<int, string> GetSeriesByBookID(int bookID)
        {
            Dictionary<int, string> series = mBooksDB.Series
                .Join(mBooksDB.Bookseries,
                s => s.Seriesid,
                bs => bs.Seriesid,
                (s, bs) => new
                {
                    bs.Bookid,
                    SeriesID = s.Seriesid,
                    s.Name
                }
                )
                .Where(x => x.Bookid == bookID)
                .Select(x => new KeyValuePair<int, string>((int)x.SeriesID, x.Name))
                .ToDictionary(x => x.Key, x => x.Value);
            
            return series;
        }

        public int GetSeriesIDBySeriesName(string name)
        {
            Dictionary<int, string> resSeries = mBooksDB.Series
                .Where(s => s.Name == name)
                .Select(x => new KeyValuePair<int, string>((int)x.Seriesid, x.Name))
                .ToDictionary(x => x.Key, x => x.Value);
            int seriesID = -1;
            if (resSeries.Count() > 0)
            {
                seriesID = resSeries.First().Key;
            }

            return seriesID;
        }

        public int GetMaxNoInSeries(int seriesID)
        {
            if (seriesID < 0)
            {
                return -1;
            }

            int maxNoInSeries = (int)mBooksDB.Bookseries.Where(s => s.Seriesid == seriesID)
                .OrderByDescending(s => s.Noinseries).First().Noinseries;

            return maxNoInSeries;
        }

        #endregion
    }
}
