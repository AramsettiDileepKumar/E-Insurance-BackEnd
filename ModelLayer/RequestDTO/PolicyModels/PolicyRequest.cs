using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.RequestDTO.PolicyModels
{
    public class PolicyRequest
    {
        public string? PolicyNumber { get; set; }
        public string? PolicyName { get; set; }
        public string? PolicyDescription { get; set; }
        public string? PolicyType { get; set; }
        public string? ClaimSettlementRatio { get; set; }
        public int EntryAge { get; set; }
        public double AnnualPremiumRange { get; set; }
       
    }
}
