using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using JiebaNet.Segmenter;

namespace Web_Homework
{
    class Chn_pre
    {
        static ArrayList list = GetStopWords();
        static ArrayList list2 = GetDelWords();
        static ArrayList GetStopWords()
        {
            ArrayList l = new ArrayList();
            StreamReader reader = new StreamReader("D:/Web/Web_Homework/Chn_Stop.txt");
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                l.Add(line);
            }
            return l;
        }
        static ArrayList GetDelWords()
        {
            ArrayList l = new ArrayList();
            StreamReader reader = new StreamReader("D:/Web/Web_Homework/DelChar.txt");
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                l.Add(line);
            }
            return l;
        }
        public static void ReadFile(string path)
        {
            string[] files = Directory.GetFiles(path, "*.txt");
            for (int i = 0; i < files.Length; i++)
            {
                StreamReader reader = new StreamReader(files[i]);
                string line;
                ArrayList text = new ArrayList();
                while ((line = reader.ReadLine()) != null)
                {
                    text.Add(line);
                }
                DelChar(ref text);
                ApartWords(ref text);
                string[] sText = (string[])text.ToArray(typeof(string));
                string result = System.Text.RegularExpressions.Regex.Replace(files[i], @"[^0-9]+", "");
                string savePath = @"D:\Web\Web_Homework\Chn_af\" + "ChnNews_After_" + result + ".txt";
                File.WriteAllLines(savePath, sText, Encoding.UTF8);
            }
        }
        #region//测试用
        public static void test()
        {
            StreamReader reader = new StreamReader("D:/Web/Web_Homework/Chn_test.txt");
            string line;
            ArrayList text = new ArrayList();
            while ((line = reader.ReadLine()) != null)
            {
                if (!string.IsNullOrWhiteSpace(line))
                    text.Add(line);
            }
            DelChar(ref text);
            ApartWords(ref text);
            string[] sText = (string[])text.ToArray(typeof(string));
            string savePath = @"D:\Web\Web_Homework\Chntest_After.txt";
            File.WriteAllLines(savePath, sText, Encoding.UTF8);
        }
        #endregion
        static void DelChar(ref ArrayList text)
        {
            for (int k = 0; k < text.Count; k++)
            {
                string s = text[k].ToString();
                foreach (string str in list2)
                {
                    if (s.Contains(str))
                        s = s.Replace(str, "");
                }
                int i = 0, j = 0;
                string t = "";
                while (i < s.Length)
                {
                    if (s[i] == '<')
                    {
                        j = i;
                        while (j < s.Length)
                        {
                            t += s[j];
                            if (s[j] == '>')
                            {
                                break;
                            }
                            j++;
                        }
                        if (s.Contains(t)) s = s.Replace(t, "");
                        i = 0;
                        t = "";
                    }
                    else
                    {
                        i++;
                    }
                }
                i = 0;j = 0;t = "";
                while (i < s.Length)
                {
                    if (s[i] == '【')
                    {
                        j = i;
                        while (j < s.Length)
                        {
                            t += s[j];
                            if (s[j] == '】')
                            {
                                break;
                            }
                            j++;
                        }
                        if (s.Contains(t)) s = s.Replace(t, "");
                        i = 0;
                        t = "";
                    }
                    else
                    {
                        i++;
                    }
                }
                text[k] = s;
            }
        }
        static void ApartWords(ref ArrayList text)
        {
            for (int i = 0; i < text.Count; i++)
            {
                string str = text[i].ToString().Trim(' ').Replace(" ", "");
                var segmenter = new JiebaSegmenter();
                var segments = segmenter.CutForSearch(str);
                string[] x=segments.ToArray();
                List<string> y = new List<string>();
                int flag = 1;
                for(int j=0;j<x.Length;j++)
                {
                    flag = 1;
                    foreach(var k in list)
                    {
                        if(x[j]==k.ToString())
                        {
                            flag = 0;
                            break;
                        }
                    }
                    if(flag==1)
                    {
                        y.Add(x[j]);
                    }
                }
                text[i] = string.Join(" ", y);
            }
        }
    }
}
