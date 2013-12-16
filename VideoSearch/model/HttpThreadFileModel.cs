using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace VideoSearch
{
    class HttpThreadFileModel
    {
        private HttpThreadFile httpThreadFile;
        private int threadNum;//线程代号　
        private string tempName;//文件名　
        private string url;//接收文件的URL　
        private FileStream fs;
        private HttpWebRequest request;
        private System.IO.Stream ns;
        private byte[] nbytes;//接收缓冲区　
        private int nreadsize;//接收字节数
        private short decryptModel;
        public HttpThreadFileModel(HttpThreadFile htf, int threadNum,short decryptModel)//构造方法
        {
            this.httpThreadFile = htf;
            this.threadNum = threadNum;
            this.decryptModel = decryptModel;
        }
        public void receive()//接收线程
        {
            this.tempName = this.httpThreadFile.tempFileName[this.threadNum];
            this.url = this.httpThreadFile.url;
            ns = null;
            nbytes = new byte[512];
            nreadsize = 0;
            Thread.Sleep(100);
            while (true)
            {
                try
                {
                    if (File.Exists(this.tempName))
                    {
                        fs = new FileStream(this.tempName, System.IO.FileMode.Open);
                    }
                    else
                    {
                        fs = new FileStream(this.tempName, System.IO.FileMode.Create);
                    }
                    break;
                }
                catch
                {
                    continue;
                }
            }
            try
            {
                request = (HttpWebRequest)HttpWebRequest.Create(this.url);
                request.UserAgent = Constant.DOWNLOAD_USER_AGENT;
                //接收的起始位置及接收的长度
                request.AddRange(this.httpThreadFile.threadFileStartIndex[this.threadNum], this.httpThreadFile.threadFileStartIndex[this.threadNum] + this.httpThreadFile.threadFileSize[this.threadNum]);
                ns = request.GetResponse().GetResponseStream();//获得接收流
                nreadsize = ns.Read(nbytes, 0, 512);
                if (this.threadNum == 0)
                {
                    if (this.decryptModel == Constant.FORCIBLY_DECRYPT_MODEL)
                    {
                        for (int i = 0; i < 160; i++)
                        {
                            nbytes[i] = (byte)~nbytes[i];
                        }
                    }
                    if (this.decryptModel == Constant.AUTO_DECRYPT_MODEL)
                    {
                        if (!FileCheck.checkFileType(nbytes))
                        {
                            for (int i = 0; i < 160; i++)
                            {
                                nbytes[i] = (byte)~nbytes[i];
                            }
                        }
                    }
                }
                while (nreadsize > 0)
                {
                    this.httpThreadFile.addDownloadSize(nreadsize);
                    fs.Write(nbytes, 0, nreadsize);
                    nreadsize = ns.Read(nbytes, 0, 512);
                    //formm.setPBValue(nreadsize);//接收字节数
                    fs.Flush();
                }
                fs.Close();
                ns.Close();
            }
            catch (Exception er)
            {
                fs.Close();
            }
            this.httpThreadFile.threadFlag[this.threadNum] = true;
        }
    }
}
