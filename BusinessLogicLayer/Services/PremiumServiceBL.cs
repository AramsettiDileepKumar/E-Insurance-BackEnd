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
    public class PremiumServiceBL:IPremiumBL
    {
        private readonly IPremiumRepo premiumRepo;
        public PremiumServiceBL(IPremiumRepo premiumRepo) 
        {
            this.premiumRepo = premiumRepo;
        }
        public async Task<decimal> CalculatePremium(CalculatePremiumRequest request)
        {
            try
            {
                return await premiumRepo.CalculatePremium(request);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public async Task<int> AddPremiumRate(PremiumRates premiumRate)
        {
            try
            {
                return await premiumRepo.AddPremiumRate(premiumRate);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
    }
}
