using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
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
        public static void initConfig()
        {
            if (File.Exists(Constant.CONFIG_FILE_PATH))
            {
                XmlFileModel config = new XmlFileModel(Constant.CONFIG_FILE_PATH);
                Constant.SERVICE_ADDRESS = config.getNode("serviceIp").InnerText;
                Constant.DEFAULT_DOWNLOAD_DIR = config.getNode("defaultDir").InnerText;
            }
        }
        public static void checkVersion()
        {
            Thread.Sleep(1000);
            HttpFileModel versionFile = HttpFileModel.load(Constant.VERSION_FILE_URL);
            if (versionFile.content == null || versionFile.content.Equals(""))
            {
                return;
            }
            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(versionFile.content);
            if (Constant.VERSION.CompareTo(xdoc.SelectSingleNode("root/version").InnerText)<0)
            {
                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("检查到有最新版本可供下载，是否下载？\r\n" + xdoc.SelectSingleNode("root/msg").InnerText, "有新版本", messButton);
                if (dr == DialogResult.OK)
                {
                    MainForm.getInterface().setRunState("正在下载新版本…………");
                    string path = MainForm.getInterface().selectPath();
                    DownloadService.downLoadFileAndSave(Constant.UPDATE_FILE_URL,path+"/ahnu_download.zip");
                    MainForm.getInterface().setRunState("下载完成，请运行新版本");
                    Process.Start(path + "/ahnu_download.zip");
                }
            }
        }
    }
}
