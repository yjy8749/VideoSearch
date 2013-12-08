using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace VideoSearch
{
    public partial class DownLoadForm : Form
    {
        private DownLoadForm()
        {
            InitializeComponent();
        }
        private static readonly DownLoadForm INTERFACE=new DownLoadForm();
        public static DownLoadForm getInterface()
        {
            return DownLoadForm.INTERFACE;
        }
        public List<Movie> queueList = new List<Movie>();
        public void addQueue(Movie movie)
        {
            this.queueList.Add(movie);
            ListViewItem item = new ListViewItem(movie.name);
            item.SubItems.Add("0.5");
            item.SubItems.Add("0.00 MB/S");
            item.SubItems.Add("正在等待");
            this.scheduleListView.Items.Add(item);
            if (downloadThread == null)
            {
                downloadThread = new Thread(startQueue);
                downloadThread.Start();
            }
        }

        private Thread downloadThread = null;
        private Thread refreshThread = null;
        private bool isDownloading = false;
        private int nowQueueIndex = 0;
        private void startQueue()
        {
            isDownloading = true;
            refreshThread = new Thread(refreshSchedule);
            refreshThread.Start();
            Message msg ;
            for (; nowQueueIndex < this.queueList.Count; nowQueueIndex++)
            {
                this.updateListviewItem(nowQueueIndex, 3, "正在下载");
                msg = this.queueList[nowQueueIndex].download();
                if (msg.isSucceed)
                {
                    this.updateListviewItem(nowQueueIndex, 3, "下载完成");
                }
                else
                {
                    this.updateListviewItem(nowQueueIndex, 3, msg.msg);
                }                
            }
            isDownloading = false;
            this.downloadThread = null;
        }
        private void refreshSchedule()
        {
            string sch;
            while (this.isDownloading)
            {
                sch = this.queueList[nowQueueIndex].getShcedule();
                this.updateListviewItem(nowQueueIndex, 1, sch);
                this.updateListviewItem(nowQueueIndex, 2,this.queueList[nowQueueIndex].getSpeed());
                if (sch.Equals("1"))
                {
                    this.updateListviewItem(nowQueueIndex, 3, "合并缓存");
                }
                Thread.Sleep(100);
            }
        }
        private delegate void UPDATELISTVIEWITEM(int index,int columns,string value);
        private void updateListviewItem(int index,int columns,string value)
        {
            if (this.scheduleListView.InvokeRequired)
            {
                UPDATELISTVIEWITEM set = new UPDATELISTVIEWITEM(updateListviewItem);
                this.Invoke(set, new object[] {index,columns,value});
            }
            else
            {
                this.scheduleListView.Items[index].SubItems[columns].Text = value;
            }
        }
        //private void button1_Click(object sender, EventArgs e)
        //{
        //    Message msg = new HttpThreadFile("shdbz01.mkv", "http://127.0.0.1:8000/生活大爆炸第六季(24集全) 第01集.mkv", Constant.FORCIBLY_DECRYPT_MODEL).startDownload();
        //    MessageBox.Show(msg.msg);
        //}
    }
}
