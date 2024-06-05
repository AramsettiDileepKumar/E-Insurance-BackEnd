using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.RequestDTO.PolicyModels
{
    public class purchaseRequest
    {
        public int CustomerId { get; set; }
        public int PolicyId { get; set; }
        public int AgentId { get; set; }
    }
}
