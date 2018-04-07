using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Text;

namespace Sky.Analysis
{
    public class HttpHelper
    {

        /// <summary>
        /// 简单的下载资源(get)
        /// </summary>
        /// <param name="url">下载地址</param>
        /// <param name="parames">参数</param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string DownloadString(string url, Dictionary<string, string> parames, Encoding encoding = null)
        {
            string result = string.Empty;
            if (string.IsNullOrWhiteSpace(url))
            {
                return result;
            }

            var sb = new StringBuilder();
            if (parames != null && parames.Count > 0)
            {
                foreach (var keyValuePair in parames)
                {
                    sb.Append(sb.Length == 0 ? "?" : "&");

                    sb.Append(keyValuePair.Key);
                    sb.Append("=");
                    sb.Append(keyValuePair.Value);
                }
            }

            // 拼接参数
            url += sb.ToString();
           
            using (var wc = new WebClient())
            {
                wc.Encoding = encoding ?? Encoding.UTF8;
                result = wc.DownloadString(url);
            }
            return result;
        }

        public static string PostData(string url, NameValueCollection data, Encoding encoding, Encoding responseEncoding)
        {
            WebClient client = new WebClient();
            client.Encoding = encoding ?? Encoding.UTF8;
            byte[] response = client.UploadValues(url, "POST", data ?? new NameValueCollection());
            string html = string.Empty;

            if (responseEncoding == null)
            {
                html = client.Encoding.GetString(response);
            }
            else
            {
                html = responseEncoding.GetString(response);
            }
            return html;
        }

        public static string PostData(string url, Dictionary<string, string> data, Encoding encoding, Encoding responseEncoding)
        {
            NameValueCollection postData = new NameValueCollection();
            if (data != null)
            {
                foreach (var item in data)
                {
                    postData.Add(item.Key, item.Value);
                }
            }
            return PostData(url, postData, encoding, responseEncoding);
        }

        public static string PostData(string url, Dictionary<string, string> data, Encoding encoding)
        {
            return PostData(url, data, encoding, null);
        }
    }
}
