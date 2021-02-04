using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

namespace mapbaidu
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Program pc = new Program();
                Console.WriteLine(pc.GetIPlist());
                var str = pc.mapbaidu("https://map.baidu.com/?qt=ipLocation&t=", "", "utf-8");
                Console.WriteLine(pc.ConvertUnicodeStringToChinese(str));//无法直接转unciode
                Console.WriteLine("\r\n");
            }
            catch (Exception eN)
            {
                // 错误处理代码
                Console.WriteLine(eN.Message);
            }
            finally
            {
                // 要执行的语句
                Console.WriteLine("\r\n");
            }           
        }
        public string ConvertUnicodeStringToChinese(string unicodeString)
        {
            if (string.IsNullOrEmpty(unicodeString))
                return string.Empty;

            string outStr = unicodeString;

            System.Text.RegularExpressions.Regex re = new System.Text.RegularExpressions.Regex("\\\\u[0123456789abcdef]{4}", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.MatchCollection mc = re.Matches(unicodeString);
            foreach (System.Text.RegularExpressions.Match ma in mc)
            {
                outStr = outStr.Replace(ma.Value, ConverUnicodeStringToChar(ma.Value).ToString());
            }
            return outStr;
        }

        private static char ConverUnicodeStringToChar(string str)
        {
            char outStr = Char.MinValue;
            outStr = (char)int.Parse(str.Remove(0, 2), System.Globalization.NumberStyles.HexNumber);
            return outStr;
        }  
        public string GetIPlist()
        {
            ///获取本机IP列表
            string AddressIP = string.Empty;
            foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    AddressIP += _IPAddress.ToString()+ "\n";
                }
            }
            return AddressIP;
        }
        public string mapbaidu(string uri, string refererUri, string encodingName)
        {
            string html = string.Empty;
            System.Net.ServicePointManager.DefaultConnectionLimit = 5; //链接超时为5秒
            //设置cookie
            CookieContainer cookieContainer = new CookieContainer();
            cookieContainer.Add(new Cookie("BAIDUID", "67017F5C6A5EE8351192F7D34E7A221E:FG=1", "", "map.baidu.com"));
            cookieContainer.Add(new Cookie("BIDUPSID", "A29EA919049CED566C183C7ED175C6AB", "", "map.baidu.com"));
            //设置请求
            //UriBuilder uri = new UriBuilder("");
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri.Uri);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.ContentType = "text/html;charset=" + encodingName; //设置编码
            request.Method = "GET"; // 默认为GET
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36";// 设置请求ua
            request.CookieContainer = cookieContainer;
            request.KeepAlive = false; //关闭持续回话
            //referer
            if (!string.IsNullOrEmpty(refererUri))
                request.Referer = refererUri;
            //获取网页返回
            string str = string.Empty;
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                var stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                str = reader.ReadToEnd();
            }
            catch (Exception eN) //网络访问出错
            {
                str = eN.Message;
            }
            return str;
        }
    }
}
