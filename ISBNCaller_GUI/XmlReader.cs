using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            XmlNode node = mXmlDocument.DocumentElement.SelectSingleNode($"/Settings/{nodeName}");
            string text = node.InnerText;

            return text;
        }
    }
}
