using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VideoSearch
{
    class Constant
    {
        public static readonly string ONLINE_HELP_URL = "http://www.baidu.com";
        public static readonly string PUT_SERVER_INFO_URL = "http://www.baidu.com";
        public static readonly string HOW_TO_GET_MY_SCHOOL_SERVER = "http://www.baidu.com";
        public static string SERVICE_ADDRESS = "http://movie.zzti.edu.cn/";
        public static string DEFAULT_DOWNLOAD_DIR = System.Environment.CurrentDirectory;
        public static readonly bool   IS_USE_HTTPS = false;
        public static readonly string SERVICE_TOTAL_XML_URL = @"mov/xml/Total.xml";
        public static readonly string TOTAL_FILE_PATH = "search_info.ahnu";
        public static readonly string CONFIG_FILE_PATH = "config.ahnu";
        public static readonly string SERVER_LIST_FILE_URL = "http://127.0.0.1:8000/server_list.ahnu";
        public static readonly string SERVET_LIST_FILE_PATH = "server_list.ahnu";
        public static readonly string SERVIC_CHECK_REGEX = @"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?/";
        public static readonly string TODAY_DATE = DateTime.Now.ToString("yyyyMMdd");
        public static readonly string YESTERDAY_DATE = DateTime.Now.AddDays(-1).ToString("yyyyMMdd"); 
        public static readonly string ABOUT_US_MSG = "软件名称：校园高清视频网视频下载器\n" +
                                            "软件版本：2.1.0\n"+
                                            "软件官网：http://www.icehoneyme.com\n"+
                                            "软件作者：杨家勇  王彪  姚岁岁\n"+
                                            "代码仓库：\n"+
                                            "开源声明：本软件所有权力归作者所有，任何企业和个人不得以任何理由将本软件用于商业用途。\n";
        public static readonly string USER_AGENT = @"Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 2.0.40607; .NET CLR 1.1.4322)";
        public static readonly string DOWNLOAD_USER_AGENT = @"Novasoft NetPlayer/4.0";
        public static readonly string FILE_TYPE = ".ahnu";
        public static readonly int RECORD_LIST_SIZE=5;
        public static readonly int DOWNLOAD_THREAD_COUNT = 8;
        public static readonly short AUTO_DECRYPT_MODEL = 0;
        public static readonly short FORCIBLY_DECRYPT_MODEL = 1;
        public static readonly short NO_DECRYPT_MODEL = -1;

        public static SetForm setForm = null;
        public static ExploreForm exploreForm = null;
        public static DownLoadForm downloadForm = null;
        public static MainForm mainForm = null;
        public static XmlFileModel totalInfo = null;
        public static XmlFileModel config = null;
        public static readonly FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
        static Constant()
        {
            folderBrowserDialog.SelectedPath = DEFAULT_DOWNLOAD_DIR;
        }
    }
}
