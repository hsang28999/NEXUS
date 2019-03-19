using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NEXUS.Models
{
    public class FeedbackModel
    {
        public int FeedbackId { get; set; }
        public string Note { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public int Status { get; set; }
    }
}