using ModelLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface ICommissionRepo
    {
        Task<int> AddCommissionRate(CommissionRates commissionRate);
        Task<bool> CalculateCommission(int PurchaseId);
        Task<IEnumerable<CommissionDetails>> getCommission(int AgentId);
        Task<int> CommissionPayment(int AgentId);
    }
}
