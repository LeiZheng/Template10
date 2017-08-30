using Metro.Phoebe.Api;
using Metro.Phoebe.Shares.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace Metro.Phoebe.Shares.Services
{
    public class StockServices
    {
        public IStockURIBuilder uriBuilder;
        public StockServices(IStockURIBuilder builder)
        {
            this.uriBuilder = builder;
        }

        public async Task<List<StockBarData>> GetStockHistoryBars(String symbal, DateTime startDate, DateTime endDate)
        {
            Uri uri = uriBuilder.StartDate(startDate).EndDate(endDate).Symbal(symbal).EndPoint(StockURIBuilderType.HISTORICAL).Build();
            List<StockBarData> stockBars = new List<StockBarData>();
            await PorcessDataByUri(uri, (lineStr, rowIndex) => {
                if (0 == rowIndex)
                {

                }
                else
                {
                    String[] vals = lineStr.Split(new char[] { ',' });
                    StockBarData barData = new StockBarData(ParseDate(vals[0])
                            , ParseDouble(vals[1])
                            , ParseDouble(vals[2])
                            , ParseDouble(vals[3])
                            , ParseDouble(vals[4])
                            , ParseLong(vals[5])
                            );
                    stockBars.Add(barData);
                }

            });
            return stockBars;
        }

        private double ParseDouble(String val)
        {
            double ret = 0;
            try
            {
                ret = Double.Parse(val);
            }
            catch (Exception e)
            {

            }
            return ret;
        }

        private DateTime ParseDate(String val)
        {
            DateTime ret = DateTime.MinValue;
            try
            {
                ret = DateTime.Parse(val);
            }
            catch (Exception e)
            {

            }
            return ret;
        }

        private long ParseLong(String val)
        {
            long ret = 0;
            try
            {
                ret = long.Parse(val);
            }
            catch (Exception e)
            {

            }
            return ret;
        }

        private async Task PorcessDataByUri(Uri myURL, Action<string, int> parser)
        {
            try { 
                var httpClient = new HttpClient();
                var resp = await httpClient.GetAsync(myURL);
                resp.EnsureSuccessStatusCode();
                var stream = await resp.Content.ReadAsInputStreamAsync();
                using (var reader = new StreamReader(stream.AsStreamForRead()))
                {
                    string line = null;
                    int count = 0;
                    while (null != (line = reader.ReadLine()))
                    {
                        parser.Invoke(line, count++);
                    }
                }
            }
            catch (Exception e)
            {
                Debugger.Break();
            }

        }
    }

}
