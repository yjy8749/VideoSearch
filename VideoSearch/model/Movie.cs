using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;

namespace VideoSearch
{
    public class Movie
    {
        private static object locker = new object();
        private static int allCount = 0 ;
        private string code;
        public string name;
        public string url;
        public string type;
        private HttpThreadFile httpThreadFile = null;
        public short decryptModel;
        public string path=Constant.DEFAULT_DOWNLOAD_DIR;
        public bool cancle = false;
        public Movie(string name,string code)
        {
            this.name = name;
            this.code = code;
            lock (locker)
            {
                Movie.allCount++;
            }
            ThreadPool.QueueUserWorkItem(new WaitCallback(analyze));
            this.url = "正在解析地址";
        }
        private void analyze(object o)
        {
            this.analyze();
            lock (locker)
            {
                Movie.allCount--;
            }
        }
        private void analyze()
        {
            string content = HttpFileModel.load(Constant.SERVICE_ADDRESS + "return.asp?info=" + code).content;
            if (content != null)
            {
                XmlDocument xmlDoc = new XmlDocument();
                try
                {
                    xmlDoc.LoadXml(content);
                    this.url = Regex.Replace(xmlDoc.SelectSingleNode("root/url").InnerText, Constant.IS_USE_HTTPS ? "https://(.*?)/" : "http://(.*?)/", Constant.SERVICE_ADDRESS);
                    if (this.url.Equals(""))
                    {
                        this.url = MsgString.THIS_MOVIE_NOT_EXIST;
                    }
                    else
                    {
                        string[] strs = this.url.Split('.');
                        this.type = "." + strs[strs.Length - 1];
                    }
                }
                catch (Exception e)
                {
                    this.url = MsgString.ANALYZE_XML_FILE_FAILED;
                }
            }
            else
            {
                this.url = MsgString.ANALYZE_URL_FILE_FAILED;
            }
        }
        public Message download()
        {
            httpThreadFile = new HttpThreadFile(name + this.type, this.url, decryptModel);
            if(!this.path.EndsWith("\\")) this.path =this.path + Path.DirectorySeparatorChar;
            return httpThreadFile.startDownload(this.path + this.name + this.type);
        }
        public Message cancleDownload()
        {
            return this.httpThreadFile.stopDownload();
        }
        public string getShcedule()
        {
            if(this.httpThreadFile==null) return "0.00";
            return this.httpThreadFile.getSchedule();
        }
        public string getSpeed()
        {
            if (this.httpThreadFile == null) return "0.00";
            return this.httpThreadFile.getSpeed();
        }
        public static bool isAllComplete()
        {
            if (Movie.allCount == 0) return true;
            return false;
        }
        public string reallyPath()
        {
            return this.path + this.name + this.type;
        }

    }
}
