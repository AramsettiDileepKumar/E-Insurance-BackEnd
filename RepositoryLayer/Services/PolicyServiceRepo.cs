using Dapper;
using ModelLayer.Entities;
using NLog;
using RepositoryLayer.Context;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class PolicyServiceRepo:IPolicyRepo
    {
        private readonly DapperContext context;
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        public PolicyServiceRepo(DapperContext context)
        {
            this.context = context;
        }
        public async Task<bool> AddPolicy(PolicyEntity request)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("PolicyNumber", request.PolicyNumber);
                parameters.Add("PolicyName", request.PolicyName);
                parameters.Add("PolicyDescription", request.PolicyDescription);
                parameters.Add("PolicyType", request.PolicyType);
                parameters.Add("ClaimSettlementRatio", request.ClaimSettlementRatio);
                parameters.Add("EntryAge", request.EntryAge);
                parameters.Add("AnnualPremiumRange", request.AnnualPremiumRange);

                _logger.Info("Policy Insertion Executed");
                using (var connection = context.CreateConnection())
                {
                    return await connection.ExecuteAsync("SP_InsertPolicy", parameters, commandType: CommandType.StoredProcedure) > 0;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while inserting policy");
                throw;
            }
        }
        public async Task<IEnumerable<PolicyEntity>> getAllPolicies()
        {
            try
            {
                using (var connection = context.CreateConnection())
                {
                    _logger.Info("GetAllPolicies Executed");
                    return await connection.QueryAsync<PolicyEntity>("SP_getAllPolicies");
                }
            }
            catch(Exception ex)
            {
                _logger.Error(ex, "Error occurred while getting all policies");
                throw;
            }
        }
    }
}
