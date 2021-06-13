using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_Homework
{
    class Cos_dis
    {
        public static void Calculate(string path)
        {
            ArrayList text = new ArrayList();
            string[] files = Directory.GetFiles(path, "*.txt");
            string t="";
            for(int i=0;i<499;i++)
            {
                for(int j=i+1;j<500;j++)
                {
                    t = i.ToString() + " and " + j.ToString() + " CosDistance: " + SimilarityCos(files[i], files[j]).ToString();
                    text.Add(t);
                    Console.WriteLine(t);
                }  
            }
            string[] sText = (string[])text.ToArray(typeof(string));
            string savePath = @"D:\Web\Web_Homework\CosDistance2.txt";
            File.WriteAllLines(savePath, sText, Encoding.UTF8);
        }
        static double SimilarityCos(string str1, string str2)
        {
            List<string> lstr1 = SimpParticiple(str1);
            List<string> lstr2 = SimpParticiple(str2);
            var strUnion = lstr1.Union(lstr2);
            List<int> int1 = new List<int>();
            List<int> int2 = new List<int>();
            foreach (var item in strUnion)
            {
                int1.Add(lstr1.Count(o => o == item));
                int2.Add(lstr2.Count(o => o == item));
            }

            double s = 0;
            double den1 = 0;
            double den2 = 0;
            for (int i = 0; i < int1.Count(); i++)
            {
                s += int1[i] * int2[i];
                den1 += Math.Pow(int1[i], 2);
                den2 += Math.Pow(int2[i], 2);
            }
            return s / (Math.Sqrt(den1) * Math.Sqrt(den2));
        }
        public static List<string> SimpParticiple(string file)
        {
            StreamReader reader = new StreamReader(file);
            string line;
            string text = "";
            while ((line = reader.ReadLine()) != null)
            {
                line = line.Trim();
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }
                text += line;
            }
            string[] str = text.Split(' ');
            List<string> vs = new List<string>();
            foreach (var item in str)
            {
                vs.Add(item.ToString());
            }
            return vs;
        }
    }
}
