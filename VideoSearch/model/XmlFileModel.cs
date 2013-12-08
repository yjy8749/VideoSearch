using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace VideoSearch
{
    class XmlFileModel
    {
        private string xmlpath = null;
        public XmlDocument xmldoc = null;
        private XmlNode root = null;
        public XmlFileModel(string path)
        {
            xmlpath = path;
            this.load();
            root = xmldoc.FirstChild;
        }
        public XmlFileModel(string path, XmlDocument xdoc)
        {
            xmlpath = path;
            xmldoc = xdoc;
            root = xmldoc.FirstChild;
        }
        private void load()
        {
            if (xmldoc == null)
            {
                xmldoc = new XmlDocument();
                if (File.Exists(xmlpath))
                {
                    try
                    {
                        xmldoc.Load(xmlpath);
                        return;
                    }
                    catch(Exception e)
                    {                        
                        xmldoc = new XmlDocument();
                        File.Delete(xmlpath);
                    }
                }
                XmlElement root = xmldoc.CreateElement("root");
                xmldoc.AppendChild(root);
            }
        }
        public void save()
        {
            xmldoc.Save(xmlpath);
        }
        public void addNode(string node, string value)
        {
            XmlElement element = xmldoc.CreateElement(node);
            element.InnerText = value;
            root.AppendChild(element);
        }
        public XmlNodeList getNodes(string key)
        {
            return root.SelectNodes(key);
        }
        public XmlNode getNode(string node)
        {
            return root.SelectSingleNode(node);
        }
        
    }
}
