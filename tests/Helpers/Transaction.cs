using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tests.Helpers
{
    public class Transaction
    {
        public string _id;
        public string userId;
        public string cost;
        public string title;
        public string description;
    }
}
