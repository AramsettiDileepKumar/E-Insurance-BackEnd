using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Entities
{
    public class PremiumRates
    {
            public string PolicyType { get; set; }
            public string AgeGroup { get; set; }
            public decimal Rate { get; set; }
    }
}
