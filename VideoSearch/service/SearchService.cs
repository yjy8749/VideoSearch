using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace VideoSearch
{
    class SearchService
    {
        public static List<MovieCata> searchMovieCatas(string keyValue)
        {
            List<MovieCata> list = new List<MovieCata>();
            XmlNodeList nodelist = XMLService.getTotalInfo().xmldoc.SelectNodes("root/film");
            float similarity;
            for (int i = 0; i < nodelist.Count; i++)
            {
                similarity=StringSimilarity.compare(nodelist[i].SelectSingleNode("a").InnerText,keyValue);
                if (similarity >= 0.2 ||
                    nodelist[i].SelectSingleNode("c").InnerText.IndexOf(keyValue) != -1 ||
                    nodelist[i].SelectSingleNode("d").InnerText.IndexOf(keyValue) != -1 )
                {
                    MovieCata mov = new MovieCata();
                    mov.name = nodelist[i].SelectSingleNode("a").InnerText;
                    mov.code = nodelist[i].SelectSingleNode("b").InnerText;
                    mov.describe = nodelist[i].SelectSingleNode("s").InnerText;
                    mov.similarity = similarity;
                    list.Add(mov);
                }
            }
            return list;
        }
    }
}
