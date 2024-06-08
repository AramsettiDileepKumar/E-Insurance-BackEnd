using ModelLayer.Entities;
using ModelLayer.RequestDTO.PolicyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IPurchaseRepo
    {
        Task<bool> purchasePolicy(purchaseRequest request);
        Task<IEnumerable<PolicyEntity>> ViewPolicies(int CustomerId);
        Task<decimal> CalculatePremium(CalculatePremiumRequest request);
        Task<int> AddPremiumRate(PremiumRates premiumRate);
        Task<int> AddPremium(PremiumRequest premiumRequest);
    }
}
