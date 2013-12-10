using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace VideoSearch
{
    class DownloadService
    {
        public static bool downLoadFileAndSave(string url,string path)
        {
            long SPosition = 0;
            FileStream FStream;
            if (File.Exists(path))
            {
                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("文件已存在是否覆盖？", "覆盖文件", messButton);
                if (dr == DialogResult.Cancel)
                {
                    return false;
                }
            }
            //文件不保存创建一个文件
            FStream = new FileStream(path, FileMode.Create);
            SPosition = 0;
            try
            {
                //打开网络连接
                HttpWebRequest myRequest = (HttpWebRequest)HttpWebRequest.Create(url);
                if (SPosition > 0)
                    myRequest.AddRange((int)SPosition);             //设置Range值
                //向服务器请求,获得服务器的回应数据流
                Stream myStream = myRequest.GetResponse().GetResponseStream();
                //定义一个字节数据
                byte[] btContent = new byte[512];
                int intSize = 0;
                intSize = myStream.Read(btContent, 0, 512);
                while (intSize > 0)
                {
                    FStream.Write(btContent, 0, intSize);
                    intSize = myStream.Read(btContent, 0, 512);
                }
                //关闭流
                FStream.Close();
                myStream.Close();
                return true;//下载成功
            }
            catch (Exception)
            {
                FStream.Close();
                return false;//下载失败
            }
        }
    }
}
