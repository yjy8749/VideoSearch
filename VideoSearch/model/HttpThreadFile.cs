using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace VideoSearch
{
    class HttpThreadFile
    {
        public bool[] threadw; //每个线程结束标志
        public string[] filenamew;//每个线程接收文件的文件名
        public int[] filestartw;//每个线程接收文件的起始位置
        public int[] filesizew;//每个线程接收文件的大小
        public string strurl;//接受文件的URL
        public bool hb;//文件合并标志
        public int thread;//进程数
        long filesize = 0;
        public double downsize = 0;
        public string moviepath;
        public string temppath = "";
        DateTime dt;
        public short decryptModel = Constant.AUTO_DECRYPT_MODEL;
        private static object locker = new object();
        public HttpThreadFile(string path, string url, short decryptModel)
        {
            moviepath = path;
            strurl = url;
            this.decryptModel = decryptModel;
            HttpWebRequest request;
            request = (HttpWebRequest)HttpWebRequest.Create(strurl);
            request.UserAgent = Constant.DOWNLOAD_USER_AGENT;
            filesize = request.GetResponse().ContentLength;//目标文件长度
            downsize = 0;
            request.Abort();
        }

        public string getSchedule()
        {
            return (downsize / filesize).ToString() + "";
        }
        public string getSpeed()
        {
            DateTime now = new DateTime();
            double m = DateTime.Now.Subtract(dt).TotalSeconds;
            return ((downsize / m) / 1000000).ToString("0.00");
        }
        public void addDownloadSize(long read)
        {
            lock (locker)
            {
                this.downsize += read;
            }
        }
        public Message startDownload(string path)
        {
            Message msg = new Message();
            ServicePointManager.DefaultConnectionLimit = 512;
            dt = DateTime.Now;//开始接收时间
            try
            {
                thread = 8;//根据线程数初始化数组
                threadw = new bool[thread];
                filenamew = new string[thread];
                filestartw = new int[thread];
                filesizew = new int[thread];　//计算每个线程应该接收文件的大小
                int filethread = (int)filesize / thread;//平均分配
                int filethreade = filethread + (int)filesize % thread;//剩余部分由最后一个线程完成　
                //为数组赋值
                for (int i = 0; i < thread; i++)
                {
                    threadw[i] = false;//每个线程状态的初始值为假
                    filenamew[i] = temppath + i.ToString() + ".ahnu";//每个线程接收文件的临时文件名
                    if (i < thread - 1)
                    {
                        filestartw[i] = filethread * i;//每个线程接收文件的起始点
                        filesizew[i] = filethread - 1;//每个线程接收文件的长度
                    }
                    else
                    {
                        filestartw[i] = filethread * i;
                        filesizew[i] = filethreade - 1;
                    }
                }
                //定义线程数组，启动接收线程
                Thread[] threadk = new Thread[thread];
                HttpThreadFileModel[] httpfile = new HttpThreadFileModel[thread];
                for (int j = 0; j < thread; j++)
                {
                    httpfile[j] = new HttpThreadFileModel(this, j,this.decryptModel);
                    //ThreadPool.QueueUserWorkItem(new WaitCallback(httpfile[j].receive),1);
                    threadk[j] = new Thread(new ThreadStart(httpfile[j].receive));
                    threadk[j].Start();
                }
                //启动合并各线程接收的文件的线程
                this.mergeFile(path);
                msg.isSucceed = true;
                msg.msg = "视频下载完成";
                return msg;
            }
            catch (Exception er)
            {
            }
            msg.isSucceed = true;
            msg.msg = "视频下载失败";
            return msg;
        }

        public void mergeFile(string path)
        {
            while (true)//等待
            {
                hb = true;
                for (int i = 0; i < thread; i++)
                {
                    if (threadw[i] == false)
                    {
                        hb = false;
                        Thread.Sleep(100);
                        break;
                    }
                }
                if (hb == true)//所有线程已结束，停止等待
                {
                    break;
                }
            }
            FileStream fs;//开始合并
            FileStream fstemp;
            int readfile;
            byte[] bytes = new byte[512];
            fs = new FileStream(path, System.IO.FileMode.Create);
            for (int k = 0; k < thread; k++)
            {
                fstemp = new FileStream(filenamew[k], System.IO.FileMode.Open);
                while (true)
                {
                    readfile = fstemp.Read(bytes, 0, 512);
                    if (readfile > 0)
                    {
                        fs.Write(bytes, 0, readfile);
                    }
                    else
                    {
                        break;
                    }
                }
                fstemp.Close();
                File.Delete(filenamew[k]);
            }
            fs.Close();
        }
    }
}
