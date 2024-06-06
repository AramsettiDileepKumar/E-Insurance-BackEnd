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
                PurchaseEntity entity = new PurchaseEntity
                {
                    CustomerId = request.CustomerId,
                    AgentId = request.AgentId,
                    PolicyId = request.PolicyId,
                    PurchaseDate=DateTime.Now,  
                };
                return await purchase.purchasePolicy(entity);
            }
            catch(Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
