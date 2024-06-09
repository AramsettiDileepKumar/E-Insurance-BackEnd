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
        public async Task<bool> CustomerDetails(CustomerDetailsRequest request, int CustomerId)
        {
            try
            {
                return await purchase.CustomerDetails(request, CustomerId);
            }
            catch(Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<IEnumerable<PolicyPurchaseEntity>> ViewPolicies(int CustomerId)
        {
            try
            {
                return await purchase.ViewPolicies(CustomerId);
            }
            catch(Exception ex) { throw new Exception(ex.Message); }
        }
      
        public async Task<int> PurchasePolicy(int CustomerId, int PolicyId)
        {
            return await purchase.PurchasePolicy(CustomerId, PolicyId);
        }
        public async Task<int> PolicyCancellation(int CustomerId, int PolicyId)
        {
            return await purchase.PolicyCancellation(CustomerId, PolicyId); 
        }
      
        public async Task<IEnumerable<PolicyPurchaseEntity>> AgentPolicies(int AgentId)
        {
            try
            {
                return await purchase.AgentPolicies(AgentId);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
    }
}
