using BusinessLogicLayer.Interfaces;
using ModelLayer.RequestDTO.Paymentmodels;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class PaymentServiceBL:IPaymentBL
    {
        private readonly IPaymentRepo paymentRepo;
        public PaymentServiceBL(IPaymentRepo paymentRepo)
        {
            this.paymentRepo = paymentRepo;
        }
        public async Task<int> AddPayment(PaymentRequest paymentRequest)
        {
            return await paymentRepo.AddPayment(paymentRequest);
        }
    }
}
