using System;//网页链接内容转txt
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace Web_Homework
{
    class TextDL
    {
        public static string[] ReadXML()
        {
            string xmlName = "HyperLinks2.xml";
            //string xmlName = "HyperLinks1.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlName);
            XmlNode xn = doc.SelectSingleNode("HyperLinks");
            XmlNodeList xnl = xn.ChildNodes;
            string[] URL = new string[xnl.Count];
            for (int i=0;i<xnl.Count;i++)
            {
                URL[i] = xnl[i].ChildNodes[0].InnerText;
            }
            return URL;
        }
        public static void ReadWeb(string[] URL)
        {
            for (int i = 0; i < URL.Length; i++)
            {
                Uri uri = new Uri("http://"+URL[i]);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                request.Method = "Get";
                request.KeepAlive = false;
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                string html = reader.ReadToEnd();
                string regex = @"<title>.+</title>";
                string title = Regex.Match(html, regex).ToString();
                string r1 = @"^(<title>)";
                title=Regex.Replace(title, r1, "");
                string r2 = @"(</title>)" + "$";
                title = Regex.Replace(title, r2, "");
                ArrayList text = new ArrayList();
                text.Add(title);
                /*foreach (Match m in Regex.Matches(html, @"\<p\>(.*?)\</p\>"))
                {
                    text.Add(m.Groups[1].Value);
                }*/
                foreach (Match m in Regex.Matches(html, "(?<=(<p[.\\s\\S]*?>))[.\\s\\S]*?(?=(</p>))"))
                {
                    string temp = m.Groups[1].Value;
                    temp = Regex.Replace(temp, "<[^>]+>", "");
                    temp = Regex.Replace(temp, "&[^;]+;", "");
                    text.Add(temp);
                }
                string[] sText = (string[])text.ToArray(typeof(string));
                string savePath = @"D:\Web\Web_Homework\Chn\"+"ChnNews_Original_"+i.ToString()+".txt";
                //string savePath = @"D:\Web\Web_Homework\Eng\" + "EngNews_Original_" + i.ToString() + ".txt";
                File.WriteAllLines(savePath, sText, Encoding.UTF8);
            }
        }
    }
}
