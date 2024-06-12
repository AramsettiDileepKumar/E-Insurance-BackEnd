using BusinessLogicLayer.Interfaces;
using ModelLayer.Entities;
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
        public async Task<int> AddPayment(PaymentRequest paymentRequest, int CustomerId)
        {
            return await paymentRepo.AddPayment(paymentRequest, CustomerId);
        }
        public async Task<IEnumerable<PaymentEntity>> getPayments(int CustomerId)
        {
            return await paymentRepo.getPayments(CustomerId);
        }
        public async Task<PaymentEntity> getReceipt(int PurchaseId, int CustomerId)
        {
            return await paymentRepo.getReceipt(PurchaseId,CustomerId);
        }
    }
}
