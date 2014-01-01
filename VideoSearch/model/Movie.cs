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
        public bool isAbort = false;
        private int num = -1;
        public Movie(string name,string code,int num)
        {
            this.name = name;
            this.code = code;
            this.num = num;
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
            string content = HttpFileModel.load(Constant.SERVICE_ADDRESS + "xy_path.asp?a="+num+"&b=" + code).content;
            if (content != null)
            {
                try
                {
                    string[] strArr = content.Split('|');
                    foreach (string str in strArr)
                    {
                        if (str.StartsWith("http"))
                        {
                            this.url = str;
                            break;
                        }
                    }
                    if (this.url.Equals("")||this.url.Equals("正在解析地址"))
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
            this.isAbort = false;
            httpThreadFile = new HttpThreadFile(name + this.type, this.url, decryptModel);
            if(!this.path.EndsWith("\\")) this.path =this.path + Path.DirectorySeparatorChar;
            return httpThreadFile.startDownload(this.path + this.name + this.type);
        }
        public Message cancleDownload()
        {
            this.isAbort = true;
            Message msg;
            if (this.httpThreadFile != null)
            {
                msg = this.httpThreadFile.stopDownload();
                this.httpThreadFile = null;
            }
            else
            {
                msg = new Message();
                msg.isSucceed = true;
                msg.msg = "取消下载";
            }
            return msg;
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
