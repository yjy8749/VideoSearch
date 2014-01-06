using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace VideoSearch
{
    class ServerListTools
    {
        public static void createServerList()
        {
            string file = @"C:\Users\leo\Desktop\serverlist.txt";
            StreamReader fs = new StreamReader(file);
            XmlDocument xmldoc = new XmlDocument();
            XmlElement root = xmldoc.CreateElement("root");
            xmldoc.AppendChild(root);
            string text = "";
            string name ="";
            string ip = "";
            XmlElement server;
            XmlElement nam;
            XmlElement url;
            while (!fs.EndOfStream)
            {
                text=fs.ReadLine();
                char [] chs=text.ToCharArray();
                for (int i = 0; i < chs.Length; i++)
                {
                    if (chs[i] <= '9' && chs[i] >= '0')
                    {
                        server = xmldoc.CreateElement("server");
                        nam = xmldoc.CreateElement("name");
                        url = xmldoc.CreateElement("url");
                        nam.InnerText = text.Substring(0, i);
                        url.InnerText = "http://" + text.Substring(i) + "/";
                        server.AppendChild(nam);
                        server.AppendChild(url);
                        root.AppendChild(server);
                        break;
                    }
                }
            }
            xmldoc.Save(Constant.SERVET_LIST_FILE_PATH);
        }
    }
}
