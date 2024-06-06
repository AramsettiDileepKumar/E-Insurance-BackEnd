using BusinessLogicLayer.Interfaces;
using ModelLayer.Entities;
using ModelLayer.RequestDTO.PolicyModels;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class PurchaseServiceBL:IPurchaseBL
    {
        private readonly IPurchaseRepo purchase;
        public PurchaseServiceBL(IPurchaseRepo purchase)
        {
            this.purchase = purchase;
        }
        public async Task<bool> purchasePolicy(purchaseRequest request)
        {
            try
            {
                return await purchase.purchasePolicy(request);
            }
            catch(Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<IEnumerable<PolicyEntity>> ViewPolicies(int CustomerId)
        {
            try
            {
                return await purchase.ViewPolicies(CustomerId);
            }
            catch(Exception ex) { throw new Exception(ex.Message); }
        }
        public async Task<decimal> CalculatePremium(int PolicyId, int age)
        {
            try
            {
                return await purchase.CalculatePremium(PolicyId, age);
            }
            catch(Exception ex) { throw new Exception(ex.Message); }
        }
        public async Task<int> AddPremiumRate(PremiumRates premiumRate)
        {
            try
            {
               return await purchase.AddPremiumRate(premiumRate);
            }
            catch(Exception ex) { throw new Exception(ex.Message); }
        }
    }
}
