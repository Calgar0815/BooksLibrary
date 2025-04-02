using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace ISBNCaller_Lib
{
    public class ISBNWorker
    {
        #region Structs

        public struct DBAuthorStruct
        {
            public int AuthorID;
            public string PreName;
            public string Name;
        }

        public struct DBBookStruct
        {
            public int BookID;
            public string Title;
            public string SubTitle;
            public string PublishingDate;
            public string Format;
            public string ISBN13;
            public string ISBN10;
            public bool IsPartOfSeries;
            public int NoInSeries;
        }

        public struct DBLentStruct
        {
            public int LentID;
            public int BookID;
            public string PreName;
            public string SurName;
            public DateTime LentDate;
        }

        public struct BookStruct
        {
            public string Titel;
            public string Untertitel;
            public string ISBN13;
            public string ISBN10;
            public string Format;
            public string PublishingDate;
            public string SerialNo;
            public string SeriesName;
            public List<string> Autoren;
        }

        #endregion

        public ISBNWorker()
        {

        }

        public BookStruct AskForISBN(string isbn)
        {
            bool ok;
            ISBNWorkerDNB isbnWorkerDNB = new ISBNWorkerDNB();
            BookStruct bookStruct = isbnWorkerDNB.AskForISBN(isbn, out ok);
            if (!ok)
            {
                ISBNWorkerOpenLibrary isbnWorkerOpenLibrary = new ISBNWorkerOpenLibrary();
                bookStruct = isbnWorkerOpenLibrary.AskForISBN(isbn, out ok);
                if(!ok)
                {
                    throw new Exception("Das Buch konnte nicht gefunden werden.");
                }
            } // if
            
            return bookStruct;
        }

        #region Calculate ISBN

        public string CalculateISBN10(string isbn13)
        {
            ISBNCalculator isbnCalculator = new ISBNCalculator();
            string isbn10 = isbnCalculator.CalculateISBN10(isbn13);

            return isbn10;
        }

        public string CalculateISBN13(string isbn10)
        {
            ISBNCalculator isbnCalculator = new ISBNCalculator();
            string isbn13 = isbnCalculator.CalculateISBN13(isbn10);

            return isbn13;
        }

        #endregion
    }
}
