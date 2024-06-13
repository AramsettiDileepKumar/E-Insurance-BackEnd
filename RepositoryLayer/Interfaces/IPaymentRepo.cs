using ModelLayer.Entities;
using ModelLayer.RequestDTO.Paymentmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IPaymentRepo
    {
        Task<int> AddPayment(PaymentRequest paymentRequest, int CustomerId);
        Task<IEnumerable<PaymentEntity>> getPayments(int CustomerId);
        Task<PaymentEntity> getReceipt(int PaymentId, int CustomerId);
    }
}
