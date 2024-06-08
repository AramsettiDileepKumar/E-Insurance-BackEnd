using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.RequestDTO.PolicyModels
{
    public class CalculatePremiumRequest
    {
        public int PolicyId {  get; set; }
        public string? CoverageAmount { get; set; } 
        public int Tenure {  get; set; }
        public string? PremiumType {  get; set; }
    }
}
