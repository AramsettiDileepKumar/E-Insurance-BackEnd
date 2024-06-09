using ModelLayer.Entities;
using ModelLayer.RequestDTO.PolicyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IPremiumBL
    {
        Task<decimal> CalculatePremium(CalculatePremiumRequest request);
        Task<int> AddPremiumRate(PremiumRates premiumRate);
    }
}
