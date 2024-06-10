using ModelLayer.Entities;
using ModelLayer.RequestDTO.PolicyModels;
using ModelLayer.RequestDTO.Purchase;
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
       
        Task<int> PurchasePolicy(int CustomerId, purchaseRequest request);
        Task<int> PolicyCancellation(int CustomerId, int PolicyId);
        
        Task<IEnumerable<PolicyPurchaseEntity>> AgentPolicies(int AgentId);

    }
}
