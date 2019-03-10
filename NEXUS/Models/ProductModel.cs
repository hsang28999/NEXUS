using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NEXUS.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Type { get; set; }
        public int TimeType { get; set; }
        public int TimeUsed { get; set; }
        public Nullable<decimal> PpmLocal { get; set; }
        public Nullable<decimal> PpmStd { get; set; }
        public Nullable<decimal> PpmMobile { get; set; }
        public int Status { get; set; }
        public int ConnectionId { get; set; }
        public int ConnectionGroupId { get; set; }
    }
}