using ISBNCaller.DBWorker;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace ISBNCaller_Lib
{
    public class DBWriter
    {
        #region Variables

        private string mConnection { get; set; }
        private BooksDB mBooksDB { get; set; }

        #endregion
        #region Constructors

        internal DBWriter(string connection, BooksDB booksDB)
        {
            mConnection = connection;
            mBooksDB = booksDB;
        }

        #endregion
        #region Author

        public bool WriteAuthor(ISBNWorker.DBAuthorStruct author)
        {
            bool ok = false;
            Authors auth = new Authors();
            auth.Name = author.Name;
            if (author.PreName != null)
            {
                auth.Prename = author.PreName;
            }

            try
            {
                mBooksDB.Authors.Add(auth);
                mBooksDB.SaveChanges();
                ok = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error wirtign new author to db: {ex.ToString()}");
            }


            return ok;
        }

        public bool WriteAuthors(List<ISBNWorker.DBAuthorStruct> authors)
        {
            bool ok = false;
            try
            {
                foreach (ISBNWorker.DBAuthorStruct author in authors)
                {
                    Authors auth = new Authors();
                    auth.Name = author.Name;
                    if (author.PreName != null)
                    {
                        auth.Prename = author.PreName;
                    }

                    mBooksDB.Authors.Add(auth);
                } // foreach

                mBooksDB.SaveChanges();
                ok = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not write new authors to DB: {ex.ToString()}.");
            }

            return ok;
        }

        #endregion
        #region Book

        public bool WriteBook(ISBNWorker.DBBookStruct book)
        {
            bool ok = false;
            Books b = FillNewBook(book);

            try
            {
                mBooksDB.Books.Add(b);
                mBooksDB.SaveChanges(true);
                ok = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not write new book to DB: {ex.ToString()}.");
            }

            return ok;
        }

        private static Books FillNewBook(ISBNWorker.DBBookStruct book)
        {
            Books b = new Books();
            b.Ispartofseries = book.IsPartOfSeries;
            b.Title = book.Title;
            if (book.SubTitle != null)
            {
                b.Subtitle = book.SubTitle;
            }

            if (book.PublishingDate != null)
            {
                b.Publishingdate = book.PublishingDate;
            }

            if (book.Format != null)
            {
                b.Format = book.Format;
            }

            if (book.ISBN10 != null)
            {
                b.Isbn10 = book.ISBN10;
            }

            if (book.ISBN13 != null)
            {
                b.Isbn13 = book.ISBN13;
            }

            return b;
        }

        public bool WriteBooks(List<ISBNWorker.DBBookStruct> books)
        {
            bool ok = false;
            try
            {
                foreach (ISBNWorker.DBBookStruct book in books)
                {
                    Books b = FillNewBook(book);
                    mBooksDB.Books.Add(b);
                } // foreach

                mBooksDB.SaveChanges(true);
                ok = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Could not write new books to DB: {ex.ToString()}.");
            }

            return ok;
        }

        #endregion
        #region BookAuthor

        public bool WriteBookAuthor(int bookID, int authorID)
        {
            bool ok = false;
            Bookauthor bookauthor = new Bookauthor();
            bookauthor.Bookid = bookID;
            bookauthor.Authorid = authorID;
            try
            {
                mBooksDB.Bookauthor.Add(bookauthor);
                mBooksDB.SaveChanges(true);
                ok = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Could not write new bookauthor to DB: {ex.ToString()}.");
            }

            return ok;
        }

        public bool WriteBookAuthors(int bookID, List<int> authorIDs)
        {
            bool ok = false;
            try
            {
                foreach(int authorID in authorIDs)
                {
                    Bookauthor ba = new Bookauthor();
                    ba.Bookid = bookID;
                    ba.Authorid = authorID;
                    mBooksDB.Bookauthor.Add(ba);
                }

                mBooksDB.SaveChanges(true);
                ok = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Could not write new bookauthors to DB: {ex.ToString()}.");
            }

            return ok;
        }

        #endregion
        #region Lent

        public bool WriteLent(ISBNWorker.DBLentStruct lent)
        {
            bool ok = false;
            Lent l = new Lent();
            l.Bookid = lent.BookID;
            l.Prename = lent.PreName;
            l.Lentdate = new DateTime(lent.LentDate.Year, lent.LentDate.Month, lent.LentDate.Day, lent.LentDate.Hour, lent.LentDate.Minute, lent.LentDate.Second, DateTimeKind.Utc);
            l.Active = true;
            if (lent.SurName != null)
            {
                l.Surname = lent.SurName;
            }

            try
            {
                mBooksDB.Lent.Add(l);
                mBooksDB.SaveChanges(true);
                ok = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Could not write new lent to DB: {ex.ToString()}.");
            }

            return ok;
        }

        public bool WriteLents(List<ISBNWorker.DBLentStruct> lents)
        {
            bool ok = false;
            foreach (ISBNWorker.DBLentStruct lent in lents)
            {
                Lent l = new Lent();
                l.Bookid = lent.BookID;
                l.Prename = lent.PreName;
                l.Lentdate = new DateTime(lent.LentDate.Year, lent.LentDate.Month, lent.LentDate.Day, lent.LentDate.Hour, lent.LentDate.Minute, lent.LentDate.Second, DateTimeKind.Utc);
                l.Active = true;
                if (lent.SurName != null)
                {
                    l.Surname = lent.SurName;
                }

                mBooksDB.Lent.Add(l);
            } // foreach

            try
            {
                mBooksDB.SaveChanges(true);
                ok = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not write new lents to DB: {ex.ToString()}.");
            }

            return ok;
        }

        public bool DeleteLent(ISBNWorker.DBLentStruct lent)
        {
            bool ok = false;
            mBooksDB.Lent.Where(x => x.Lentid == lent.LentID).ToList().ForEach(x => x.Active = false);
            try
            {
                mBooksDB.SaveChanges(true);
                ok = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Could not update lent to DB: {ex.ToString()}.");
            }

            return ok;
        }

        public bool DeleteLents(List<ISBNWorker.DBLentStruct> lents)
        {
            bool ok = false;
            List<int> lentIDs = new List<int>();
            foreach (ISBNWorker.DBLentStruct lent in lents)
            {
                lentIDs.Add(lent.LentID);
            }

            mBooksDB.Lent.Where(x => lentIDs.Contains(x.Lentid)).ToList().ForEach(x => x.Active = false);
            try
            {
                mBooksDB.SaveChanges(true);
                ok = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Could not update lents to DB: {ex.ToString()}.");
            }

            return ok;
        }

        public bool DeleteLents(List<int> lentIDs)
        {
            bool ok = false;
            mBooksDB.Lent.Where(x => lentIDs.Contains(x.Lentid)).ToList().ForEach(x => x.Active = false);
            try
            {
                mBooksDB.SaveChanges(true);
                ok = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not update lents to DB: {ex.ToString()}.");
            }

            return ok;
        }

        #endregion
        #region Series

        public bool WriteSeries(string name)
        {
            bool ok = false;
            Series series = new Series();
            series.Name = name;
            try
            {
                mBooksDB.Series.Add(series);
                mBooksDB.SaveChanges(true);
                ok = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not write new series to DB: {ex.ToString()}.");
            }

            return ok;
        }

        #endregion
        #region BookSeries

        public bool WriteBookSeries(int bookID, int seriesID)
        {
            bool ok = false;
            Bookseries bs = new Bookseries();
            bs.Bookid = bookID;
            bs.Seriesid = seriesID;
            try
            {
                mBooksDB.Bookseries.Add(bs);
                mBooksDB.SaveChanges(true);
                ok = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Could not write new bookseries to DB: {ex.ToString()}.");
            }

            return ok;
        }

        public bool WriteBookSeries(int bookID, int seriesID, int noInSeries)
        {
            bool ok = false;
            Bookseries bs = new Bookseries();
            bs.Bookid = bookID;
            bs.Seriesid = seriesID;
            bs.Noinseries = noInSeries;
            try
            {
                mBooksDB.Bookseries.Add(bs);
                mBooksDB.SaveChanges(true);
                ok = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not write new bookseries to DB: {ex.ToString()}.");
            }

            return ok;
        }

        #endregion
    }
}
