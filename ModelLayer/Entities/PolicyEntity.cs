using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Entities
{
    public class PolicyEntity
    {
        public int PolicyId { get; set; }
        public string PolicyNumber { get; set; }
        public string PolicyName { get; set; }
        public string PolicyDescription { get; set; }
        public string PolicyType { get; set; }
        public string? ClaimSettlementRatio { get; set; }
        public int EntryAge { get; set; }
        public double AnnualPremiumRange { get; set; }
    }
}
