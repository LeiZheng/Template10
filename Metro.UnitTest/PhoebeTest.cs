
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Metro.Phoebe.Shares.Services;
using Metro.Phoebe.Api.Impl;

namespace Metro.UnitTest
{
    [TestClass]
    public class StockServicesTest
    {
        private StockServices services = new StockServices(new GoogleStockURIBuilder());
        [TestMethod]
        public void TestGetStockHistoryBars()
        {
            var task = services.GetStockHistoryBars("AAPL", DateTime.Now.AddYears(-10), DateTime.Now);
            task.Wait();
            var ret = task.Result;
            Assert.IsTrue(ret.Count > 0);

        }
    }


}
