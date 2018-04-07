using System.Collections.Generic;
using System.Linq;
using Sky.Models;

namespace Sky.Analysis
{

    /*
     *1.搜索音乐,得到Id   
     *2.拼接音乐下载链接     
     */
    public class Domain
    {
        /// <summary>
        /// 搜索地址
        /// </summary>
        private string _searchAddress = "http://sou.kuwo.cn/ws/NSearch";
        /// <summary>
        /// 搜索歌曲
        /// </summary>
        /// <param name="name">歌曲名称</param>
        /// <param name="pageNo">页码</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        public List<KwMusic> SearchMusic(string name,int pageNo ,int pageSize = 25)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return new List<KwMusic>();
            }
            var parame = new Dictionary<string,string>()
            {
                {"type","music"},
                {"key",name},
                {"catalog","yueku2016" }

            };
            if (pageNo > 1)
                parame.Add("pn",pageNo.ToString());

            var resultHtml = HttpHelper.DownloadString(_searchAddress, parame);
           

            // 清洗网页
            var resultList = HtmlWash.GetList(resultHtml);
            if(pageSize > 25)// 大于25则需要再获取一页数据
            {
                if (parame.ContainsKey("pn"))
                    parame["pn"] = (pageNo + 1).ToString();
                else
                    parame.Add("pn", (pageNo + 1).ToString());
            }
            else
            {
                resultList = resultList.Take(pageSize).ToList();
            }
            return resultList;
        }     
    }
}
