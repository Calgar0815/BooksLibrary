using System.Collections.Generic;
using System.Xml;

namespace ISBNCaller_GUI
{
    internal class XmlReader
    {
        private XmlDocument mXmlDocument { get; set; }

        public XmlReader(string filePath)
        {
            mXmlDocument = new XmlDocument();
            mXmlDocument.Load(filePath);
        }

        public string Read(string nodeName)
        {
            XmlNode node = mXmlDocument.DocumentElement.SelectSingleNode($"{nodeName}");
            string text = node.InnerText;

            return text;
        }

        public XmlNodeList ReadChildNodes(string nodeName)
        {
            XmlNode node = mXmlDocument.DocumentElement.SelectSingleNode($"{nodeName}");
            XmlNodeList childNodes = node.ChildNodes;

            return childNodes;
        }
    }
}
