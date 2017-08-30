using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metro.Phoebe.Api.Impl
{
    public class GoogleStockURIBuilder : IStockURIBuilder
    {

    private DateTime _startDate;
    private DateTime _endDate;
    private string _endPoint = "historical";
    private string _symbal;
    private readonly string GOOGLE_STOCK_DAILY_URI = "http://www.google.com/finance/{0}?q={1}&histperiod=daily&startdate={2}&enddate={3}&output=csv";

    public IStockURIBuilder StartDate(DateTime date)
    {
        this._startDate = date;
        return this;
    }

    public IStockURIBuilder EndDate(DateTime date)
    {
        this._endDate = date;
        return this;
    }

    public IStockURIBuilder EndPoint(String endPoint)
    {
        this._endPoint = endPoint;
        return this;
    }

    public IStockURIBuilder Symbal(String symbal)
    {
        this._symbal = symbal;
        return this;
    }

    public Uri Build()
    {
        return new Uri(string.Format(GOOGLE_STOCK_DAILY_URI, _endPoint, _symbal, _startDate.ToString("MM/dd/yyyy"), _endDate.ToString("MM/dd/yyyy")));
    }
}

}
