using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Entities
{
    public class PaymentEntity
    {
            public int PaymentId { get; set; }
            public DateTime PaymentDate { get; set; }
            public string Status { get; set; }
            public decimal PremiumAmount { get; set; }       
            public string PaymentMethod { get; set; }
            public int PolicyId { get; set; }
    }
}
