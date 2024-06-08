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
        Task<bool> CustomerDetails(CustomerDetailsRequest request,int CustomerId);
        Task<IEnumerable<PolicyPurchaseEntity>> ViewPolicies(int CustomerId);
        Task<decimal> CalculatePremium(CalculatePremiumRequest request);
        Task<int> AddPremiumRate(PremiumRates premiumRate);
        Task<int> PurchasePolicy(int CustomerId, int PolicyId);
        Task<int> PolicyCancellation(int CustomerId, int PolicyId);
    }
}
