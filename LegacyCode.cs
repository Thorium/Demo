using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;

namespace ClassLibrary1
{
    public class LegacyCode
    {
        public static Uri MakeUrl(string symbol, DateTime dfrom, DateTime dto)
        {
            return new Uri("http://ichart.finance.yahoo.com/table.csv?s=" + symbol +
               "&e=" + dto.Day.ToString() + "&d=" + dto.Month.ToString() + "&f=" + dto.Year.ToString() +
               "&g=d&b=" + dfrom.Day.ToString() + "&a=" + dfrom.Month.ToString() + "&c=" + dfrom.Year.ToString() +
               "&ignore=.csv");
        }

        private string Fetch(Uri url)
        {
            var req = WebRequest.Create (url);
            using(var stream = req.GetResponse().GetResponseStream()){
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        private List<List<string>> Decompose(string data)
        {
            char mark = '\n';
            char mark2 = ',';
            
            List<List<string>> result = new List<List<string>>();

            foreach (string datedata in data.Split(mark))
            {
                if(datedata!=""){
                    List<string> tempList = new List<string>();
                    foreach (string item in datedata.Split(mark2))
                    {
                        tempList.Add(item);
                    }
                    result.Add(tempList);
                }
            }

            return result;
        }

        private List<Tuple<DateTime, double>> ReFormat(List<List<string>> list)
        {
            
            list.RemoveAt(0);
            
            List<Tuple<DateTime, double>> result = new List<Tuple<DateTime,double>>();
 
            foreach(var sublist in list){
                
                string date = sublist[0];
                string rate = sublist[4];

                DateTime parseDate = DateTime.ParseExact(date, "yyyy-mm-dd", CultureInfo.InvariantCulture);
                double parseRate = Double.Parse(rate, CultureInfo.GetCultureInfo("en-US"));
                
                result.Add(new Tuple<DateTime, double>(parseDate, parseRate));
                
            }

            return result;
        }

        public List<Tuple<DateTime, double>> GetResult(Uri url)
        {
            var res1 = Fetch(url);
            var res2 = Decompose(res1);
            var res3 = ReFormat(res2);

            return res3;
        }

        public Tuple<DateTime, double> DateMaxClose(IEnumerable<Tuple<DateTime, double>> mylist)
        {
            double max = -1;
            foreach(var datedata in mylist){
                if (datedata.Item2>max)
                        max=datedata.Item2;
            }

            foreach(var datedata in mylist){
                if(datedata.Item2==max)
                    return datedata;
            }
            return null;
        }
    }
}