using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metro.Kids.Models;
using LiteDB;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.Storage;

namespace Metro.Kids.Services
{
    public class StorageServices
    {
        public void Insert(SessionRecord sessionRec)
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                // Get customer collection
                var customers = db.GetCollection<SessionRecord>("sessions");
                customers.Insert(sessionRec);
            }
        }

        public ObservableCollection<SessionRecord> GetAllSessionRecords()
        {
            using (var db = new LiteDatabase(ApplicationData.Current.LocalFolder.Path +   "\\MyData.db"))
            {
                // Get customer collection
                var customers = db.GetCollection<SessionRecord>("sessions");
                return new ObservableCollection<SessionRecord>(customers.FindAll());
            }
        }
    }
}
