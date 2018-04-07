using System.Collections.Generic;
using AngleSharp.Parser.Html;
using Sky.Models;

namespace Sky.Analysis
{
    /// <summary>
    /// 网页清洗
    /// </summary>
    public class HtmlWash
    {
        /// <summary>
        /// 下载地址
        /// </summary>
        private static string _DlUrl = "http://antiserver.kuwo.cn/anti.s?format=aac|mp3&rid={0}&type=convert_url&response=res";
        /// <summary>
        /// 取出酷我音乐列表数据
        /// </summary>
        /// <param name="htmlSource">html文本</param>
        /// <returns></returns>
        public static List<KwMusic> GetList(string htmlSource)
        {
            if (string.IsNullOrWhiteSpace(htmlSource))
            {
                return new List<KwMusic>();
            }

            var kwMusicLst = new List<KwMusic>();

            //创建一个（可重用）解析器
            var parser = new HtmlParser();

            // parse为htmldocument对象
            var document = parser.Parse(htmlSource);

            var musicLstHtml = document.QuerySelectorAll("li.clearfix");

            if (musicLstHtml != null)
            {
                foreach (var element in musicLstHtml)
                {
                    kwMusicLst.Add(new KwMusic()
                    {
                        AlbumName = element.QuerySelector(".a_name a")?.GetAttribute("title"),
                        Name = element.QuerySelector(".m_name a")?.GetAttribute("title"),
                        SingerName = element.QuerySelector(".s_name a")?.GetAttribute("title"),
                        DlUrl = string.Format(_DlUrl, element.QuerySelector("p.number input")?.GetAttribute("value"))
                    });
                }
            }
           

            return kwMusicLst;
        }
    }

    
}
