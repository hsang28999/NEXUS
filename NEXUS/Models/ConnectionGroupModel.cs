using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NEXUS.Models
{
    public class ConnectionGroupModel
    {
        public List<ProductModel> Products { get; set; }
        public Nullable<int> Bandwidth { get; set; }
        public string Name { get; set; }
    }
}