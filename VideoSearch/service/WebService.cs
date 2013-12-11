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

        private TcpListener myListener;
        private int port = 9999;
        public Message start()
        {
            Message msg = new Message();
            try
            {
                myListener = new TcpListener(port);
                myListener.Start();
                Thread th = new Thread(new ThreadStart(StartListen));
                th.Start();
                msg.isSucceed = true;
                msg.msg ("启动成功，开始监听9999端口");
            }
            catch (Exception e)
            {
                msg.isSucceed = false;
                msg.msg ("启动失败，请确认本机9999端口未被占用");
            }
            return msg;
        }


        public void StartListen()
        {

            int iStartPos = 0;
            String sRequest;
            String sDirName;
            String sRequestedFile;
            String sErrorMessage;
            String sLocalDir;
            String sMyWebServerRoot="WEB"; //设置虚拟目录            
            String sPhysicalFilePath="";
            String sFormattedMessage="";
            String sResponse ="";
            while (true)
            {
                Socket mySocket = myListener.AcceptSocket();
                if (mySocket.Connected)
                {
                    Byte[] bReceive = new Byte[1024];
                    int i = mySocket.Receive(bReceive, bReceive.Length, 0);
                    string sBuffer = Encoding.ASCII.GetString(bReceive);
                    if (sBuffer.Substring(0, 3) !="GET")
                    {
                        mySocket.Close();
                        return;
                    }
                    iStartPos = sBuffer.IndexOf("HTTP", 1);
                    string sHttpVersion = sBuffer.Substring(iStartPos, 8);
                    sRequest = sBuffer.Substring(0, iStartPos - 1);
                    sRequest.Replace("\\", "/");

                    //如果结尾不是文件名也不是以"/"结尾则加"/"
                    if ((sRequest.IndexOf(".") < 1) && (!sRequest.EndsWith("/")))
                    {
                        sRequest = sRequest + "/";
                    }
                    //得到请求文件名
                    iStartPos = sRequest.LastIndexOf("/") + 1;
                    sRequestedFile = sRequest.Substring(iStartPos);
                    //得到请求文件目录
                    sDirName = sRequest.Substring(sRequest.IndexOf("/"), sRequest.LastIndexOf("/") - 3);
                    //获取虚拟目录物理路径
                    sLocalDir = sMyWebServerRoot;
                    if (sLocalDir.Length == 0)
                    {
                        //404
                        StreamReader fileStream = new StreamReader(sMyWebServerRoot + Path.DirectorySeparatorChar + WebConstant.WEB_404_FILE_PATH);
                        string content = fileStream.ReadToEnd();
                        SendHeader(sHttpVersion, "", content.Length, " 404 Not Found", ref mySocket);
                        SendToBrowser(content, ref mySocket);
                        mySocket.Close();
                        continue;
                    }
                    if (sRequestedFile.Length == 0)
                    {
                        sRequestedFile = Path.DirectorySeparatorChar + WebConstant.WEB_INDEX_FILE_PATH;
                    }

                    String sMimeType ("text/html");
                    sPhysicalFilePath = sLocalDir + sRequestedFile;
                    if (File.Exists(sPhysicalFilePath) == false)
                    {   
                        //404
                        StreamReader fileStream = new StreamReader(sMyWebServerRoot + Path.DirectorySeparatorChar + WebConstant.WEB_404_FILE_PATH);
                        string content = fileStream.ReadToEnd();
                        SendHeader(sHttpVersion, "", content.Length, " 404 Not Found", ref mySocket);
                        SendToBrowser(content, ref mySocket);
                        mySocket.Close();
                        continue;
                    }
                    else
                    {
                        int iTotBytes = 0;
                        sResponse ("");
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

                        SendHeader(sHttpVersion, sMimeType, iTotBytes, " 200 OK", ref mySocket);
                        SendToBrowser(bytes, ref mySocket);
                        //mySocket.Send(bytes, bytes.Length,0);
                    }
                    mySocket.Close();
                }
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
