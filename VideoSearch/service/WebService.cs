using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading ;

namespace VideoSearch
{
    class WebService
    {

        private TcpListener webListener= null;
        private Thread listenThread = null;
        private int port = 9999;
        public Message start()
        {
            Message msg = new Message();
            try
            {
                webListener = new TcpListener(IPAddress.Any, port);
                webListener.Start();
                isListen = true;
                listenThread = new Thread(new ThreadStart(startListen));
                listenThread.Start();
                msg.isSucceed = true;
                msg.msg ="启动成功，开始监听9999端口";
            }
            catch (Exception e)
            {
                msg.isSucceed = false;
                msg.msg ="启动失败，请确认本机9999端口未被占用";
            }
            return msg;
        }

        public string stop()
        {
            Message msg = new Message();
            webListener.Stop();
            isListen = false;
            listenThread.Abort();
            return "共享服务已停止";
        }
        private bool isListen = true;
        public void startListen()
        {

            int iStartPos = 0;
            String sRequest;
            while (isListen)
            {
                Socket acceptSocket = webListener.AcceptSocket();
                if (acceptSocket.Connected)
                {
                    Byte[] bReceive = new Byte[1024];
                    int i = acceptSocket.Receive(bReceive, bReceive.Length, 0);
                    string requestInfo = Encoding.ASCII.GetString(bReceive);
                    if (requestInfo.Substring(0, 3) !="GET")
                    {
                        //非GET请求
                        acceptSocket.Close();
                        continue;
                    }
                    iStartPos = requestInfo.IndexOf("HTTP", 1);
                    string sHttpVersion = requestInfo.Substring(iStartPos, 8);
                    sRequest = requestInfo.Substring(0, iStartPos - 1);
                    sRequest.Replace("\\", "/");

                    //如果结尾不是文件名也不是以"/"结尾则加"/"
                    
                    doRequest(sRequest,sHttpVersion, ref acceptSocket);
                    acceptSocket.Close();
                }
            }
        }
        private static String sMyWebServerRoot = "WEB"; //设置虚拟目录  
        public void doRequest(string sRequest, string sHttpVersion,ref Socket acceptSocket)
        {
            String sDirName;
            String sRequestedFile;
            String sLocalDir;      
            String sPhysicalFilePath = "";
            String sResponse = "";
            string[] request = sRequest.Split('/');
            sRequestedFile = request[request.Length-1];
            //得到请求文件目录
            sDirName = request[1];
            //获取虚拟目录物理路径
            sLocalDir = sMyWebServerRoot;
            if (sLocalDir.Length == 0)
            {
                //404
                StreamReader fileStream = new StreamReader(sMyWebServerRoot + Path.DirectorySeparatorChar + WebConstant.WEB_404_FILE_PATH);
                string content = fileStream.ReadToEnd();
                SendHeader(sHttpVersion, "", content.Length, " 404 Not Found", ref acceptSocket);
                SendToBrowser(content, ref acceptSocket);
                acceptSocket.Close();
            }

            if (sRequestedFile.Length == 0)
            {
                sRequestedFile = Path.DirectorySeparatorChar + WebConstant.WEB_INDEX_FILE_PATH;
            }

            String sMimeType = "text/html";
            sPhysicalFilePath = sLocalDir + Path.DirectorySeparatorChar + sRequestedFile;
            if (File.Exists(sPhysicalFilePath) == false)
            {
                //404
                StreamReader fileStream = new StreamReader(sMyWebServerRoot + Path.DirectorySeparatorChar + WebConstant.WEB_404_FILE_PATH);
                string content = fileStream.ReadToEnd();
                SendHeader(sHttpVersion, "", content.Length, " 404 Not Found", ref acceptSocket);
                SendToBrowser(content, ref acceptSocket);
                acceptSocket.Close();
            }
            else
            {
                int iTotBytes = 0;
                sResponse = "";
                FileStream fs = new FileStream(sPhysicalFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                BinaryReader reader = new BinaryReader(fs);
                byte[] bytes = new byte[fs.Length];
                int read;
                while ((read = reader.Read(bytes, 0, bytes.Length)) != 0)
                {
                    sResponse = sResponse + Encoding.ASCII.GetString(bytes, 0, read);

                    iTotBytes = iTotBytes + read;

                }
                reader.Close();
                fs.Close();

                SendHeader(sHttpVersion, sMimeType, iTotBytes, " 200 OK", ref acceptSocket);
                SendToBrowser(bytes, ref acceptSocket);
                //mySocket.Send(bytes, bytes.Length,0);
            }
        }
        public void SendHeader(string sHttpVersion, string sMIMEHeader, int iTotBytes, string sStatusCode, ref Socket mySocket)
        {

            String sBuffer ="";

            if (sMIMEHeader.Length == 0)
            {
                sMIMEHeader ="text/html"; // 默认 text/html
            }

            sBuffer = sBuffer + sHttpVersion + sStatusCode + "\r\n";
            sBuffer = sBuffer + "Server: cx1193719-b\r\n";
            sBuffer = sBuffer + "Content-Type: " + sMIMEHeader + "\r\n";
            sBuffer = sBuffer + "Accept-Ranges: bytes\r\n";
            sBuffer = sBuffer + "Content-Length: " + iTotBytes + "\r\n\r\n";

            Byte[] bSendData = Encoding.ASCII.GetBytes(sBuffer);

            SendToBrowser(bSendData, ref mySocket);

            Console.WriteLine("Total Bytes : " + iTotBytes.ToString());

        }

        public void SendToBrowser(String sData, ref Socket mySocket)
        {
            SendToBrowser(Encoding.ASCII.GetBytes(sData), ref mySocket);
        }

        public void SendToBrowser(Byte[] bSendData, ref Socket mySocket)
        {
            int numBytes = 0;

            try
            {
                if (mySocket.Connected)
                {
                    if ((numBytes = mySocket.Send(bSendData, bSendData.Length, 0)) == -1)
                        Console.WriteLine("Socket Error cannot Send Packet");
                    else
                    {
                        Console.WriteLine("No. of bytes send {0}", numBytes);
                    }
                }
                else
                    Console.WriteLine("连接失败....");
            }
            catch (Exception e)
            {
                Console.WriteLine("发生错误 : {0} ", e);

            }
        }
        
    }
}
