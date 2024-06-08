using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.RequestDTO.Paymentmodels
{
    public class PaymentRequest
    {
        public string PaymentMethod { get; set; }
        public int PolicyId { get; set; }
    }
}
