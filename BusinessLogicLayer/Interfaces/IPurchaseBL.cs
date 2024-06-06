using ModelLayer.Entities;
using ModelLayer.RequestDTO.PolicyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IPurchaseBL
    {
        Task<bool> purchasePolicy(purchaseRequest request);
        Task<IEnumerable<PolicyEntity>> ViewPolicies(int CustomerId);
        Task<decimal> CalculatePremium(int PolicyId, int age);
        Task<int> AddPremiumRate(PremiumRates premiumRate);
    }
}
