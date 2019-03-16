using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NEXUS.Models
{
    public class EmployeeModel
    {
        public List<store> ListStore { get; set; }
        public int Id { get; set; }

        public string PhoneNumber { get; set; }
        public string UserCode { get; set; }
        public string FullName { get; set; }
        public Nullable<decimal> Money { get; set; }
        public string Email { get; set; }
        public Nullable<int> Gender { get; set; }
        public Nullable<int> Birthday { get; set; }
        public string Address { get; set; }
        public int Role { get; set; }
    }
}