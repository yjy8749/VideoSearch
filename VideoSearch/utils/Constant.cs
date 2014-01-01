using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VideoSearch
{
    class Constant
    {
        public static readonly string VERSION = "2.2.0";
        public static readonly string ONLINE_HELP_URL = "http://www.icehoney.me/help.html";
        public static readonly string SHARE_SERVICE_EXPLAN_URL = "http://www.icehoney.me/explanation.html";
        public static readonly string PUT_SERVER_INFO_URL = "https://docs.google.com/forms/d/1eJlE4LS-K4_DGuLe9ZYaClwCKe6_Px6YvwR_AKlQ_jE/viewform";
        public static readonly string HOW_TO_GET_MY_SCHOOL_SERVER = "http://www.icehoney.me/help.html#howget";
        public static string SERVICE_ADDRESS = "http://movie.zzti.edu.cn/";
        public static string DEFAULT_DOWNLOAD_DIR = System.Environment.CurrentDirectory;
        public static readonly bool   IS_USE_HTTPS = false;
        public static readonly string SERVICE_TOTAL_XML_URL = @"mov/xml/Total.xml";
        public static readonly string TOTAL_FILE_PATH = "search_info.ahnu";
        public static readonly string CONFIG_FILE_PATH = "config.ahnu";
        public static readonly string SHARE_CONFIG_FILE_PATH = "share_config.ahnu";
        //public static readonly string SERVER_LIST_FILE_URL = "http://127.0.0.1:8000/server_list.ahnu";
        //public static readonly string VERSION_FILE_URL = "http://127.0.0.1:8000/version.ahnu";
        public static readonly string SERVER_LIST_FILE_URL = "http://www.icehoney.me/server_list.ahnu";
        public static readonly string VERSION_FILE_URL = "http://www.icehoney.me/version.ahnu";
        public static readonly string UPDATE_FILE_URL = "http://www.icehoney.me/ahnu_download.zip";
        public static readonly string REPORT_BUG_OR_SUGGEST_URL = "https://docs.google.com/forms/d/1eJlE4LS-K4_DGuLe9ZYaClwCKe6_Px6YvwR_AKlQ_jE/viewform";
        public static readonly string SERVET_LIST_FILE_PATH = "server_list.ahnu";
        public static readonly string SERVIC_CHECK_REGEX = @"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?/";
        public static readonly string TODAY_DATE = DateTime.Now.ToString("yyyyMMdd");
        public static readonly string YESTERDAY_DATE = DateTime.Now.AddDays(-1).ToString("yyyyMMdd"); 
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
        public static ShareForm shareForm = null;
        public static XmlFileModel totalInfo = null;
        public static XmlFileModel config = null;
        public static readonly FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
        static Constant()
        {
            folderBrowserDialog.Description = "请选择目录";
            folderBrowserDialog.SelectedPath = DEFAULT_DOWNLOAD_DIR;
        }
    }
}
