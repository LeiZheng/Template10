using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metro.Kids.Models
{
    public class Record
    {
        public long RecordId;
        public DateTime StartTime;
        public DateTime EndTime;
        public int ErrorCount;
    }
    public class SignleRecord : Record
    {

    }

    public class CollectionRecord : Record
    {
        public int CollectionCount;
    }
}