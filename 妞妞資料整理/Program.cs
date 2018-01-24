using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 妞妞資料整理
{
    class Program
    {
        static void Main(string[] args)
        {
           List<List<string>> RawData = new List<List<string>>();
           StreamReader sr = new StreamReader("代購登記簿.txt" );
           StreamWriter sw = new StreamWriter("result.csv" ,false, Encoding.GetEncoding("utf-8"));
            
           while (!sr.EndOfStream)
           {
              string dataList = sr.ReadLine();
              RawData.Add(dataList.Split('\t').ToList());
           }

           for(int i=3; i<RawData.Count; i++)
           {
                string OrderProduct = "";
                for (int j=6; j<RawData[i].Count; j++)
                {
                    int BuyQuantitiy = 0;                   
                    if(RawData[i][j].CompareTo("")!=0)
                    {
                        try
                        {
                            BuyQuantitiy = int.Parse(RawData[i][j]);
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine(RawData[i][j]);
                            Console.WriteLine(e.Message);
                        }
                    }

                    if (BuyQuantitiy != 0)
                    {
                        OrderProduct += RawData[0][j] +" "+ RawData[1][j] + "*" + BuyQuantitiy + "/r/n";
                    }

                }

                string ResultWriteLine = RawData[i][2] +"," + OrderProduct;
                sw.WriteLine(ResultWriteLine);
           }

           sr.Close();
           sw.Close();
        }
    }
}
