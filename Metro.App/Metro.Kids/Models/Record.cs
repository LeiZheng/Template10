using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metro.Kids.Models
{
    public class Record
    {
        public long RecordId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int ErrorCount { get; set; }
    }
    public class SignleRecord : Record
    {

    }

    public class SessionRecord : Record
    {
        public int CollectionCount { get; set; }
    }
}