using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.RequestDTO.PolicyModels
{
    public class PolicyRequest
    {
        public string PolicyNumber { get; set; }
        public string PolicyName { get; set; }
        public string PolicyDescription { get; set; }
        public string PolicyType { get; set; }
        public double CoverageAmount { get; set; }
        public int EntryAge { get; set; }
        public double AnnualPremiumRange { get; set; }
        public int TermLength { get; set; }
    }
}
