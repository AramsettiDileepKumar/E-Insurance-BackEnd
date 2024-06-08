using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
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
    public class PolicyServiceBL : IPolicyBL
    {
        private readonly IPolicyRepo policy;
        public PolicyServiceBL(IPolicyRepo policy)
        {
            this.policy = policy;
        }
        public async Task<bool> AddPolicy(PolicyRequest request)
        {
            try
            {
                PolicyEntity policyEntity = new PolicyEntity
                {
                    PolicyNumber = request.PolicyNumber,
                    PolicyName = request.PolicyName,
                    PolicyType = request.PolicyType,
                    PolicyDescription = request.PolicyDescription,
                    ClaimSettlementRatio=request.ClaimSettlementRatio,
                    EntryAge = request.EntryAge,
                    AnnualPremiumRange = request.AnnualPremiumRange,   
                };
                return await policy.AddPolicy(policyEntity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public async Task<IEnumerable<PolicyEntity>> getAllPolicies()
        {
            return await policy.getAllPolicies();
        }
    }
}
