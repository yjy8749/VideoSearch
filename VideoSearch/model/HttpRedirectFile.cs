using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace VideoSearch
{
    class HttpRedirectFile
    {
        private Socket acceptSocket;
        private string url;
        private short model;
        private HttpWebRequest request;
        private System.IO.Stream ns;
        private byte[] buff = new byte[1024];
        private bool isDecrypted = false;
        public HttpRedirectFile(string url, short model, ref Socket acceptSocket)//构造方法
        {
            this.url = url;
            this.model = model;
            this.acceptSocket = acceptSocket;
        }
        public long getLength()
        {
            request = (HttpWebRequest)HttpWebRequest.Create(this.url);
            request.UserAgent = Constant.DOWNLOAD_USER_AGENT;
            long length = request.GetResponse().ContentLength;
            request.Abort();
            return length;
        }
        public void startRedirect()//开始转发
        {

            request = (HttpWebRequest)HttpWebRequest.Create(this.url);
            request.UserAgent = Constant.DOWNLOAD_USER_AGENT;
            ns = request.GetResponse().GetResponseStream();//获得接收流
            int read = ns.Read(buff, 0, buff.Length);
            if (!isDecrypted)
            {
                if (this.model == Constant.FORCIBLY_DECRYPT_MODEL)
                {
                    for (int i = 0; i < 160; i++)
                    {
                        buff[i] = (byte)~buff[i];
                    }
                }
                if (this.model == Constant.AUTO_DECRYPT_MODEL)
                {
                    if (!FileCheck.checkFileType(buff))
                    {
                        for (int i = 0; i < 160; i++)
                        {
                            buff[i] = (byte)~buff[i];
                        }
                    }
                }
                isDecrypted = true;
            }
            while (read > 0)
            {
                try
                {
                    acceptSocket.Send(buff, buff.Length, 0);
                    read = ns.Read(buff, 0, buff.Length);
                }
                catch
                {
                }               
            }
            ns.Close();

        }
    }
}
