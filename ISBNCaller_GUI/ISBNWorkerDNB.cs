using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;

namespace ISBNCaller_Lib
{
    internal class ISBNWorkerDNB : ISBNWorker
    {
        private static string mResponse = "";

        internal BookStruct AskForISBN(string isbn, out bool ok)
        {
            ok = false;
            CallDNB(isbn);

            while (mResponse == "")
            {
                Thread.Sleep(5);
            }

            string response = mResponse;
            mResponse = "";
            BookStruct book;
            ok = ParseBook(response, out book);
            if (ok)
            {
                book = ReplaceEncodingErrors(book);
            }

            return book;
        }

        private bool ParseBook(string response, out BookStruct book)
        {
            book = new BookStruct();
            book.Format = "";
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(response);
            XmlNode recordsNode = xmlDocument.DocumentElement["records"];
            XmlNode recordNode = recordsNode.FirstChild;
            if (recordNode == null) return false;

            XmlNode recordDataNode = GetNodeOfChilds(recordNode, "recordData");
            if (recordDataNode == null) return false;

            XmlNode rdfNode = recordDataNode.FirstChild;
            XmlNode rdfDescriptionNode = rdfNode.FirstChild;
            XmlNode titleNode = GetNodeOfChilds(rdfDescriptionNode, "dc:title");
            if (titleNode == null) return false;

            SplitTitle(titleNode, out string title, out string subTitle);
            book.Titel = title;
            book.Untertitel = subTitle;
            XmlNode isbn13Node = GetNodeOfChilds(rdfDescriptionNode, "bibo:isbn13");
            if (isbn13Node == null) return false;

            book.ISBN13 = isbn13Node.InnerText;
            XmlNode isbn10Node = GetNodeOfChilds(rdfDescriptionNode, "bibo:isbn10");
            if (isbn10Node != null)
            {
                book.ISBN10 = isbn10Node.InnerText;
            }

            XmlNode issuedNode = GetNodeOfChilds(rdfDescriptionNode, "dcterms:issued");
            if (issuedNode != null)
            {
                book.PublishingDate = issuedNode.InnerText;
            }

            XmlNode bibliographicCitationNode = GetNodeOfChilds(rdfDescriptionNode, "dcterms:bibliographicCitation");
            if (bibliographicCitationNode != null)
            {
                GetSeriesInformations(bibliographicCitationNode, out string serialNo, out string seriesName);
                book.SerialNo = serialNo;
                book.SeriesName = seriesName;
            } // if
            else
            {
                book.SerialNo = "";
                book.SeriesName = "";
            }

            book.Autoren = GetAuthors(rdfDescriptionNode);

            return true;
        }

        private void GetSeriesInformations(XmlNode bibliographicCitationNode, out string serialNo, out string seriesName)
        {
            serialNo = "";
            seriesName = "";

            string fullText = bibliographicCitationNode.InnerText;
            string[] split = fullText.Split(';');
            Regex regex = new Regex(@"\d");
            MatchCollection matchCollection = regex.Matches(split[1]);
            foreach (Match match in matchCollection)
            {
                serialNo = match.Value;
            }

            seriesName = split[0].Trim();
        }

        private List<string> GetAuthors(XmlNode rdfDescriptionNode)
        {
            List<string> authors = new List<string>();
            XmlNode p60327 = GetNodeOfChilds(rdfDescriptionNode, "rdau:P60327");
            if (p60327 == null) return authors;

            string author = p60327.InnerText;
            if(author.Contains(";"))
            {
                string[] split = author.Split(';');
                author = split[0].Trim();
            }

            if(author.Contains(","))
            {
                string[] authorsArray = author.Split(',');
                foreach(string auth in authorsArray)
                {
                    authors.Add(auth.Trim());
                }
            } // if
            else
            {
                authors.Add(author);
            }

            return authors;
        }

        private XmlNode GetNodeOfChilds(XmlNode recordNode, string nodeName)
        {
            foreach (XmlNode node in recordNode.ChildNodes)
            {
                if (node.Name == nodeName)
                {
                    return node;
                }
            } // foreach

            return null;
        }

        private void SplitTitle(XmlNode titleNode, out string title, out string subTitle)
        {
            title = titleNode.InnerText;
            subTitle = "";

            if (title.Contains(" â€“ "))
            {
                title = title.Replace("â€“", "-");
            }

            if (title.Contains(" - "))
            {
                string[] split = title.Split('-');
                title = split[0].Trim();
                subTitle = split[1].Trim();
            } // if
        }

        private BookStruct ReplaceEncodingErrors(BookStruct book)
        {
            string word = "";
            if (book.Autoren != null)
            {
                List<string> newAuthors = new List<string>();
                foreach (var autor in book.Autoren)
                {
                    word = ReplaceEncodingErrors(autor);
                    newAuthors.Add(word);
                } // foreach

                book.Autoren = newAuthors;
            } // if

            book.Untertitel = ReplaceEncodingErrors(book.Untertitel);
            book.Titel = ReplaceEncodingErrors(book.Titel);
            book.Format = book.Format != null ? ReplaceEncodingErrors(book.Format) : null;
            book.SeriesName = ReplaceEncodingErrors(book.SeriesName);

            return book;
        }

        private string ReplaceEncodingErrors(string word)
        {
            List<string> errorCases = new List<string>() { "AÌˆ", "aÌˆ", "OÌˆ", "oÌˆ", "UÌˆ", "uÌˆ" };
            if (word == "" | !errorCases.Any(substring => word.Contains(substring)))
                return word;

            if (word.Contains("AÌˆ"))
            {
                word = word.Replace("AÌˆ", "Ä");
            }
            
            if (word.Contains("aÌˆ"))
            {
                word = word.Replace("aÌˆ", "ä");
            }
            
            if (word.Contains("OÌˆ"))
            {
                word = word.Replace("OÌˆ", "Ö");
            }
            
            if (word.Contains("oÌˆ"))
            {
                word = word.Replace("oÌˆ", "ö");
            }
            
            if(word.Contains("UÌˆ"))
            {
                word = word.Replace("UÌˆ", "Ü");
            }
            
            if (word.Contains("uÌˆ"))
            {
                word = word.Replace("uÌˆ", "ü");
            }

            if(word.Contains("ÃŸ"))
            {
                word = word.Replace("ÃŸ", "ß");
            }

            return word;
        }

        private static void CallDNB(string isbn)
        {
            string call = $@"https://services.dnb.de/sru/dnb?version=1.1&operation=searchRetrieve&query={isbn}";
            try
            {
                WebClient webClient = new WebClient();
                mResponse = webClient.DownloadString(call);
            } // try
            catch (Exception ex)
            {
                mResponse = $@"An error occured: {ex.ToString()}";
            } // catch
        }

        /* Zum Testen:
        https://services.dnb.de/sru/dnb?version=1.1&operation=searchRetrieve&query=9783770473267
        https://services.dnb.de/sru/dnb?version=1.1&operation=searchRetrieve&query=9783570166840
        https://services.dnb.de/sru/dnb?version=1.1&operation=searchRetrieve&query=9783847901570

        Mehrere Autoren:
        https://services.dnb.de/sru/dnb?version=1.1&operation=searchRetrieve&query=9783737343220
        https://services.dnb.de/sru/dnb?version=1.1&operation=searchRetrieve&query=9783426522622

        Unbekannt:
        https://services.dnb.de/sru/dnb?version=1.1&operation=searchRetrieve&query=9781473224322

        Allgemein:
        https://services.dnb.de/sru/dnb?version=1.1&operation=searchRetrieve&query={isbn} */


    }
}
