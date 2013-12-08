using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace VideoSearch
{
    class XMLService
    {

        public static XmlFileModel getTotalInfo()
        {
            string filePath = Constant.TODAY_DATE+Constant.TOTAL_FILE_PATH;
            if (File.Exists(filePath))
            {
                if (Constant.totalInfo == null)
                {
                    Constant.totalInfo = new XmlFileModel(filePath);
                }
            }
            else
            {
                HttpFileModel file=HttpFileModel.load(Constant.SERVICE_ADDRESS + Constant.SERVICE_TOTAL_XML_URL);
                try
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(file.content);
                    xmlDoc.Save(filePath);
                    File.Delete(Constant.YESTERDAY_DATE + Constant.TOTAL_FILE_PATH);
                    if (Constant.totalInfo == null)
                    {
                        Constant.totalInfo = new XmlFileModel(filePath,xmlDoc);
                    }
                }catch (Exception e)
                {
                    if (Constant.totalInfo == null)
                    {
                        Constant.totalInfo = new XmlFileModel(Constant.YESTERDAY_DATE + Constant.TOTAL_FILE_PATH);
                        File.Delete(Constant.YESTERDAY_DATE + Constant.TOTAL_FILE_PATH);
                    }
                }
            }
            return Constant.totalInfo;
        }

        public static bool saveConfigXMl(Hashtable table)
        {
            File.Delete(Constant.CONFIG_FILE_PATH);
            XmlFileModel configXml = new XmlFileModel(Constant.CONFIG_FILE_PATH);
            foreach (string key in table.Keys)
            {
                configXml.addNode(key, (string)table[key]);
            }
            configXml.save();
            return true;
        }
        public static Hashtable updateServerList()
        {
            File.Delete(Constant.SERVET_LIST_FILE_PATH);
            return XMLService.initServerList();
        }
        public static Hashtable initServerList()
        {
            if (!File.Exists(Constant.SERVET_LIST_FILE_PATH))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(HttpFileModel.load(Constant.SERVER_LIST_FILE_URL,"UTF-8").content);
                xmlDoc.Save(Constant.SERVET_LIST_FILE_PATH);
            }
            XmlNodeList serverList = new XmlFileModel(Constant.SERVET_LIST_FILE_PATH).getNodes("server");
            Hashtable table = new Hashtable();
            foreach (XmlNode server in serverList)
            {
                table.Add(server.SelectSingleNode("name").InnerText, server.SelectSingleNode("url").InnerText);
            }
            return table;
        }
    }
}
