using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System;

namespace ISBNCaller_Lib
{
    internal class ISBNWorkerOpenLibrary : ISBNWorker
    {
        internal BookStruct AskForISBN(string isbn, out bool ok)
        {
            ok = false;
            IRest_JSON rest_JSON = new IRest_JSON();
            string response = rest_JSON.CallRest($@"https://openlibrary.org/isbn/{isbn}.json", false);
            if (response.StartsWith("An error occured:"))
            {
                if (response.Contains("(404) Nicht gefunden"))
                {
                    return new BookStruct();
                }
                else
                {
                    throw new Exception(response);
                }
            } // if

            List<string> authorCodes;
            BookStruct book = ParseBook(response, out authorCodes);
            book.Autoren = new List<string>();
            if (authorCodes.Count > 0)
            {
                book.Autoren.AddRange(AskForAuthors(rest_JSON, authorCodes));
            }

            ok = true;

            return book;
        }

        private BookStruct ParseBook(string response, out List<string> authorCodes)
        {
            BookStruct book = new BookStruct();
            authorCodes = new List<string>();
            var json = JObject.Parse(response);
            book.Titel = json["title"].ToString();
            string subTitleKey = "subtitle";
            if (json.ContainsKey(subTitleKey))
            {
                book.Untertitel = json[subTitleKey].ToString();
            }
            else
            {
                book.Untertitel = "";
            }

            string physicalFormatKey = "physical_format";
            if (json.ContainsKey(physicalFormatKey))
            {
                book.Format = json[physicalFormatKey].ToString();
            }
            else
            {
                book.Format = "unbekannt";
            }

            string isbn13Key = "isbn_13";
            if (json.ContainsKey(isbn13Key))
            {
                book.ISBN13 = json[isbn13Key].ToString();
                book.ISBN13 = FormatISBN(book.ISBN13);
            }
            else
            {
                book.ISBN13 = "unbekannt";
            }

            string isbn10Key = "isbn_10";
            if (json.ContainsKey(isbn10Key))
            {
                book.ISBN10 = json[isbn10Key].ToString().Replace("[\"", "").Replace("\"]", "");
                book.ISBN10 = FormatISBN(book.ISBN10);
            }
            else
            {
                book.ISBN10 = "unbekannt";
            }

            string publishDateKey = "publish_date";
            if (json.ContainsKey(publishDateKey))
            {
                book.PublishingDate = json[publishDateKey].ToString();
            }
            else
            {
                book.PublishingDate = "unbekannt";
            }

            string authorsKey = "authors";
            if (json.ContainsKey(authorsKey))
            {
                string authors = json[authorsKey].ToString();
                authors = FormatAuthors(authors);
                authorCodes.AddRange(authors.Split(','));
            }

            book.SerialNo = "";
            book.SeriesName = "";

            return book;
        }

        private string FormatAuthors(string authors)
        {
            authors = FormatISBN(authors);
            authors = authors.Replace("{", "");
            authors = authors.Replace("}", "");
            authors = authors.Replace("key:", "");

            return authors;
        }

        private string FormatISBN(string isbn)
        {
            isbn = isbn.Replace("[", "");
            isbn = isbn.Replace("\r\n", "");
            isbn = isbn.Replace("\"", "");
            isbn = isbn.Replace("]", "");
            isbn = isbn.Replace(" ", "");

            return isbn;
        }

        private List<string> AskForAuthors(IRest_JSON rest_JSON, List<string> authorCodes)
        {
            List<string> authorsList = new List<string>();
            foreach (string authorCode in authorCodes)
            {
                string response = rest_JSON.CallRest($@"https://openlibrary.org{authorCode}.json", false);
                string author = ParseAuthors(response);
                authorsList.Add(author);
            } // foreach


            return authorsList;
        }

        private string ParseAuthors(string response)
        {
            string author = "";
            JObject json = JObject.Parse(response);
            if (json.ContainsKey("name"))
            {
                author = json["name"].ToString();
            } // if

            return author;
        }
    }
}
