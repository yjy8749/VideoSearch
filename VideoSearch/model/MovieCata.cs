using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;

namespace VideoSearch
{
    public class MovieCata
    {
        private bool isDSJ;
        public string name;
        public string code;
        private string url;
        public string describe;
        public float similarity;
        public List<Movie> movieList = new List<Movie>();
        public MovieCata() { }
        public MovieCata(string code)
        {
            this.code = code;
        }
        public Message analyze()
        {
            Message msg = new Message();
            this.url = Constant.SERVICE_ADDRESS + "mov/" + code + "/url.xml";
            string content=HttpFileModel.load(this.url).content;
            if (content == null)
            {
                msg.isSucceed = false;
                msg.msg = MsgString.ANALYZE_URL_FILE_FAILED;
                return msg;
            }
            XmlDocument XML = new XmlDocument();
            string playurl;
            try
            {
                XML.LoadXml(content);
                playurl = XML.SelectSingleNode("root/b").InnerText;
                this.name = XML.SelectSingleNode("root/a").InnerText;
            }
            catch (Exception e)
            {
                msg.isSucceed = false;
                msg.msg = MsgString.ANALYZE_XML_FILE_FAILED;
                return msg;
            }
            playurl = playurl.Replace("\n", "");
            playurl = playurl.Replace("\r", "");
            string [] codes = playurl.Split(',');
            if (codes.Length > 2) this.isDSJ = true;
            this.movieList.Clear();
            for (int i = 0; i < codes.Length-1; i++)
            {
                if (this.isDSJ)
                {
                    this.movieList.Add(new Movie(MovieCata.formatMovieName(this.name,i,codes.Length),this.code,i));
                }
                else
                {
                    this.movieList.Add(new Movie(this.name,this.code,i));
                }
            }
            msg.isSucceed = true;
            msg.movieCataList = new List<MovieCata>();
            msg.movieCataList.Add(this);
            msg.msg = MsgString.ANALYZE_SUCCESS.Replace("%num%", this.movieList.Count.ToString());
            return msg;
        }
        private static string formatMovieName(string name,int num,int length)
        {
            length = length.ToString().Length - 1;
            num += 1;
            if (num >= Math.Pow(10, length))
            {
                return name += " 第" + num.ToString() + "集";
            }
            else
            {
                return name += " 第" + num.ToString().PadLeft(length + 1, '0') + "集";
            }
        }
    }
}
