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
           List<string> CustomerData = new List<string>();  
           StreamReader sr = new StreamReader("代購登記簿.txt" );
           StreamReader sr2 = new StreamReader("CUSTOMER.txt");
           StreamWriter sw = new StreamWriter("result.csv" ,false, Encoding.GetEncoding("utf-8"));
           StringBuilder OutputString = new StringBuilder();

            while (!sr.EndOfStream)
            {
                string dataList = sr.ReadLine();
                RawData.Add(dataList.Split('\t').ToList());
            }

            while (!sr2.EndOfStream)
            {
                string customerData = sr2.ReadLine();
                CustomerData.Add(customerData);
            }


           string Source = "";
           for (int i=3; i<RawData.Count; i++)
           {
                string OrderProduct = "";
                string newLine = "\n";

                string BeforeOrderProduct = "哈囉哈囉\n您訂購的商品是:\n";
                OrderProduct += "\"" + BeforeOrderProduct;
                #region Product
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
                        OrderProduct += "  "+ RawData[0][j] +" "+ RawData[1][j] + "*" + BuyQuantitiy + newLine ;
                    }

                   

                }

                OrderProduct += "總金額: " + RawData[i][3];

                try
                {
                    int FreeShipping = int.Parse(RawData[i][3]);
                    if(FreeShipping >= 2500)
                        OrderProduct += "(有達免運優惠)";
                }
                catch(Exception e)
                { }
                #endregion
                #region CustomerData

                

                if (RawData[i][0].CompareTo("") != 0)
                    Source = RawData[i][0].Trim();

                for(int j=1; j<CustomerData.Count; j++)
                {

                    if (RawData[i][2].Trim().CompareTo("")!=0 && CustomerData[j].Trim().Contains(RawData[i][2].Trim()) && CustomerData[j].Trim().Contains(Source))
                    {
                        try
                        {
                            int FreeShipping = int.Parse(RawData[i][3]);
                            if (CustomerData[j].Contains("FAMILY MART") && FreeShipping < 2500)
                            {
                                OrderProduct += "+60(全家店到店) = " + (FreeShipping + 60).ToString();
                            }
                            else if (CustomerData[j].Contains("7-11") && FreeShipping < 2500)
                            {
                                OrderProduct += "+60(7-11店到店) = " + (FreeShipping + 60).ToString();
                            }
                            else if (CustomerData[j].Contains("POST"))
                            {

                            }
                        }
                        catch (Exception e)
                        { }


                        string RemoveCharacter = CustomerData[j].Replace("\"", "");

                        string[] customerdatacell = RemoveCharacter.Split(new char[] { '\t' });

                       

                        string _customerData = "";

                        for(int k=2; k<customerdatacell.Length; k++)
                        {
                            _customerData += customerdatacell[k] +"  ";

                            if (k == 3) _customerData += "\n  ";
                            if (k == 6) break;
                        }
                        OrderProduct += "\n\n寄件資料:\n  " + _customerData + "\n\n";
                        OrderProduct += "麻煩幫我核對以上資料喔，如有錯誤或需更新的資料請私訊告知\n確認無誤後麻煩匯款到郵局(700) 01210990850824 / 蘆竹郵局 林映辰\n可以轉帳或無摺\n匯款後請告知後5碼以供對帳(無摺請拍收據)\n\n希望您收到商品會喜歡，謝謝你♡";

                       
                    }
                }


                #endregion


                OrderProduct += "\"";

                string ResultWriteLine = RawData[i][0] + "," + RawData[i][2] + "," + OrderProduct +"\n";
                OutputString.Append(ResultWriteLine);


               
           }

           sw.Write(OutputString);
           sr.Close();
           sr2.Close();
           sw.Close();
        }
    }
}
