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
        public HttpThreadFile formm;
        public int threadh;//线程代号　
        public string filename;//文件名　
        public string strUrl;//接收文件的URL　
        public FileStream fs;
        public HttpWebRequest request;
        public System.IO.Stream ns;
        public byte[] nbytes;//接收缓冲区　
        public int nreadsize;//接收字节数
        private short decryptModel;
        public HttpThreadFileModel(HttpThreadFile htf, int thread, short decryptModel)//构造方法
        {
            formm = htf;
            threadh = thread;
            this.decryptModel = decryptModel;
        }
        public void receive()//接收线程
        {
            filename = formm.filenamew[threadh];
            strUrl = formm.strurl;
            ns = null;
            nbytes = new byte[512];
            nreadsize = 0;
            Thread.Sleep(100);
            while (true)
            {
                try
                {
                    fs = new FileStream(filename, System.IO.FileMode.Create);
                    break;
                }
                catch
                {
                    continue;
                }
            }
            try
            {
                request = (HttpWebRequest)HttpWebRequest.Create(strUrl);
                request.UserAgent = Constant.DOWNLOAD_USER_AGENT;
                request.Timeout = 1000000;
                //接收的起始位置及接收的长度
                request.AddRange(formm.filestartw[threadh], formm.filestartw[threadh] + formm.filesizew[threadh]);
                ns = request.GetResponse().GetResponseStream();//获得接收流
                nreadsize = ns.Read(nbytes, 0, 512);
                formm.addDownloadSize(nreadsize);
                if (this.threadh == 0)
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
                    fs.Write(nbytes, 0, nreadsize);
                    nreadsize = ns.Read(nbytes, 0, 512);
                    //formm.setPBValue(nreadsize);//接收字节数
                    formm.addDownloadSize(nreadsize);
                }
                fs.Close();
                ns.Close();
            }
            catch (Exception er)
            {
                fs.Close();
            }
            formm.threadw[threadh] = true;
        }            
    }
}
