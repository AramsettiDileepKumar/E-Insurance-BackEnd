using BusinessLogicLayer.Interfaces;
using ModelLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class CommissionServiceBL:ICommissionBL
    {
        private readonly ICommissionRepo repo;
        public CommissionServiceBL(ICommissionRepo repo) 
        {
            this.repo = repo;
        }    
        public async Task<int> AddCommissionRate(CommissionRates commissionRate)
        {
            return await repo.AddCommissionRate(commissionRate);
        }
        public async Task<bool> CalculateCommission(int PurchaseId)
        {
            return await repo.CalculateCommission(PurchaseId);
        }
        public async Task<IEnumerable<CommissionDetails>> getCommission(int AgentId)
        {
            return await repo.getCommission(AgentId);
        }
    }
}
