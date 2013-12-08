using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace VideoSearch
{
    class HttpFileModel
    {
        private string url = null;
        private string filePath = null;
        public string content = null;
        public HttpFileModel(string url)
        {
            this.url = url;
        }
        public HttpFileModel(string url,string filePath)
        {
            this.url = url;
            this.filePath = filePath;
        }
        public void load()
        {
            this.content = HttpFileModel.load(this.url).content;
        }
        public static HttpFileModel load(string url, string encode = "GB2312")
        {
            HttpFileModel httpFile = new HttpFileModel(url);

            HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(url);
            StreamReader sr = null;
            HttpWebResponse httpResponse = null;
            httpReq.UserAgent = Constant.USER_AGENT;
            try
            {
                httpResponse = (HttpWebResponse)httpReq.GetResponse();
                sr = new StreamReader(httpResponse.GetResponseStream(), Encoding.GetEncoding(encode));
                httpFile.content = sr.ReadToEnd();
            }
            catch
            {
                httpFile.content = null;
            }
            return httpFile;
        }
        public void save()
        {
            this.save(this.filePath);
        }
        public void save(string path)
        {
            StreamWriter sw = File.CreateText(path);
            sw.Write(this.content);
            sw.Close();
        }
    }
}
