using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ClassLibrary1;
using System.Timers;

namespace TestProject3
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void TestMethodStocks()
        {
            var url = Stocks.MakeUrl("MSFT",
                        new DateTime(2000, 9, 15),
                        new DateTime(2000, 10, 18)
                      );
            var res = Stocks.GetResult(url);

            var closerate = Stocks.DateMaxClose(res);
            Assert.IsTrue(closerate.Item2 > 20);

        }

        [TestMethod]
        public void TestMethodNewCode()
        {
            var l = new NewCode();
            var url = NewCode.MakeUrl("MSFT",
                        new DateTime(2000, 9, 15),
                        new DateTime(2000, 10, 18)
                      );
            var res = l.GetResult(url);

            var closerate = l.DateMaxClose(res);
            Assert.IsTrue(closerate.Item2 > 20);

        }

        [TestMethod]
        public void TestMethodLegacyCode()
        {
            var l = new LegacyCode();
            var url = LegacyCode.MakeUrl("MSFT", 
                        new DateTime(2000,9,15),
                        new DateTime(2000,10,18)
                      );
            var res = l.GetResult(url);

            var closerate = l.DateMaxClose(res);
            Assert.IsTrue(closerate.Item2 > 20);
            
        }
                
    }
}
