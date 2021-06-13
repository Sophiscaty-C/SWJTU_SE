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
            //Eng_pre.ReadFile(path1);
            //Chn_pre.ReadFile(path2);
            //Eng_pre.test();
            //Chn_pre.test();

            //Cos_dis.Calculate(path1);

            double[,] test = KMeans_pre.pre();
            KMeans k = new KMeans(test);
            int[,] result = k.GetProcess();
            for (int i = 0; i < 20; i++)
            {
                Console.Write(i);
                Console.Write(": ");
                for (int j = 0; j < result.GetLength(1); j++)
                {
                    if (result[i, j] != 0)
                    {
                        Console.Write(result[i, j]);
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
            int len = 500;
            var result3 = from pair in k.f orderby pair.Value ascending select pair;
            foreach (var p in result3)
            {
                int q = 0;
                for(q=0;q<len;q++)
                {
                    if(p.Key==result[12, q])
                    {
                        break;
                    }
                }
                if(q!=len)
                {
                    Console.Write(p.Key);
                    Console.Write(" ");
                    Console.Write(p.Value);
                    Console.WriteLine();
                }
            }
        }
    }
}
