using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Entities
{
    public class CommissionDetails
    {
        public int CommissionId { get; set; }
        public int PolicyPurchaseId { get; set; }
        public int AgentId { get; set; }
        public decimal CommissionAmount { get; set; }
        public DateTime CalculationDate { get; set; }
    }
}
