using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Platform.Utils
{
    public static class HttpUtils
    {
        const string DEFAULT_USER_AGENT = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727; .NET CLR 4.0.30319)";
        const string DEFAULT_CONTENT_TYPE = "application/json";

        /// <summary>
        /// GET请求，可以添加自定义的Header信息
        /// </summary>
        /// <returns></returns>
        public static string Get(string url, IDictionary<string, string> headerList)
        {
            var request = WebRequest.Create(url) as HttpWebRequest;
            request.UserAgent = DEFAULT_USER_AGENT;
            request.ContentType = DEFAULT_CONTENT_TYPE;
            if (headerList != null)
            {
                foreach (var header in headerList)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }

            var responseStream = request.GetResponse().GetResponseStream();

            if (responseStream == null) return string.Empty;

            using (var reader = new StreamReader(responseStream))
            {
                return reader.ReadToEnd();
            }
        }

        /// <summary>
        /// GET请求
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string Get(string url)
        {
            return Get(url, null);
        }

        /// <summary>
        /// POST请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static string Post(string url, string postData, string contentType = DEFAULT_CONTENT_TYPE)
        {
            var webClient = new WebClient();
            webClient.Headers.Add("user-agent", DEFAULT_USER_AGENT);
            webClient.Headers.Add("Content-Type", contentType);
            byte[] sendData = Encoding.UTF8.GetBytes(postData);
            byte[] responseData = webClient.UploadData(url, "POST", sendData);
            return Encoding.UTF8.GetString(responseData);
        }

        /// <summary>
        /// POST请求，并可以传入Header信息
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <param name="headParams"></param>
        /// <returns></returns>
        public static string Post(string url, string postData, IDictionary<string, string> headParams)
        {
            var webClient = new WebClient();
            webClient.Headers.Add("user-agent", DEFAULT_USER_AGENT);
            webClient.Headers.Add("Content-Type", "application/json");
            if (headParams != null && headParams.Count > 0)
            {
                foreach (var item in headParams)
                {
                    if (webClient.Headers.AllKeys.Contains(item.Key))
                    {
                        webClient.Headers.Remove(item.Key);
                    }
                    webClient.Headers.Add(item.Key, item.Value);
                }
            }

            byte[] sendData = Encoding.UTF8.GetBytes(postData);
            byte[] responseData = webClient.UploadData(url, "POST", sendData);
            return Encoding.UTF8.GetString(responseData);
        }
    }
}
