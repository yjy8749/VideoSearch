using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading ;
using System.Diagnostics;

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
                if ( this.webListener != null)
                {
                   msg.msg = "共享服务已启动";
                   return msg;
                }
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
            isListen = false;
            listenThread.Abort();
            webListener.Stop();
            return "共享服务已停止";
        }
        private bool isListen = true;
        public void startListen()
        {
            while (isListen)
            {
                Socket acceptSocket = webListener.AcceptSocket();
                string clienIp=acceptSocket.RemoteEndPoint.ToString();
                if (WebConstant.ALLOW_IP_TABLE.Equals("")||clienIp.Split(':')[0].IndexOf(WebConstant.ALLOW_IP_TABLE) > 0)
                {
                    Thread th = new Thread(doSocket);
                    th.Start((object)acceptSocket);
                }
            }
        }
        private void doSocket(object objectSocket)
        {
            Socket acceptSocket = (Socket)objectSocket;
            if (acceptSocket.Connected)
            {
                int iStartPos = 0;
                String sRequest;
                Byte[] bReceive = new Byte[1024];
                int i = acceptSocket.Receive(bReceive, bReceive.Length, 0);
                string requestInfo = Encoding.ASCII.GetString(bReceive);
                if (requestInfo.Substring(0, 3) != "GET")
                {
                    //非GET请求
                    acceptSocket.Close();
                    return;
                }
                iStartPos = requestInfo.IndexOf("HTTP", 1);
                string sHttpVersion = requestInfo.Substring(iStartPos, 8);
                sRequest = requestInfo.Substring(0, iStartPos - 1);
                sRequest.Replace("\\", "/");
                sRequest = sRequest.Substring(sRequest.IndexOf("/"));
                this.doRequest(sRequest, sHttpVersion, ref acceptSocket);
                acceptSocket.Close();
            }
        }
        private static String webServerRoot = "WEB"; //设置虚拟目录  
        public void doRequest(string sRequest, string sHttpVersion,ref Socket acceptSocket)
        {
            if (sRequest.IndexOf('.') > 0&&sRequest.IndexOf("play")<0)
            {
                this.doFileRequest(sRequest, sHttpVersion, ref acceptSocket);
                return;
            }
            else
            {
                this.doServerRequest(sRequest, sHttpVersion, ref acceptSocket);
                return;
            }
        }

        private void doServerRequest(string sRequest, string sHttpVersion, ref Socket acceptSocket)
        {
            string[] urlInfo = sRequest.Split('?');
            switch (urlInfo[0])
            {
                case "/":
                    {
                        if (WebConstant.indexTmpFile == null)
                        {
                            WebConstant.indexTmpFile = this.readToEnd(webServerRoot + Path.DirectorySeparatorChar + WebConstant.WEB_INDEX_FILE_PATH);
                        }
                        //this.SendHeader(sHttpVersion, "", WebConstant.indexTmpFile.Length, "200", ref acceptSocket);
                        this.SendToBrowser(WebConstant.indexTmpFile.Replace("{url}",WebConstant.LOCAL_URL), ref acceptSocket);
                        break;
                    }
                case "/share":
                    {
                        this.doShareRequest(ref acceptSocket);
                        break;
                    }
                case "/search":
                    {
                        if (urlInfo.Length < 2)
                        {
                            if (WebConstant.error404TmpFile == null)
                            {
                                WebConstant.error404TmpFile = this.readToEnd(webServerRoot + Path.DirectorySeparatorChar + WebConstant.WEB_404_FILE_PATH);
                            }
                            this.SendHeader(sHttpVersion, "", WebConstant.error404TmpFile.Length, "404", ref acceptSocket);
                            this.SendToBrowser(WebConstant.error404TmpFile, ref acceptSocket);
                            break;
                        }
                        string[] parms = urlInfo[1].Split('&')[0].Split('=');
                        this.doSearchRequest(parms, ref acceptSocket);
                        break;
                    }
                case "/view":
                    {
                        if (urlInfo.Length < 2)
                        {
                            if (WebConstant.error404TmpFile == null)
                            {
                                WebConstant.error404TmpFile = this.readToEnd(webServerRoot + Path.DirectorySeparatorChar + WebConstant.WEB_404_FILE_PATH);
                            }
                            this.SendHeader(sHttpVersion, "", WebConstant.error404TmpFile.Length, "404", ref acceptSocket);
                            this.SendToBrowser(WebConstant.error404TmpFile, ref acceptSocket);
                            break;
                        }
                        string[] parms = urlInfo[1].Split('&')[0].Split('=');
                        this.doViewRequest(parms, ref acceptSocket);
                        break;
                    }
                case "/play":
                    {
                        if (urlInfo.Length < 2)
                        {
                            if (WebConstant.error404TmpFile == null)
                            {
                                WebConstant.error404TmpFile = this.readToEnd(webServerRoot + Path.DirectorySeparatorChar + WebConstant.WEB_404_FILE_PATH);
                            }
                            this.SendHeader(sHttpVersion, "", WebConstant.error404TmpFile.Length, "404", ref acceptSocket);
                            this.SendToBrowser(WebConstant.error404TmpFile, ref acceptSocket);
                            break;
                        }
                        string[] parms = urlInfo[1].Split('&');
                        this.doPlayRequest(parms,sHttpVersion, ref acceptSocket);
                        break;
                    }
                case "/shutdown":
                    {
                        if (urlInfo.Length < 2)
                        {
                            if (WebConstant.error404TmpFile == null)
                            {
                                WebConstant.error404TmpFile = this.readToEnd(webServerRoot + Path.DirectorySeparatorChar + WebConstant.WEB_404_FILE_PATH);
                            }
                            this.SendHeader(sHttpVersion, "", WebConstant.error404TmpFile.Length, "404", ref acceptSocket);
                            this.SendToBrowser(WebConstant.error404TmpFile, ref acceptSocket);
                            break;
                        }
                        string [] parms = urlInfo[1].Split('=');
                        this.doShutDownRequest(parms, sHttpVersion, ref acceptSocket);
                        break;
                    }
            }
        }

        private void doShutDownRequest(string[] parms, string sHttpVersion, ref Socket acceptSocket)
        {
            if (WebConstant.SHUT_DOWN_PASSWORD.Equals(""))
            {
                return;
            }
            if (parms.Length > 1)
            {
                if (parms[0].Equals("password") && parms[1].Equals(WebConstant.SHUT_DOWN_PASSWORD))
                {
                    Process myProcess = new Process();
                    myProcess.StartInfo.FileName = "cmd.exe";
                    myProcess.StartInfo.UseShellExecute = false;
                    myProcess.StartInfo.RedirectStandardInput = true;
                    myProcess.StartInfo.RedirectStandardOutput = true;
                    myProcess.StartInfo.RedirectStandardError = true;
                    myProcess.StartInfo.CreateNoWindow = true; myProcess.Start();
                    myProcess.StandardInput.WriteLine("shutdown -s -t 60");
                }
            }
        }

        private void doShareRequest(ref Socket acceptSocket)
        {
            if (shareFileList.Count == 0)
            {
                foreach (string dir in WebConstant.SHARE_DIRS)
                {
                    this.getDirFiles(new DirectoryInfo(dir), dir);
                }
            }
            string str;
            string tmp = "";
            foreach (string file in shareFileList)
            {
                str = WebConstant.shareListModel;
                str = str.Replace("{name}", file);
                str = str.Replace("{url}", file);
                tmp = tmp + str;
            }
            if (WebConstant.listTmpFile == null)
            {
                WebConstant.listTmpFile = this.readToEnd(webServerRoot + Path.DirectorySeparatorChar + WebConstant.WEB_LIST_FILE_PATH);
            }
            this.SendToBrowser(WebConstant.listTmpFile.Replace("{content}", tmp), ref acceptSocket);
        }
        private List<string> shareFileList = new List<string>();
        public void getDirFiles(FileSystemInfo info,string localDir)
        {
            if (!info.Exists) return;
            DirectoryInfo dir = info as DirectoryInfo;
            if (dir == null) return;
            FileSystemInfo[] files = dir.GetFileSystemInfos();
            for (int i = 0; i < files.Length; i++)
            {
                FileInfo file = files[i] as FileInfo;
                if (file != null)
                {
                    shareFileList.Add(files[i].FullName.Substring(localDir.Length + 1).Replace("\\", "/"));
                }
                else
                {
                    getDirFiles(files[i], localDir);
                }
            }
        } 
        private void doPlayRequest(string[] parms,string httpVersion, ref Socket acceptSocket)
        {
            if (parms.Length > 1)
            {
                string url=null, model="0",name="text";
                foreach (string str in parms)
                {
                    string[] strs = str.Split('=');
                    if (strs.Length > 1)
                    {
                        switch (strs[0])
                        {
                            case "url":
                                {
                                    url = strs[1];
                                    break;
                                }
                            case "model":
                                {
                                    model = strs[1];
                                    break;
                                }
                            case "name":
                                {
                                    name = strs[1];
                                    break;
                                }
                        }
                    }
                }
                if (url != null)
                {
                    if (url.EndsWith(".mp4") || url.EndsWith(".mkv")) model = "1";
                    HttpRedirectFile httpfile = new HttpRedirectFile(url, short.Parse(model), ref acceptSocket);
                    string file_type = url.Substring(url.LastIndexOf('.') + 1);
                    this.SendHeader(httpVersion, ContentType.get(file_type), httpfile.getLength(), "200", ref acceptSocket);
                    httpfile.startRedirect();
                }
            }
        }

        private void doViewRequest(string[] parms, ref Socket acceptSocket)
        {
            if (parms.Length > 1)
            {
                if (parms[0].Equals("code"))
                {

                    Message msg = new MovieCata(Uri.UnescapeDataString(parms[1])).analyze();
                    if (msg.isSucceed)
                    {
                        string tmp = "";
                        string str;
                        while (true)
                        {
                            if (Movie.isAllComplete()) break;
                            Thread.Sleep(1000);
                        }
                        foreach (Movie movie in msg.movieCataList[0].movieList)
                        {
                            if (movie.url.Equals(MsgString.THIS_MOVIE_NOT_EXIST))
                            {
                                str = WebConstant.noFileViewModel;
                            }
                            else
                            {
                                str = WebConstant.viewModel;
                            }
                            str = str.Replace("{name}", movie.name);
                            str = str.Replace("{url}", movie.url);
                            tmp = tmp + str;
                        }
                        if (WebConstant.listTmpFile == null)
                        {
                            WebConstant.listTmpFile = this.readToEnd(webServerRoot + Path.DirectorySeparatorChar + WebConstant.WEB_LIST_FILE_PATH);
                        }
                        this.SendToBrowser(WebConstant.listTmpFile.Replace("{content}", tmp), ref acceptSocket);
                    }
                }
            }
        }

        private void doSearchRequest(string[] parms , ref Socket acceptSocket)
        {
            if (parms.Length > 1)
            {
                if (parms[0].Equals("keyValue"))
                {
                    Message msg;
                    switch (Uri.UnescapeDataString(parms[1]))
                    {
                        case "newResource":
                            {
                                msg= AnalyzeService.newResource();
                                break;
                            }
                        case "allResource":
                            {
                                msg = AnalyzeService.allResource();
                                break;
                            }
                        default:
                            {
                                msg = AnalyzeService.analyzeKeyValue(Uri.UnescapeDataString(parms[1]));
                                break;
                            }
                    }

                    if (msg.isSucceed)
                    {
                        string tmp = "";
                        string str;
                        foreach (MovieCata mc in msg.movieCataList)
                        {
                            str = WebConstant.listModel;
                            str=str.Replace("{name}", mc.name);
                            str=str.Replace("{code}", mc.code);
                            str = str.Replace("{describe}", mc.describe);
                            tmp = tmp + str;
                        }
                        if (WebConstant.listTmpFile == null)
                        {
                            WebConstant.listTmpFile = this.readToEnd(webServerRoot + Path.DirectorySeparatorChar + WebConstant.WEB_LIST_FILE_PATH);
                        }
                        this.SendToBrowser(WebConstant.listTmpFile.Replace("{content}",tmp),ref acceptSocket);
                    }
                }
            }
        }

        private void doFileRequest(string file,string httpVersion , ref Socket acceptSocket)
        {
            string[] urlInfo = file.Split('?');
            file = Uri.UnescapeDataString(urlInfo[0]);
            file = file.Replace("/","\\");
            string sStatusCode = "200";
            if (File.Exists(webServerRoot + file))
            {
                file = webServerRoot + file;
            }
            else
            {
                string tmp_sStatusCode = "404";
                string tmp_file = webServerRoot + Path.DirectorySeparatorChar + WebConstant.WEB_404_FILE_PATH;
                if (WebConstant.SHARE_DIRS != null)
                {
                    foreach (string str in WebConstant.SHARE_DIRS)
                    {
                        if (File.Exists(str + file))
                        {
                            tmp_sStatusCode = "200";
                            tmp_file = str + file;
                            break;
                        }
                        tmp_sStatusCode = "404";
                        tmp_file = webServerRoot + Path.DirectorySeparatorChar + WebConstant.WEB_404_FILE_PATH;
                    }
                }
                file = tmp_file;
                sStatusCode = tmp_sStatusCode;
            }

            FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read);
            string file_type = file.Substring(file.LastIndexOf('.')+1);
            this.SendHeader(httpVersion, ContentType.get(file_type),fs.Length, sStatusCode, ref acceptSocket);
            byte[] buffer = new byte[1024];
            int read =0;
            do
            {
                buffer.Initialize();
                read = fs.Read(buffer, 0, buffer.Length);
                SendToBrowser(buffer, ref acceptSocket);
            }
            while (read > 0);
            fs.Close();
        }
        private string readToEnd(string file)
        {
            StreamReader fs = new StreamReader(file);
            return fs.ReadToEnd();
        }
        public void SendHeader(string sHttpVersion, string sMIMEHeader, long iTotBytes, string sStatusCode, ref Socket mySocket)
        {
            String sBuffer ="";
            if (sMIMEHeader.Length == 0)
            {
                sMIMEHeader ="text/html"; // 默认 text/html
            }
            sBuffer = sBuffer + sHttpVersion + sStatusCode + "\r\n";
            sBuffer = sBuffer + "Server: AHNU-100705066\r\n";
            sBuffer = sBuffer + "Content-Type: " + sMIMEHeader + "\r\n";
            sBuffer = sBuffer + "Accept-Ranges: bytes\r\n";
            sBuffer = sBuffer + "Content-Length: " + iTotBytes + "\r\n\r\n";
            Byte[] bSendData = Encoding.UTF8.GetBytes(sBuffer);
            SendToBrowser(bSendData, ref mySocket);
        }
        public void SendHeader(string sHttpVersion, string sMIMEHeader, long iTotBytes, string sStatusCode,string file, ref Socket mySocket)
        {
            String sBuffer = "";
            if (sMIMEHeader.Length == 0)
            {
                sMIMEHeader = "text/html"; // 默认 text/html
            }
            sBuffer = sBuffer + sHttpVersion + sStatusCode + "\r\n";
            sBuffer = sBuffer + "Server: AHNU-100705066\r\n";
            sBuffer = sBuffer + "Content-Type: " + sMIMEHeader + "\r\n";
            sBuffer = sBuffer + "Accept-Ranges: bytes\r\n";
            sBuffer = sBuffer + "Content-Length: " + iTotBytes + "\r\n";
            sBuffer = sBuffer + "Content-Disposition: attachment;fileName=" + file + "\r\n\r\n";
            Byte[] bSendData = Encoding.UTF8.GetBytes(sBuffer);
            SendToBrowser(bSendData, ref mySocket);
        }
        public void SendToBrowser(String sData, ref Socket mySocket)
        {
            SendToBrowser(Encoding.UTF8.GetBytes(sData), ref mySocket);
        }

        public void SendToBrowser(Byte[] bSendData, ref Socket mySocket)
        {
            try
            {
                if (mySocket.Connected)
                {
                    mySocket.Send(bSendData, bSendData.Length, 0);
                }
            }
            catch (Exception e)
            {
            }
        }
        
    }
}
