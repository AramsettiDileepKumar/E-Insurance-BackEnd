using ModelLayer.RequestDTO.Paymentmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IPaymentBL
    {
        Task<int> AddPayment(PaymentRequest paymentRequest);
    }
}
