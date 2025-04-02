using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace ISBNCaller_GUI
{
    internal class XmlWriter
    {
        private XmlDocument mXmlDocument { get; set; }
        private string mXmlName { get; set; }
        public XmlWriter(string filePath)
        {
            mXmlName = filePath;
            mXmlDocument = new XmlDocument();
            if (File.Exists(filePath))
            {
                mXmlDocument.Load(filePath);
            }
        }

        /// <summary>
        /// For writing direct inner text of nodes
        /// </summary>
        /// <param name="nodeName">The direct name of the node to change</param>
        /// <param name="text">The new text</param>
        /// <returns>True if ok</returns>
        public bool Write(string nodeName, string text)
        {
            bool ok = false;
            try
            {
                XmlNode node = mXmlDocument.SelectSingleNode($"/Settings/{nodeName}");
                node.InnerText = text;
                mXmlDocument.Save(mXmlName);
                ok = true;
            }
            catch(Exception ex)
            {

            }

            return ok;
        }

        public bool AddNode(string nodeName, string text)
        {
            bool ok = false;
            try
            {
                XElement xml = XElement.Load(mXmlName);
                var childrens = xml.DescendantsAndSelf().ToArray();
                childrens[childrens.Length-1].AddAfterSelf(new XElement($"{nodeName}", $"{text}"));

                xml.Save(mXmlName);
                //File.AppendAllText(mXmlName, $@"<{nodeName}>{text}</{nodeName}>\r\n</Settings>");
                ok = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show($@"An Error occured:{ex.ToString()}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return ok;
        }

        public bool CreateSettingsXML(string filePath, string text)
        {
            bool ok = false;
            try
            {
                File.WriteAllText(filePath, text);
                ok = true;
            } // try
            catch(Exception ex)
            {
                MessageBox.Show($@"An Error occured:{ex.ToString()}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return ok;
        }
    }
}
