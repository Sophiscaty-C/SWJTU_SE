using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_Homework
{
    class Program
    {
        public static void Main(string[] args)
        {
            //string URL = "http://www.chinadaily.com.cn/world/china-africa/page_";
            //string URL = "http://china.chinadaily.com.cn/5bd5639ca3101a87ca8ff636/page_";
            //WebDL.WebDownLoad(URL);
            //TextDL.ReadWeb(TextDL.ReadXML());
            string path1 = "D:/Web/Web_Homework/Eng";
            string path2 = "D:/Web/Web_Homework/Chn";
            Eng_pre.ReadFile(path1);
            Chn_pre.ReadFile(path2);
            //Eng_pre.test();
            //Chn_pre.test();
        }
    }
}
