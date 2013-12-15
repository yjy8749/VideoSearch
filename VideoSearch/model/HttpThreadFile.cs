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
        public string url;
        public string filePath;
        public bool[] threadFlag; //每个线程结束标志
        public string[] tempFileName;//每个线程接收文件的文件名
        public int[] threadFileStartIndex;//每个线程接收文件的起始位置
        public int[] threadFileSize;//每个线程接收文件的大小        
        public bool isStartMerge = false;//文件合并标志
        public int threadCount;//进程数
        public long fileSize = 0;
        public long haveDownSize = 0;        
        private DateTime startTime;
        private short decryptModel;
        private Thread[] threadList;
        private bool cancle = false;
        private static object locker = new object();
        public HttpThreadFile(string path, string url, short decryptModel)
        {
            this.filePath = path;
            this.decryptModel = decryptModel;
            this.url = url;
            HttpWebRequest request;
            request = (HttpWebRequest)HttpWebRequest.Create(this.url);
            request.UserAgent = Constant.DOWNLOAD_USER_AGENT;
            this.fileSize = request.GetResponse().ContentLength;//目标文件长度
            request.Abort();
            this.haveDownSize = 0;
        }

        public string getSchedule()
        {
            return (this.haveDownSize * 100 / this.fileSize).ToString();
        }
        public string getSpeed()
        {
            double m = DateTime.Now.Subtract(startTime).TotalSeconds;
            return ((this.haveDownSize / m) / 1000000).ToString("0.00")+"M/s";
        }
        public void addDownloadSize(long read)
        {
            lock (locker)
            {
                this.haveDownSize += read;
            }
        }
        public void stopDownload()
        {
            for (int i = 0; i < this.threadList.Length; i++)
            {
                if (this.threadList[i].ThreadState != ThreadState.Stopped)
                {
                    this.threadList[i].Abort();
                }
            }
            this.cancle = true;
        }

        public Message startDownload(string path)
        {
            Message msg = new Message();
            ServicePointManager.DefaultConnectionLimit = 512;
            this.startTime = DateTime.Now;//开始接收时间
            try
            {          
                this.threadCount = Constant.DOWNLOAD_THREAD_COUNT;//根据线程数初始化数组
                this.threadFlag = new bool[this.threadCount];
                this.tempFileName = new string[this.threadCount];
                this.threadFileStartIndex = new int[this.threadCount];
                this.threadFileSize = new int[this.threadCount];　//计算每个线程应该接收文件的大小
                int filethread = (int)this.fileSize / this.threadCount;//平均分配
                int filethreade = filethread + (int)this.fileSize % this.threadCount;//剩余部分由最后一个线程完成　
                //为数组赋值
                for (int i = 0; i < this.threadCount; i++)
                {
                    this.threadFlag[i] = false;//每个线程状态的初始值为假
                    this.tempFileName[i] = i.ToString() + Constant.FILE_TYPE;//每个线程接收文件的临时文件名
                    if (i < this.threadCount - 1)
                    {
                        this.threadFileStartIndex[i] = filethread * i;//每个线程接收文件的起始点
                        this.threadFileSize[i] = filethread - 1;//每个线程接收文件的长度
                    }
                    else
                    {
                        this.threadFileStartIndex[i] = filethread * i;
                        this.threadFileSize[i] = filethreade - 1;
                    }
                }
                //定义线程数组，启动接收线程
                threadList = new Thread[this.threadCount];
                HttpThreadFileModel[] httpThreadFile = new HttpThreadFileModel[this.threadCount];
                for (int j = 0; j < this.threadCount; j++)
                {
                    httpThreadFile[j] = new HttpThreadFileModel(this, j,this.decryptModel);
                    //ThreadPool.QueueUserWorkItem(new WaitCallback(httpfile[j].receive),1);
                    threadList[j] = new Thread(new ThreadStart(httpThreadFile[j].receive));
                    threadList[j].Start();
                }
                //启动合并各线程接收的文件的线程
                this.mergeFile(path);
                msg.isSucceed = true;
                msg.msg = MsgString.DOWNLOAD_MOVIE_SUCCESS;
                return msg;
            }
            catch (Exception er)
            {
                msg.isSucceed = false;
                msg.msg = er.Message;
                return msg;
            }
        }

        public void mergeFile(string path)
        {
            bool flag = true;
            while (true)//等待
            {
                flag = true;
                for (int i = 0; i < threadCount; i++)
                {
                    if (threadFlag[i] == false)
                    {
                        flag = false;
                        Thread.Sleep(1000);
                        break;
                    }
                }
                if (flag == true)//所有线程已结束，停止等待
                {
                    break;
                }
            }
            FileStream fs;//开始合并
            FileStream fstemp;
            int readfile;
            byte[] bytes = new byte[512];
            fs = new FileStream(path, System.IO.FileMode.Create);
            for (int k = 0; k < this.threadCount; k++)
            {
                if (!this.threadFlag[k])
                {
                    k--;
                    Thread.Sleep(100);
                    continue;
                }
                fstemp = new FileStream(this.tempFileName[k], System.IO.FileMode.Open);
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
                    fs.Flush();
                }
                fstemp.Close();
                File.Delete(this.tempFileName[k]);
            }
            fs.Close();
        }
    }
}
