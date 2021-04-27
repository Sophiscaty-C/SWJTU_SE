using System;//下载网页
using System.Text;
using System.Xml;
using System.Net;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;

namespace Web_Homework
{
   class WebDL
    {
        public static void WebDownLoad(string URL)
        {
            ArrayList a = new ArrayList();
            for (int i = 1; i <= 100; i++)//for (int i = 1; i <= 40; i++)
            {
                string page = GetPageSource(URL + i.ToString() + ".html");
                ArrayList links = GetLinks(page);
                foreach (string s in links)
                {
                    a.Add(s);
                }
            }
            a.Sort();
            ArrayList b = new ArrayList();
            for (int i = 0; i < a.Count - 1; i++)
            {
                bool flag = true;
                for (int j = i + 1; j < a.Count; j++)
                {
                    if (string.Compare(a[i].ToString(), a[j].ToString()) == 0)
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag == true)
                {
                    b.Add(a[i]);
                }
            }
            WriteToXml(URL, b);
        }

        static string GetPageSource(string URL)
        {
            Uri uri = new Uri(URL);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            request.Method = "Get";
            request.KeepAlive = false;
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            return reader.ReadToEnd();
        }
        static ArrayList GetLinks(string page)
        {
            ArrayList list = new ArrayList();
            string regex = @"([\w-]+\.)+[\w]+[/a]+(/[\w- ./?%&=]*)*.html";
            Regex r = new Regex(regex, RegexOptions.IgnoreCase);
            MatchCollection m = r.Matches(page);
            for (int i = 0; i < m.Count; i++)
            {
                string str = m[i].ToString();
                list.Add(str);
            }
            return list;
        }
        static void WriteToXml(string URL, ArrayList Links)
        {
            XmlTextWriter writer = new XmlTextWriter("HyperLinks1.xml", Encoding.UTF8);
            //XmlTextWriter writer = new XmlTextWriter("HyperLinks2.xml", Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartDocument(false);
            writer.WriteComment(URL);
            writer.WriteStartElement("HyperLinks");
            //writer.WriteStartElement("HyperLinks", null);
            writer.WriteAttributeString("DateTime", DateTime.Now.ToString());
            foreach (string str in Links)
            {
                string title = GetDomain(str);
                string body = str;
                writer.WriteElementString(title, null, body);
            }
            writer.WriteEndElement();
            //writer.WriteEndElement();
            writer.Flush();
            writer.Close();
        }
        static string GetDomain(string URL)
        {
            string ret;
            string regex = @"(\.com/|\.net/|\.cn/|\.org/|\.gov/)";
            Regex r = new Regex(regex, RegexOptions.IgnoreCase);
            Match m = r.Match(URL);
            ret = m.ToString();
            regex = @"\.|/$";
            ret = Regex.Replace(ret, regex, "").ToString();
            if (ret == "")
                ret = "other";
            return ret;
        }
    }
}