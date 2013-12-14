using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VideoSearch
{
    class WebConstant
    {
        public static WebService webService = null;
        public static string LOCAL_URL = "http://127.0.0.1:9999";
        public static string WEB_404_FILE_PATH="404.html";
        public static string WEB_INDEX_FILE_PATH="index.html";
        public static string WEB_LIST_FILE_PATH = "list.html";
        public static string WEB_VIEW_FILE_PATH = "view.html";
        public static string[] SHARE_DIRS = null;
        public static string SHARE_DIRS_STRING = null;
        public static string ALLOW_IP_TABLE = "";
        public static string indexTmpFile = null;
        public static string listTmpFile = null;
        public static string viewTmpFIle = null;
        public static string error404TmpFile = null;
        public static string viewModel = "<li class='content'>{name}<span><a href='/play?model=0&name={name}&url={url}'>打开(自动)</a>--<a href='/play?url={url}&model=1&name={name}'>打开(强制)</a>--<a href='{url}'>打开(直接)</a></span></li>";
        public static string noFileViewModel = "<li class='content'>{name}<span>--{url}--</span></li>";
        public static string listModel = "<li class='content'><a href='/view?code={code}'>{name}</a><h5>{describe}</h5></li>";
        public static string shareListModel = "<li class='content'><a href='/{url}'>{name}</a></li>";
    }
}
