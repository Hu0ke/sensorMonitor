﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.IO;

namespace sensorMonitor
{
    class sss
    {
        //body是要传递的参数,格式"roleId=1&uid=2"
        //post的cotentType填写:
        //"application/x-www-form-urlencoded"
        //soap填写:"text/xml; charset=utf-8"
        
        public static string PostHttp(string url, string body, string contentType)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

            httpWebRequest.ContentType = contentType;
            httpWebRequest.Method = "POST";
            httpWebRequest.Timeout = 20000;

            byte[] btBodys = Encoding.UTF8.GetBytes(body);
            httpWebRequest.ContentLength = btBodys.Length;
            httpWebRequest.GetRequestStream().Write(btBodys, 0, btBodys.Length);

            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream());
            string responseContent = streamReader.ReadToEnd();

            httpWebResponse.Close();
            streamReader.Close();
            httpWebRequest.Abort();
            httpWebResponse.Close();

            return responseContent;
        }

        public static string GetHttp(string url, HttpContext httpContext)
        {
         string queryString = "?";

 
         foreach (string key in httpContext.Request.QueryString.AllKeys)
         {
             queryString += key + "=" + httpContext.Request.QueryString[key] + "&";
         }
 
         queryString = queryString.Substring(0, queryString.Length - 1);
 
         HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url + queryString);
 
         httpWebRequest.ContentType = "application/json";
         httpWebRequest.Method = "GET";
         httpWebRequest.Timeout = 20000;
 
         //byte[] btBodys = Encoding.UTF8.GetBytes(body);
         //httpWebRequest.ContentLength = btBodys.Length;
         //httpWebRequest.GetRequestStream().Write(btBodys, 0, btBodys.Length);
 
         HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
         StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream());
         string responseContent = streamReader.ReadToEnd();
 
         httpWebResponse.Close();
         streamReader.Close();
 
         return responseContent;
     }

    }
}
