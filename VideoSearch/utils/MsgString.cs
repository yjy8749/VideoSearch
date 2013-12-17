using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VideoSearch
{
    class MsgString
    {
        public static readonly string SEARCH_INFO_CAN_NOT_NULL = "搜索信息不能为空";
        public static readonly string SEARCH_INFO_IS_NOT_RIGHT = "网址格式不正确，请重新输入";
        public static readonly string THIS_MOVIE_NOT_EXIST = "服务器没有本视频资源";
        public static readonly string NOW_IS_DOING_WORK = "正在处理《%name%》......";
        public static readonly string ADD_DOWNLOAD_QUEUE = "添加下载任务《%name%》......";
        public static readonly string ANALYZE_SUCCESS = "解析成功，共%num%条数据";
        public static readonly string ANALYZE_URL_FILE_FAILED = "解析失败，请检查网络连接";
        public static readonly string ANALYZE_XML_FILE_FAILED = "解析失败，请检查配置网址";
        public static readonly string COPY_DOWNLOAD_URL_SUCCESS = "下载地址复制成功";
        public static readonly string ALL_MOVIE_ADDRESS_ANALYZE_COMPELTE = "全部视频地址解析完成";
        public static readonly string DOWNLOAD_MOVIE_SUCCESS = "视频下载完成";
        public static readonly string SERVICE_IP_NOT_RIGHT = @"网址不合法，必须类似:http://210.45.193.47/";
        public static readonly string SAVE_CONFIG_FILE_FAILED = @"保存配置信息出错，请以管理员身份运行";
        public static readonly string RESET_SOFTWARE_SUCCESS = "软件已还原到初始状态";
        public static readonly string CANCLE_DOWNLOAD_QUEUE = "任务已取消";
    }
}
