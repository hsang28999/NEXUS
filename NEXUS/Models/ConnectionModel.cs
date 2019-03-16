using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NEXUS.Models
{
    public class ConnectionModel
    {
        public decimal SecurityDeposit { get; set; }
        public string Name { get; set; }
        public List<ConnectionGroupModel> ConnectionGroups { get; set; }
    }
}