using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;

namespace ClassLibrary1
{
    public class NewCode
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
            var req = WebRequest.Create(url);
            using (var stream = req.GetResponse().GetResponseStream())
            {
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        private IEnumerable<IEnumerable<string>> Decompose(string data)
        {
            char mark = '\n';
            char mark2 = ',';

            var result = from datedata in data.Split(mark)
                         where !string.IsNullOrEmpty(datedata)
                         select datedata.Split(mark2);

            return result;
        }

        private IEnumerable<Tuple<DateTime, double>> ReFormat(IEnumerable<IEnumerable<string>> list)
        {

            Func<string, DateTime> parseDate = 
                dt => DateTime.ParseExact(dt, "yyyy-mm-dd", CultureInfo.InvariantCulture);

            Func<string, double> parseRate = rt => Double.Parse(rt, CultureInfo.GetCultureInfo("en-US"));

            var result = from sublist in list.Skip(1)
                         select new Tuple<DateTime, double>
                         (parseDate(sublist.ElementAt(0)), parseRate(sublist.ElementAt(4)));
            
            return result;
        }

        public IEnumerable<Tuple<DateTime, double>> GetResult(Uri url)
        {
            var res1 = Fetch(url);
            var res2 = Decompose(res1);
            var res3 = ReFormat(res2);

            return res3;
        }

        public Tuple<DateTime, double> DateMaxClose(IEnumerable<Tuple<DateTime, double>> mylist)
        {
            return mylist.Aggregate(
                (sum, item) => (item.Item2 > sum.Item2) ? item : sum
                );
        }
    }
}