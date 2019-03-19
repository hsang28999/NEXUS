using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NEXUS.Models
{
    public class ContractModel
    {
        public string CustomerName { get; set; }
        public string StoreName { get; set; }
        public string EmployeeName { get; set; }
        public int ContractId { get; set; }
        public int Status { get; set; }
        public string Note { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int EmployeeId { get; set; }
        public int StoreId { get; set; }
        public int SigningDate { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int StartDate { get; set; }
        public int EndDate { get; set; }
        public int ProductType { get; set; }
        public int ProductTimeUsed { get; set; }
        public Nullable<decimal> PpmLocal { get; set; }
        public Nullable<decimal> PpmStd { get; set; }
        public Nullable<decimal> PpmMobile { get; set; }
        public string Address { get; set; }
        public decimal SecurityDeposit { get; set; }
        public int TimeUsedAvailable { get; set; }
        
    }
}