﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Web;
using System.Net;
using System.IO;

namespace SkyCommon
{
    public class CurlFileTools
    {
        /// <summary>
        /// FORM表单POST方式上传一个多媒体文件
        /// </summary>
        /// <param name="url">API URL</param>
        /// <param name="typeName"></param>
        /// <param name="fileName"></param>
        /// <param name="fs"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string HttpRequestPost(string url, string typeName, string fileName, Stream fs, string encoding = "UTF-8")
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.Timeout = 10000;
            var postStream = new MemoryStream();
            #region 处理Form表单文件上传
            //通过表单上传文件
            string boundary = "----" + DateTime.Now.Ticks.ToString("x");
            string formdataTemplate = "\r\n--" + boundary + "\r\nContent-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: application/octet-stream\r\n\r\n";
            try
            {
                var formdata = string.Format(formdataTemplate, typeName, fileName);
                var formdataBytes = Encoding.ASCII.GetBytes(postStream.Length == 0 ? formdata.Substring(2, formdata.Length - 2) : formdata);//第一行不需要换行
                postStream.Write(formdataBytes, 0, formdataBytes.Length);

                //写入文件
                byte[] buffer = new byte[1024];
                int bytesRead = 0;
                while ((bytesRead = fs.Read(buffer, 0, buffer.Length)) != 0)
                {
                    postStream.Write(buffer, 0, bytesRead);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //结尾
            var footer = Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            postStream.Write(footer, 0, footer.Length);
            request.ContentType = string.Format("multipart/form-data; boundary={0}", boundary);
            #endregion

            request.ContentLength = postStream != null ? postStream.Length : 0;
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            request.KeepAlive = true;
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36";

            #region 输入二进制流
            if (postStream != null)
            {
                postStream.Position = 0;

                //直接写入流
                Stream requestStream = request.GetRequestStream();

                byte[] buffer = new byte[1024];
                int bytesRead = 0;
                while ((bytesRead = postStream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    requestStream.Write(buffer, 0, bytesRead);
                }

                postStream.Close();//关闭文件访问
            }
            #endregion

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (Stream responseStream = response.GetResponseStream())
            {
                using (StreamReader myStreamReader = new StreamReader(responseStream, Encoding.GetEncoding(encoding)))
                {
                    string retString = myStreamReader.ReadToEnd();
                    return retString;
                }
            }
        }

        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="url"></param>
        /// <param name="stream"></param>
        public static void Download(string url, Stream stream)
        {
            WebClient wc = new WebClient();
            var data = wc.DownloadData(url);
            foreach (var b in data)
            {
                stream.WriteByte(b);
            }
        }
    }
}
