using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NEXUS.Models
{
    public class StoreModel
    {
        public int StoreId { get; set; }
        public string Address { get; set; }
        public Nullable<int> Status { get; set; }
        public string Name { get; set; }
        public List<UserModel> ListUser { get; set; }
        
    }
}