using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace VideoSearch
{
    class AnalyzeService
    {       
        public static Message analyzeKeyValue(string keyValue)
        {
            Message msg = new Message();
            if (keyValue == null || keyValue.Equals(""))
            {
                msg.isSucceed = false;
                msg.msg = MsgString.SEARCH_INFO_CAN_NOT_NULL ;
                return msg;
            }
            if (keyValue.Length > 7 && keyValue.Substring(0, 7).ToLower().Equals(Constant.IS_USE_HTTPS ? "https:/" : "http://"))
            {
                try
                {
                    string[] datas = keyValue.Split('?');
                    string[] parm = datas[1].Split('&');
                    string code = parm[0].Substring(5);
                    msg.isSucceed = true;
                    msg.movieCataList = new List<MovieCata>();
                    MovieCata movcata = new MovieCata(code);
                    movcata.analyze();
                    msg.movieCataList.Add(movcata);
                    return msg;
                }
                catch(Exception e)
                {
                    msg.isSucceed = false;
                    msg.msg = MsgString.SEARCH_INFO_IS_NOT_RIGHT;
                    return msg;
                }
            }
            
            msg.movieCataList = SearchService.searchMovieCatas(keyValue);
            msg.movieCataList = msg.movieCataList.OrderByDescending<MovieCata, float>(mc => mc.similarity).ToList<MovieCata>();
            msg.isSucceed = true;
            msg.msg = MsgString.ANALYZE_SUCCESS.Replace("%num%", msg.movieCataList.Count.ToString());
            return msg;
        }
        public static Message newResource()
        {
            Message msg = new Message();
            msg.movieCataList = SearchService.searchMovieCatasaAfter(DateTime.Now.AddDays(-7));
            msg.movieCataList = msg.movieCataList.OrderByDescending<MovieCata, float>(mc => mc.similarity).ToList<MovieCata>();
            msg.isSucceed = true;
            msg.msg = MsgString.ANALYZE_SUCCESS.Replace("%num%", msg.movieCataList.Count.ToString());
            return msg;
        }
        public static Message allResource()
        {
            Message msg = new Message();
            msg.movieCataList = SearchService.searchAllMovieCatas();
            msg.movieCataList = msg.movieCataList.OrderByDescending<MovieCata, string>(mc => mc.describe).ToList<MovieCata>();
            msg.isSucceed = true;
            msg.msg = MsgString.ANALYZE_SUCCESS.Replace("%num%", msg.movieCataList.Count.ToString());
            return msg;
        }
    }
}
