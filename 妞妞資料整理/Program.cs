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
           StreamReader sr = new StreamReader("1月代購登記簿.csv" , Encoding.GetEncoding("big5"));
            
           while (!sr.EndOfStream)
           {
              string dataList = sr.ReadLine();
              RawData.Add(dataList.Split(',').ToList());
           }

           for(int i=3; i<RawData.Count; i++)
           {
                for(int j=4; i<RawData[i].Count; j++)
                {
                    int BuyQuantitiy = 0;
                    string OrderProduct = "";
                    if(RawData[i][j].CompareTo("")!=0)
                    {
                        try
                        {
                            BuyQuantitiy = int.Parse(RawData[i][j]);
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }

                    OrderProduct += RawData[i][0] + " *";

                }
           }

           sr.Close();
        }
    }
}
