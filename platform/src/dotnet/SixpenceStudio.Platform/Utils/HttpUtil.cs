﻿using SixpenceStudio.Platform.Command;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Platform.Utils
{
    /// <summary>
    /// HTTP帮助类
    /// </summary>
    public static class HttpUtil
    {
        const string DEFAULT_USER_AGENT = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727; .NET CLR 4.0.30319)";
        const string DEFAULT_CONTENT_TYPE = "application/json";

        #region 同步
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
        /// 上传单个文件
        /// </summary>
        /// <param name="url"></param>
        /// <param name="uploadFile"></param>
        /// <returns></returns>
        public static string Post(string url, UploadFile uploadFile)
        {
            var client = new RestSharp.RestClient(url);
            var req = new RestSharp.RestRequest();
            req.Method = RestSharp.Method.POST;
            req.AddFile(uploadFile.Name, uploadFile.Data, uploadFile.Filename, uploadFile.ContentType);
            var resp = client.Execute(req);
            if (!resp.IsSuccessful)
            {
                throw new SpException("上传文件失败：" + resp.ErrorException.Message);
            }
            return resp.Content;
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


        #endregion

        #region 异步
        /// <summary>
        /// 异步Get请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headerList"></param>
        /// <returns></returns>
        public async static Task<string> GetAsync(string url, IDictionary<string, string> headerList)
        {
            var request = new HttpClient();
            return await request.GetStringAsync(url);
        }

        /// <summary>
        /// 异步Get请i去
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async static Task<string> GetAsync(string url)
        {
            var request = new HttpClient();
            return await GetAsync(url, null);
        }

        /// <summary>
        /// 异步Post请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public async static Task<string> PostAsync(string url, string postData, string contentType = DEFAULT_CONTENT_TYPE)
        {
            var client = new HttpClient();
            byte[] sendData = Encoding.UTF8.GetBytes(postData);
            var content = new ByteArrayContent(sendData);
            content.Headers.Add("user-agent", DEFAULT_USER_AGENT);
            content.Headers.ContentType = MediaTypeHeaderValue.Parse(contentType);
            return await client.PostAsync(url, content).Result.Content.ReadAsStringAsync();
        }
        #endregion



    }

    public class UploadFile
    {
        public string Name { get; set; }
        public byte[] Data { get; set; }
        public string Filename { get; set; }
        public string ContentType { get; set; }
    }
}
