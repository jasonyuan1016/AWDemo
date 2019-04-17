using System.Web;
using System.IO;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;


namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// 抓取当前开奖结果
        /// </summary>
        [TestMethod]
        public void TestMethod1()
        {
            string htmlInfo = GetHtmlInfo("http://caipiao.163.com/");
            Regex reg = new Regex(@"<div class=""awardDetail"">([\s\S]+?)<\/div>");
            htmlInfo = GetElementContent(htmlInfo, reg)[0];
            reg = new Regex(@"<strong class=""c_ba2636"">(.*?)<\/strong>");
            string numb = GetElementContent(htmlInfo, reg)[0];
            reg = new Regex(@"<em class=""smallRedball"">(.*?)<\/em>");
            string[] smallRedball = GetElementContent(htmlInfo, reg);
            reg = new Regex(@"<em class=""smallBlueball"">(.*?)<\/em>");
            string[] smallBlueball = GetElementContent(htmlInfo, reg);
        }

        string GetHtmlInfo(string url)
        {
            WebRequest request = WebRequest.Create(url);
            string htmlinfo = string.Empty;
            using (WebResponse response = request.GetResponse())
            {
                using (Stream resStream = response.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(resStream, System.Text.Encoding.UTF8))
                    {
                        htmlinfo = sr.ReadToEnd();
                    }
                }
            }
            return htmlinfo;
        }

        /// <summary>
        /// 获得指定元素的内容
        /// </summary>
        /// <param name="str">元字符</param>
        /// <param name="reg"></param>
        /// <returns></returns>
        string[] GetElementContent(string str, Regex reg)
        {
            var regMatches = reg.Matches(str);
            string[] retString = new string[regMatches.Count];
            int i = 0;
            foreach (Match mtc in regMatches)
            {
                if (mtc.Success && mtc.Groups.Count > 1)
                {
                    retString[i] += mtc.Groups[1].Value;
                }
                i++;
            }
            return retString;
        }
    }
}
