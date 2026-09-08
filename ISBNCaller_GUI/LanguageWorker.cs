using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ISBNCaller_GUI
{
    internal class LanguageWorker
    {
        #region Variables

        private string mTargetLanguage { get; set; }
        private string mLanguagesFilePath { get; set; }
        private Form mForm { get; set; }

        #endregion

        internal LanguageWorker(string targetLanguage, string languagesFilePath, Form form)
        {
            mTargetLanguage = targetLanguage;
            mLanguagesFilePath = languagesFilePath;
            mForm = form;
        }

        #region Get and set texts

        public void LoadTexts()
        {
            List<KeyValuePair<string, string>> allTexts = LoadTextsFromXML();
            SetTexts(allTexts);
        }

        private void SetTexts(List<KeyValuePair<string, string>> allTexts)
        {
            List<Control> controlsList = GetControls(mForm);
            foreach (Control control in controlsList)
            {
                if (control.GetType() == typeof(DataGridView))
                {
                    DataGridView dgv = (DataGridView)control;
                    for (int index = 0; index < dgv.ColumnCount; index++)
                    {
                        string replacement = $"_Col{index}";
                        var matches = from val in allTexts where val.Key == dgv.Name+replacement select val.Value;
                        foreach (var match in matches)
                        {
                            dgv.Columns[index].Name = match.ToString();
                        }
                    } // for
                } // if
                else
                {
                    var matches = from val in allTexts where val.Key == control.Name select val.Value;
                    foreach (var match in matches)
                    {
                        control.Text = match.ToString();
                    }
                } // else
            } // foreach
        }

        private List<Control> GetControls(Control form)
        {
            var controlsList = new List<Control>();
            foreach (Control childControl in form.Controls)
            {
                // Recurse child controls.
                controlsList.AddRange(GetControls(childControl));
                controlsList.Add(childControl);
            } // foreach

            return controlsList;
        }

        private List<KeyValuePair<string, string>> LoadTextsFromXML()
        {
            if (!File.Exists(mLanguagesFilePath))
            {
                CreateLanguagesXML();
            } // if

            XmlReader reader = new XmlReader(mLanguagesFilePath);
            System.Xml.XmlNodeList childNodes = reader.ReadChildNodes($"/Languages");
            List<KeyValuePair<string, string>> loadedTexts = new List<KeyValuePair<string, string>>();
            foreach (System.Xml.XmlNode node in childNodes)
            {
                string text = "xxxx";
                bool found = false;
                for (int index = 0; index < node.ChildNodes.Count && !found; index++)
                {
                    if (node.ChildNodes[index].Name == mTargetLanguage)
                    {
                        text = node.ChildNodes[index].InnerText;
                        found = true;
                    }
                } // foreach

                KeyValuePair<string, string> kvp = new KeyValuePair<string, string>(node.Name, text);
                loadedTexts.Add(kvp);
            } // foreach

            return loadedTexts;
        }

        #endregion

        #region Create LabelTexts.xml

        private void CreateLanguagesXML()
        {
            string xmlText = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n<Languages>" +
            "\r\n<!-- SearchTab -->" +
            "\r\n\t<tabPageSearch>\r\n\t\t<de>Suchen</de>\r\n\t\t<en>Search</en>\r\n\t</tabPageSearch>" +
            "\r\n\t<SearchTab_labelISBN>\r\n\t\t<de>ISBN:</de>\r\n\t\t<en>ISBN:</en>\r\n\t</SearchTab_labelISBN>" +
            "\r\n\t<SearchTab_labelTitle>\r\n\t\t<de>Titel:</de>\r\n\t\t<en>Title:</en>\r\n\t</SearchTab_labelTitle>" +
            "\r\n\t<SearchTab_labelSubTitle>\r\n\t\t<de>Untertitel:</de>\r\n\t\t<en>Subtitle:</en>\r\n\t</SearchTab_labelSubTitle>" +
            "\r\n\t<SearchTab_labelAuthorPreName>\r\n\t\t<de>Autor_in Vorname:</de>\r\n\t\t<en>Authors first name:</en>\r\n\t</SearchTab_labelAuthorPreName>" +
            "\r\n\t<SearchTab_labelAuthorSurName>\r\n\t\t<de>Autor_in Nachname:</de>\r\n\t\t<en>Authors family name:</en>\r\n\t</SearchTab_labelAuthorSurName>" +
            "\r\n\t<SearchTab_radioBtnWSeries>\r\n\t\t<de>Mit Serien</de>\r\n\t\t<en>Series included</en>\r\n\t</SearchTab_radioBtnWSeries>" +
            "\r\n\t<SearchTab_radioBtnWOutSeries>\r\n\t\t<de>Ohne Serien</de>\r\n\t\t<en>Without series</en>\r\n\t</SearchTab_radioBtnWOutSeries>" +
            "\r\n\t<SearchTab_labelSeries>\r\n\t\t<de>Serie:</de>\r\n\t\t<en>Series:</en>\r\n\t</SearchTab_labelSeries>" +
            "\r\n\t<SearchTab_labelFormat>\r\n\t\t<de>Format:</de>\r\n\t\t<en>Format:</en>\r\n\t</SearchTab_labelFormat>" +
            "\r\n\t<SearchTab_labelPublishedFrom>\r\n\t\t<de>Veröffentlicht von:</de>\r\n\t\t<en>Published from:</en>\r\n\t</SearchTab_labelPublishedFrom>" +
            "\r\n\t<SearchTab_labelPublishedTo>\r\n\t\t<de>bis:</de>\r\n\t\t<en>to:</en>\r\n\t</SearchTab_labelPublishedTo>" +
            "\r\n\t<SearchTab_chkBoxShowLent>\r\n\t\t<de>Verliehene mit anzeigen</de>\r\n\t\t<en>Show including lent</en>\r\n\t</SearchTab_chkBoxShowLent>" +
            "\r\n\t<SearchTab_chkBoxOnlyShowFirstAuthor>\r\n\t\t<de>Nur erste_n Autor_in anzeigen</de>\r\n\t\t<en>Only show first author</en>\r\n\t</SearchTab_chkBoxOnlyShowFirstAuthor>" +
            "\r\n\t<SearchTab_btnCorrection>\r\n\t\t<de>Korrigieren</de>\r\n\t\t<en>Correct</en>\r\n\t</SearchTab_btnCorrection>" +
            "\r\n\t<SearchTab_btnSearch>\r\n\t\t<de>Suchen</de>\r\n\t\t<en>Search</en>\r\n\t</SearchTab_btnSearch>" +
            "\r\n\t<SearchTab_chkBoxUseDates>\r\n\t\t<de>Daten nutzen</de>\r\n\t\t<en>Use Dates</en>\r\n\t</SearchTab_chkBoxUseDates>" +
            "\r\n\t<SearchTab_dataGridViewSearch_Col0>\r\n\t\t<de>Titel</de>\r\n\t\t<en>Title</en>\r\n\t</SearchTab_dataGridViewSearch_Col0>" +
            "\r\n\t<SearchTab_dataGridViewSearch_Col1>\r\n\t\t<de>Untertitel</de>\r\n\t\t<en>Subtitle</en>\r\n\t</SearchTab_dataGridViewSearch_Col1>" +
            "\r\n\t<SearchTab_dataGridViewSearch_Col2>\r\n\t\t<de>Serie</de>\r\n\t\t<en>Series</en>\r\n\t</SearchTab_dataGridViewSearch_Col2>" +
            "\r\n\t<SearchTab_dataGridViewSearch_Col3>\r\n\t\t<de>Nr.</de>\r\n\t\t<en>No</en>\r\n\t</SearchTab_dataGridViewSearch_Col3>" +
            "\r\n\t<SearchTab_dataGridViewSearch_Col4>\r\n\t\t<de>Autor_in</de>\r\n\t\t<en>Author</en>\r\n\t</SearchTab_dataGridViewSearch_Col4>" +
            "\r\n\t<SearchTab_dataGridViewSearch_Col5>\r\n\t\t<de>Veröffentlicht</de>\r\n\t\t<en>Published</en>\r\n\t</SearchTab_dataGridViewSearch_Col5>" +
            "\r\n\t<SearchTab_dataGridViewSearch_Col6>\r\n\t\t<de>Format</de>\r\n\t\t<en>Format</en>\r\n\t</SearchTab_dataGridViewSearch_Col6>" +
            "\r\n\t<SearchTab_dataGridViewSearch_Col7>\r\n\t\t<de>ISBN</de>\r\n\t\t<en>ISBN</en>\r\n\t</SearchTab_dataGridViewSearch_Col7>" +
            "\r\n<!-- WriteTab -->" +
            "\r\n\t<tabPageWrite>\r\n\t\t<de>Eintragen</de>\r\n\t\t<en>New entry</en>\r\n\t</tabPageWrite>" +
            "\r\n\t<WriteTab_ISBNLabel_1>\r\n\t\t<de>ISBN:</de>\r\n\t\t<en>ISBN:</en>\r\n\t</WriteTab_ISBNLabel_1>" +
            "\r\n\t<WriteTab_btnRegisterWOutISBN>\r\n\t\t<de>Eintragen ohne ISBN</de>\r\n\t\t<en>Fill in without ISBN</en>\r\n\t</WriteTab_btnRegisterWOutISBN>" +
            "\r\n\t<WorkInProgressLabel>\r\n\t\t<de>Verarbeitung läuft...</de>\r\n\t\t<en>Work in progress...</en>\r\n\t</WorkInProgressLabel>" +
            "\r\n\t<WriteTab_groupBoxBook>\r\n\t\t<de>Buch</de>\r\n\t\t<en>Book</en>\r\n\t</WriteTab_groupBoxBook>" +
            "\r\n\t<WriteTab_Book_labelTitle>\r\n\t\t<de>Titel:</de>\r\n\t\t<en>Title:</en>\r\n\t</WriteTab_Book_labelTitle>" +
            "\r\n\t<WriteTab_Book_labelSubTitle>\r\n\t\t<de>Untertitel:</de>\r\n\t\t<en>Subtitle:</en>\r\n\t</WriteTab_Book_labelSubTitle>" +
            "\r\n\t<WriteTab_Book_labelPublishingDate>\r\n\t\t<de>Veröffentlicht:</de>\r\n\t\t<en>Published:</en>\r\n\t</WriteTab_Book_labelPublishingDate>" +
            "\r\n\t<WriteTab_Book_labelFormat>\r\n\t\t<de>Format:</de>\r\n\t\t<en>Format:</en>\r\n\t</WriteTab_Book_labelFormat>" +
            "\r\n\t<WriteTab_Book_labelISBN10>\r\n\t\t<de>ISBN 10:</de>\r\n\t\t<en>ISBN 10:</en>\r\n\t</WriteTab_Book_labelISBN10>" +
            "\r\n\t<WriteTab_Book_labelISBN13>\r\n\t\t<de>ISBN 13:</de>\r\n\t\t<en>ISBN 13:</en>\r\n\t</WriteTab_Book_labelISBN13>" +
            "\r\n\t<WriteTab_Book_btnCalculateISBN10>\r\n\t\t<de>Berechnen</de>\r\n\t\t<en>Calculate</en>\r\n\t</WriteTab_Book_btnCalculateISBN10>" +
            "\r\n\t<WriteTab_Book_btnCalculateISBN13>\r\n\t\t<de>Berechnen</de>\r\n\t\t<en>Calculate</en>\r\n\t</WriteTab_Book_btnCalculateISBN13>" +
            "\r\n\t<WriteTab_Book_chkBoxIsPartOfSeries>\r\n\t\t<de>Gehört zu Serie</de>\r\n\t\t<en>Is part of series</en>\r\n\t</WriteTab_Book_chkBoxIsPartOfSeries>" +
            "\r\n\t<WriteTab_Book_labelNoInSeries>\r\n\t\t<de>Nummer in Serie:</de>\r\n\t\t<en>No in series:</en>\r\n\t</WriteTab_Book_labelNoInSeries>" +
            "\r\n\t<WriteTab_Book_labelMaxNo>\r\n\t\t<de>Max No.:</de>\r\n\t\t<en>Max no:</en>\r\n\t</WriteTab_Book_labelMaxNo>" +
            "\r\n\t<WriteTab_Book_chkBoxIsNewSeries>\r\n\t\t<de>Neue Serie</de>\r\n\t\t<en>New series</en>\r\n\t</WriteTab_Book_chkBoxIsNewSeries>" +
            "\r\n\t<WriteTab_btnOK>\r\n\t\t<de>Suchen</de>\r\n\t\t<en>Search</en>\r\n\t</WriteTab_btnOK>" +
            "\r\n\t<WriteTab_btnWriteToDB>\r\n\t\t<de>Eintragen</de>\r\n\t\t<en>Save</en>\r\n\t</WriteTab_btnWriteToDB>" +
            "\r\n\t<WriteTab_btnCancel>\r\n\t\t<de>Abbrechen</de>\r\n\t\t<en>Cancel</en>\r\n\t</WriteTab_btnCancel>" +
            "\r\n\t<WriteTab_groupBoxAuthor>\r\n\t\t<de>Autor_in</de>\r\n\t\t<en>Author</en>\r\n\t</WriteTab_groupBoxAuthor>" +
            "\r\n\t<WriteTab_Author_dataGridViewAuthor_Col0>\r\n\t\t<de>Vorname</de>\r\n\t\t<en>Name</en>\r\n\t</WriteTab_Author_dataGridViewAuthor_Col0>" +
            "\r\n\t<WriteTab_Author_dataGridViewAuthor_Col1>\r\n\t\t<de>Name</de>\r\n\t\t<en>Family name</en>\r\n\t</WriteTab_Author_dataGridViewAuthor_Col1>" +
            "\r\n\t<WriteTab_Author_dataGridViewAuthor_Col2>\r\n\t\t<de>In der DB</de>\r\n\t\t<en>Already in DB</en>\r\n\t</WriteTab_Author_dataGridViewAuthor_Col2>" +
            "\r\n<!-- LentTab -->" +
            "\r\n\t<tabPageLent>\r\n\t\t<de>Verleihen</de>\r\n\t\t<en>Lent</en>\r\n\t</tabPageLent>" +
            "\r\n\t<LentTab_groupBoxSuchen>\r\n\t\t<de>Suchen</de>\r\n\t\t<en>Search</en>\r\n\t</LentTab_groupBoxSuchen>" +
            "\r\n\t<LentTab_labelISBN>\r\n\t\t<de>ISBN:</de>\r\n\t\t<en>ISBN:</en>\r\n\t</LentTab_labelISBN>" +
            "\r\n\t<LentTab_labelSubTitle>\r\n\t\t<de>Untertitel:</de>\r\n\t\t<en>Subtitle:</en>\r\n\t</LentTab_labelSubTitle>" +
            "\r\n\t<LentTab_labelAuthorPreName>\r\n\t\t<de>Autor_in Vorname:</de>\r\n\t\t<en>Author first name:</en>\r\n\t</LentTab_labelAuthorPreName>" +
            "\r\n\t<LentTab_labelAuthorSurName>\r\n\t\t<de>Nachname:</de>\r\n\t\t<en>Family name:</en>\r\n\t</LentTab_labelAuthorSurName>" +
            "\r\n\t<LentTab_labelTitle>\r\n\t\t<de>Titel:</de>\r\n\t\t<en>Title:</en>\r\n\t</LentTab_labelTitle>" +
            "\r\n\t<LentTab_btnSearch>\r\n\t\t<de>Suche</de>\r\n\t\t<en>Search</en>\r\n\t</LentTab_btnSearch>" +
            "\r\n\t<LentTab_btnPull>\r\n\t\t<de>Übernehmen</de>\r\n\t\t<en>Pull</en>\r\n\t</LentTab_btnPull>" +
            "\r\n\t<LentTab_groupBoxVerleihen>\r\n\t\t<de>Verleihen</de>\r\n\t\t<en>Lent</en>\r\n\t</LentTab_groupBoxVerleihen>" +
            "\r\n\t<LentTab_btnLent>\r\n\t\t<de>Verleihen</de>\r\n\t\t<en>Lent</en>\r\n\t</LentTab_btnLent>" +
            "\r\n\t<LentTab_btnRemove>\r\n\t\t<de>Entfernen</de>\r\n\t\t<en>Remove</en>\r\n\t</LentTab_btnRemove>" +
            "\r\n\t<LentTab_dataGridViewSearch_Col0>\r\n\t\t<de>BookID</de>\r\n\t\t<en>BookID</en>\r\n\t</LentTab_dataGridViewSearch_Col0>" +
            "\r\n\t<LentTab_dataGridViewSearch_Col1>\r\n\t\t<de>Titel</de>\r\n\t\t<en>Title</en>\r\n\t</LentTab_dataGridViewSearch_Col1>" +
            "\r\n\t<LentTab_dataGridViewSearch_Col2>\r\n\t\t<de>Untertitel</de>\r\n\t\t<en>Subtitle</en>\r\n\t</LentTab_dataGridViewSearch_Col2>" +
            "\r\n\t<LentTab_dataGridViewSearch_Col3>\r\n\t\t<de>Autor_in</de>\r\n\t\t<en>Author</en>\r\n\t</LentTab_dataGridViewSearch_Col3>" +
            "\r\n\t<LentTab_dataGridViewSearch_Col4>\r\n\t\t<de>Serie</de>\r\n\t\t<en>Series</en>\r\n\t</LentTab_dataGridViewSearch_Col4>" +
            "\r\n\t<LentTab_dataGridViewLent_Col0>\r\n\t\t<de>BookID</de>\r\n\t\t<en>BookID</en>\r\n\t</LentTab_dataGridViewLent_Col0>" +
            "\r\n\t<LentTab_dataGridViewLent_Col1>\r\n\t\t<de>Titel</de>\r\n\t\t<en>Title</en>\r\n\t</LentTab_dataGridViewLent_Col1>" +
            "\r\n\t<LentTab_dataGridViewLent_Col2>\r\n\t\t<de>Untertitel</de>\r\n\t\t<en>Subtitle</en>\r\n\t</LentTab_dataGridViewLent_Col2>" +
            "\r\n\t<LentTab_dataGridViewLent_Col3>\r\n\t\t<de>Autor_in</de>\r\n\t\t<en>Author</en>\r\n\t</LentTab_dataGridViewLent_Col3>" +
            "\r\n\t<LentTab_dataGridViewLent_Col4>\r\n\t\t<de>Verleihdatum</de>\r\n\t\t<en>Lent Date</en>\r\n\t</LentTab_dataGridViewLent_Col4>" +
            "\r\n\t<LentTab_dataGridViewLent_Col5>\r\n\t\t<de>Vorname</de>\r\n\t\t<en>Name</en>\r\n\t</LentTab_dataGridViewLent_Col5>" +
            "\r\n\t<LentTab_dataGridViewLent_Col6>\r\n\t\t<de>Name (optional)</de>\r\n\t\t<en>Family name (optional)</en>\r\n\t</LentTab_dataGridViewLent_Col6>" +
            "\r\n<!-- ReturnTab -->" +
            "\r\n\t<tabPageReturn>\r\n\t\t<de>Zurücknehmen</de>\r\n\t\t<en>Return</en>\r\n\t</tabPageReturn>" +
            "\r\n\t<ReturnTab_btnShowAll>\r\n\t\t<de>Alle anzeigen</de>\r\n\t\t<en>Show all</en>\r\n\t</ReturnTab_btnShowAll>" +
            "\r\n\t<ReturnTab_chkBoxIgnoreIsActive>\r\n\t\t<de>Aktive ignorieren</de>\r\n\t\t<en>Ignore active</en>\r\n\t</ReturnTab_chkBoxIgnoreIsActive>" +
            "\r\n\t<ReturnTab_GroupBoxSearch>\r\n\t\t<de>Suchen</de>\r\n\t\t<en>Search</en>\r\n\t</ReturnTab_GroupBoxSearch>" +
            "\r\n\t<ReturnTab_labelISBN>\r\n\t\t<de>ISBN:</de>\r\n\t\t<en>ISBN:</en>\r\n\t</ReturnTab_labelISBN>" +
            "\r\n\t<ReturnTab_labelBookTitle>\r\n\t\t<de>Buchtitel:</de>\r\n\t\t<en>Book title:</en>\r\n\t</ReturnTab_labelBookTitle>" +
            "\r\n\t<ReturnTab_labelLentTo>\r\n\t\t<de>Verliehen an:</de>\r\n\t\t<en>Lent to:</en>\r\n\t</ReturnTab_labelLentTo>" +
            "\r\n\t<ReturnTab_labelPreName>\r\n\t\t<de>Vorname:</de>\r\n\t\t<en>First name:</en>\r\n\t</ReturnTab_labelPreName>" +
            "\r\n\t<ReturnTab_labelSurName>\r\n\t\t<de>Nachname:</de>\r\n\t\t<en>Family name:</en>\r\n\t</ReturnTab_labelSurName>" +
            "\r\n\t<ReturnTab_labelOptional>\r\n\t\t<de>(optional)</de>\r\n\t\t<en>(optional)</en>\r\n\t</ReturnTab_labelOptional>" +
            "\r\n\t<ReturnTab_GroupBoxGetBack>\r\n\t\t<de>Rückgabe</de>\r\n\t\t<en>Return</en>\r\n\t</ReturnTab_GroupBoxGetBack>" +
            "\r\n\t<ReturnTab_btnReturn>\r\n\t\t<de>Zurücknehmen</de>\r\n\t\t<en>Return</en>\r\n\t</ReturnTab_btnReturn>" +
            "\r\n\t<ReturnTab_btnSearch>\r\n\t\t<de>Suche</de>\r\n\t\t<en>Search</en>\r\n\t</ReturnTab_btnSearch>" +
            "\r\n\t<ReturnTab_dataGridViewReturn_Col0>\r\n\t\t<de>LentID</de>\r\n\t\t<en>LentID</en>\r\n\t</ReturnTab_dataGridViewReturn_Col0>" +
            "\r\n\t<ReturnTab_dataGridViewReturn_Col1>\r\n\t\t<de>Titel</de>\r\n\t\t<en>Title</en>\r\n\t</ReturnTab_dataGridViewReturn_Col1>" +
            "\r\n\t<ReturnTab_dataGridViewReturn_Col2>\r\n\t\t<de>Untertitel</de>\r\n\t\t<en>Subtitle</en>\r\n\t</ReturnTab_dataGridViewReturn_Col2>" +
            "\r\n\t<ReturnTab_dataGridViewReturn_Col3>\r\n\t\t<de>Serie</de>\r\n\t\t<en>Series</en>\r\n\t</ReturnTab_dataGridViewReturn_Col3>" +
            "\r\n\t<ReturnTab_dataGridViewReturn_Col4>\r\n\t\t<de>Autor_in</de>\r\n\t\t<en>Author</en>\r\n\t</ReturnTab_dataGridViewReturn_Col4>" +
            "\r\n\t<ReturnTab_dataGridViewReturn_Col5>\r\n\t\t<de>Verleihdatum</de>\r\n\t\t<en>Lent Date</en>\r\n\t</ReturnTab_dataGridViewReturn_Col5>" +
            "\r\n\t<ReturnTab_dataGridViewReturn_Col6>\r\n\t\t<de>Vorname</de>\r\n\t\t<en>Name</en>\r\n\t</ReturnTab_dataGridViewReturn_Col6>" +
            "\r\n\t<ReturnTab_dataGridViewReturn_Col7>\r\n\t\t<de>Nachname</de>\r\n\t\t<en>Family name</en>\r\n\t</ReturnTab_dataGridViewReturn_Col7>" +
            "\r\n</Languages>";
            XmlWriter writer = new XmlWriter(mLanguagesFilePath);
            writer.CreateSettingsXML(mLanguagesFilePath, xmlText);
        }

        #endregion
    }
}
