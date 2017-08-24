using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metro.Kids.Models
{
    public class Record
    {
        public DateTime StartTime { get; set; }
        public TimeSpan Duration { get; set; }
        public long RecordId { get; set; }
        public int ErrorCount { get; set; }
    }
    public class SignleRecord : Record
    {
        
    }

    public class SessionRecord : Record
    {
        public ObjectId Id { get; set; }
        public string CorrectRate { get; set; }
        public int CollectionCount { get; set; }
        
    }
}