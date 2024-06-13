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
    public class CommissionServiceRepo:ICommissionRepo
    {
        private readonly DapperContext context;
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        public CommissionServiceRepo(DapperContext context)
        {
            this.context = context;
        }
        public async Task<int> AddCommissionRate(CommissionRates commissionRate)
        {
            try
            {
                using (var connection = context.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@PolicyType", commissionRate.PolicyType, DbType.String);
                    parameters.Add("@Rate", commissionRate.CommissionRate, DbType.Decimal);
                    var result = await connection.ExecuteAsync("SP_AddCommissionRate", parameters, commandType: CommandType.StoredProcedure);
                    _logger.Info("Add Commission Rate Executed");
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while Adding Commission rate");
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> CalculateCommission(int PurchaseId)
        {
            try
            {
                using (var connection = context.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("PurchaseId", PurchaseId);
                    _logger.Info("Calculate Commission Executed");
                    return await connection.ExecuteAsync("SP_CalculateCommission", parameters) > 0;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while calculating commission");
                throw new Exception(ex.Message);
            }
        }
        public async Task<IEnumerable<CommissionDetails>> getCommission(int AgentId)
        {
            try
            {
                using (var connection = context.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("AgentId", AgentId);
                    _logger.Info("Get commission by Agent Executed");
                    return await connection.QueryAsync<CommissionDetails>("SP_getAgentCommission", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while Fetching Commission");
                throw new Exception(ex.Message);
            }
        }
        public async Task<int> CommissionPayment(int AgentId)
        {
            try
            {
                using(var connection = context.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("AgentId", AgentId);
                    _logger.Info("Commission Payment For Agent is Executed");
                    return await connection.ExecuteAsync("SP_PayAgentCommission", parameters);
                }
            }
            catch(Exception ex) 
            {
                _logger.Error(ex, "Error occurred while Commission Payment");
                throw new Exception(ex.Message);    
            }
        }
    }
}
