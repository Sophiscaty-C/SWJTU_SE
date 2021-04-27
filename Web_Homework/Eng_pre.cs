using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Web_Homework
{
    class Eng_pre
    {
        static ArrayList list = GetStopWords();
        static ArrayList list2 = GetDelWords();
        static ArrayList GetStopWords()
        {
            ArrayList l = new ArrayList();
            StreamReader reader = new StreamReader("D:/Web/Web_Homework/Eng_Stop.txt");
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
            int i = 0;
            string[] files = Directory.GetFiles(path, "*.txt");
            foreach (string file in files)
            {
                StreamReader reader = new StreamReader(file);
                string line;
                ArrayList text=new ArrayList();
                while ((line = reader.ReadLine()) != null)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                        text.Add(line);
                }
                DelChar(ref text);
                ApartWords(ref text);
                Stemming(ref text);
                string[] sText = (string[])text.ToArray(typeof(string));
                string savePath = @"D:\Web\Web_Homework\Eng_af\" + "EngNews_After_" + i.ToString() + ".txt";
                File.WriteAllLines(savePath, sText, Encoding.UTF8);
                i++;
            }
        }
        #region//测试用
        public static void test()
        {
            StreamReader reader = new StreamReader("D:/Web/Web_Homework/Eng_test.txt");
            string line;
            ArrayList text = new ArrayList();
            while ((line = reader.ReadLine()) != null)
            {
                if (!string.IsNullOrWhiteSpace(line))
                    text.Add(line);
            }
            DelChar(ref text);
            ApartWords(ref text);
            Stemming(ref text);
            string[] sText = (string[])text.ToArray(typeof(string));
            string savePath = @"D:\Web\Web_Homework\Engtest_After.txt";
            File.WriteAllLines(savePath, sText, Encoding.UTF8);
        }
        #endregion
        static void DelChar(ref ArrayList text)
        {
            for(int k=0;k<text.Count;k++)
            {
                string s = text[k].ToString();
                foreach(string str in list2)
                {
                    if (s.Contains(str))
                        s = s.Replace(str, " ");
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
                        if (s.Contains(t)) s = s.Replace(t, " ");
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
            int x = 0;
            for(int i=0;i<text.Count;i++)
            {
                string[] sp = Regex.Split(text[i].ToString(), "\\s+", RegexOptions.IgnoreCase);
                text[i] = "";
                for(int j=0;j<sp.Length;j++)
                {
                    if(sp[j]=="")
                    {
                        continue;
                    }
                    sp[j]=sp[j].ToLower();
                    x = 0;
                    foreach (string s in list)
                    {
                        if (sp[j] == s)
                        {
                            x = 1;
                            break;
                        }
                    }
                    if(x==0)
                    {
                        text[i] += sp[j] + " ";
                    }
                }
            }
        }
        static void Stemming(ref ArrayList text)
        {
			for(int i=0;i<text.Count;i++)
            {
				string[] sp = text[i].ToString().Split(" ");
				text[i] = "";
				for (int j = 0; j < sp.Length; j++)
                {
                    Stemming st=new Stemming();
                    sp[j]=st.StemWord(sp[j]);
                    text[i] += sp[j] + " ";
				}
			}
        }
	}
}
