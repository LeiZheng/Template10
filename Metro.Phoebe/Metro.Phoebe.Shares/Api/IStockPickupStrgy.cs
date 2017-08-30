using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metro.Phoebe.Api
{
    public interface IStockPickupStrgy
    {
        bool Test();
    }
    public interface IStockURIBuilder
    {
        IStockURIBuilder StartDate(DateTime date);
        IStockURIBuilder EndDate(DateTime date);
        IStockURIBuilder EndPoint(String endPoint);
        IStockURIBuilder Symbal(String symbal);
        Uri Build();
    }

}
