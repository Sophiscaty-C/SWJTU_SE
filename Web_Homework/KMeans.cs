using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_Homework
{
    class KMeans_pre
    {
        public static double[,] pre()
        {
            string[] files = Directory.GetFiles("D:/Web/Web_Homework/Eng_af", "*.txt");
            ArrayList text = new ArrayList();
            Dictionary<int, double> idf = new Dictionary<int, double>();
            List<List<string>> str=new List<List<string>>();
            List<string> t = new List<string>();
            int q = 0;
            foreach (string file in files)
            {
                t = Cos_dis.SimpParticiple(file);
                str.Add(t);
                foreach(string temp in t)
                {
                    if(!text.Contains(temp))
                    {
                        text.Add(temp);
                    }
                }
                q++;
                if(q==500)
                {
                    break;
                }
            }
            int x = 0;
            int p = 0;
            foreach(string i in text)
            {
                x = 0;
                foreach(var j in str)
                {
                    if(j.Contains(i))
                    {
                        x++;
                    }
                }
                idf.Add(p, Math.Log10(500.0 / (double)(x + 1)));
                p++;
            }
            int k;
            double[,] data=new double[500, text.Count];
            for(int i=0;i<500;i++)
            {
                for(int j=0;j<str[i].Count;j++)
                {
                    k = text.IndexOf(str[i][j]);
                    data[i, k] =idf[k]* (double)str[i].Count(o => o == str[i][j])/ (double)str[i].Count;
                }
            }
            return data;
        }
    }
    class KMeans
    {
        double[,] inPut;//数据
        int k;//类别数
        int Num;//文件数
        int sub;//特征值数
        int[] groupNum;//各组数目
        public Dictionary<int, double> f = new Dictionary<int, double>();
        public KMeans(double[,] input)
        {
            inPut = input;
            Num = input.GetLength(0);
            sub = input.GetLength(1);
            k = 20;
            groupNum = new int[k];
        }

        public int[,] GetProcess()
        {
            double[,] tmpCenter = new double[k, sub];
            for (int i = 0; i < k; i++)
                for (int j = 0; j < sub; j++)
                    tmpCenter[i, j] = inPut[i, j];
            double[,] preCenter = new double[k, sub];
            int[,] resultP;
            while (true)
            {
                resultP = new int[k, Num];
                for (int i = 0; i < k; i++)
                {
                    groupNum[i] = 0;
                }

                #region //根据点到质心的距离，将点放到不同的组中

                for (int i = 0; i < Num; i++)
                {
                    double tmpDis = 0.0;
                    int index = 0;
                    for (int j = 0; j < k; j++)
                    {
                        double tmpIn = 0.0;
                        for (int m = 0; m < sub; m++)
                        {
                            tmpIn += Math.Pow((inPut[i, m] - tmpCenter[j, m]), 2);
                        }
                        if (j == 0)
                        {
                            tmpDis = tmpIn;
                            index = 0;
                        }
                        else
                        {
                            if (tmpDis > tmpIn)
                            {
                                tmpDis = tmpIn;
                                index = j;
                            }
                        }
                    }
                    if (index == 12)
                    {
                        if (f.ContainsKey(i+1))
                        {
                            if (tmpDis < f[i+1])
                            {
                                f[i+1] = tmpDis;
                            }
                        }
                        else
                        {
                            f.Add(i+1, tmpDis);
                        }
                    }
                    int groupKnum = groupNum[index];
                    resultP[index, groupKnum] = i + 1;
                    groupNum[index]++;
                }
                #endregion

                #region //保存质心
                for (int i = 0; i < k; i++)
                    for (int j = 0; j < sub; j++)
                        preCenter[i, j] = tmpCenter[i, j];
                #endregion

                #region //确定新质心
                for (int i = 0; i < k; i++)
                {
                    int kNum = groupNum[i];
                    if (kNum > 0)
                    {
                        for (int j = 0; j < sub; j++)
                        {
                            double tmp = 0.0;
                            for (int m = 0; m < kNum; m++)
                            {
                                int groupIndex = resultP[i, m] - 1;
                                tmp += inPut[groupIndex, j];
                            }
                            tmpCenter[i, j] = tmp / kNum;
                        }
                    }
                }
                #endregion

                #region //判断质心是否变化
                bool judge = true;
                for (int i = 0; i < k; i++)
                {
                    for (int j = 0; j < sub; j++)
                    {
                        judge = judge && (preCenter[i, j] == tmpCenter[i, j]);
                    }
                }
                if (judge)
                {
                    break;
                }
                #endregion
            }
            return resultP;
        }
    }
}
