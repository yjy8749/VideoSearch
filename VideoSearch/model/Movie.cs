using System;
using System.Collections.Generic;
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
        private string type;
        private HttpThreadFile httpThreadFile = null;
        public short decryptModel;
        public Movie(string name,string code)
        {
            this.name = name;
            this.code = code;
            lock (locker)
            {
                Movie.allCount++;
            }
            ThreadPool.QueueUserWorkItem(new WaitCallback(analyze));           
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
            string content = HttpFileModel.load(Constant.SERVICE_ADDRESS + "/return.asp?info=" + code).content;
            if (content != null)
            {
                XmlDocument xmlDoc = new XmlDocument();
                try
                {
                    xmlDoc.LoadXml(content);
                    this.url = Regex.Replace(xmlDoc.SelectSingleNode("root/url").InnerText, Constant.IS_USE_HTTPS ? "https://(.*?)/" : "http://(.*?)/", Constant.SERVICE_ADDRESS+"/");
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
            return httpThreadFile.startDownload();
        }
        public void cancleDownload()
        {
            if (httpThreadFile != null)
            {
                httpThreadFile.stopDownload();
            }

        }
        public string getShcedule()
        {
            if(this.httpThreadFile==null) return "0";
            return this.httpThreadFile.getSchedule();
        }
        public string getSpeed()
        {
            if (this.httpThreadFile == null) return "0";
            return this.httpThreadFile.getSpeed();
        }
        public static bool isAllComplete()
        {
            if (Movie.allCount == 0) return true;
            return false;
        }
    }
}
