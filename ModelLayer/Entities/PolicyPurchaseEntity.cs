using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Entities
{
    public class PolicyPurchaseEntity
    {
            public int PurchaseId { get; set; }
            public int PolicyId { get; set; }
            public string? PolicyType { get; set; }
            public int CustomerId { get; set; }
            public int AgentId { get; set; }
            public decimal CoverageAmount { get; set; }
            public int Tenure { get; set; }
            public string PremiumType { get; set; }
            public decimal PremiumAmount {  get; set; }
            public string Status { get; set; }
            public DateTime PurchaseDate { get; set; }
    }
}
