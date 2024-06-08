using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.RequestDTO.PolicyModels
{
    public class PremiumRequest
    {
        public int PolicyId { get; set; }
        public decimal PremiumAmoumt { get; set; }
    }
}
