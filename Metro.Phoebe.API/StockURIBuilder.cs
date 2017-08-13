using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metro.Phoebe.API
{
    public interface StockURIBuilder
    {
        StockURIBuilder startDate(DateTime date);
        StockURIBuilder endDate(DateTime date);
        StockURIBuilder endPoint(String endPoint);
        StockURIBuilder symbal(String symbal);
        Uri build();
    }
}
