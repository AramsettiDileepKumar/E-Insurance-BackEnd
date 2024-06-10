using Dapper;
using Microsoft.Data.SqlClient;
using ModelLayer.Entities;
using ModelLayer.RequestDTO.PolicyModels;
using ModelLayer.RequestDTO.Purchase;
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
    public class PurchaseServiceRepo : IPurchaseRepo
    {
        private readonly DapperContext context;
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        public PurchaseServiceRepo(DapperContext context)
        {
            this.context = context;
        }
        public async Task<bool> CustomerDetails(CustomerDetailsRequest request, int CustomerId)
        {
            try
            {
                using (var connection = context.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@CustomerId", CustomerId, DbType.Int32);
                    parameters.Add("@PolicyId", request.PolicyId, DbType.Int32);
                    parameters.Add("@AgentId", request.AgentId, DbType.Int32);
                    parameters.Add("@AnnualIncome", request.AnnualIncome, DbType.Decimal);
                    parameters.Add("@FirstName", request.FirstName, DbType.String);
                    parameters.Add("@LastName", request.LastName, DbType.String);
                    parameters.Add("@Gender", request.Gender, DbType.String);
                    parameters.Add("@DateofBirth", request.DateOfBirth, DbType.Date);
                    parameters.Add("@MobileNumber", request.MobileNumber, DbType.Int64);
                    parameters.Add("@Address", request.Address, DbType.String);
                    _logger.Info("Customer Details Executed");
                    var result = await connection.ExecuteAsync("SP_AddCustomerDetails", parameters, commandType: CommandType.StoredProcedure);
                    return result<0;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while Executing Customer Details");
                throw new Exception("An error occurred while processing the policy purchase "+ex.Message);
            }
        }
       public async Task<IEnumerable<PolicyPurchaseEntity>> ViewPolicies(int CustomerId)
        {
            try
            {
                using (var connections = context.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("CustomerId",CustomerId, DbType.Int32);
                    _logger.Info("Get policies Executed");
                    return await connections.QueryAsync<PolicyPurchaseEntity>("SP_getCustomerPolicies", parameters);
                }
            }
            catch(Exception ex) 
            {
                _logger.Error(ex, "Error occurred while Fetching policy");
                throw ex;
            }
        }
       
        public async Task<int> PurchasePolicy(int CustomerId, purchaseRequest request)
        {
            try
            {
                using(var connection = context.CreateConnection()) 
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("CustomerId",CustomerId);
                    parameters.Add("PolicyId", request.PolicyId);
                    parameters.Add("CoverageAmount", request.CoverageAmount, DbType.Int32);
                    parameters.Add("Tenure", request.Tenure, DbType.Int32);
                    parameters.Add("PremiumType", request.PremiumType, DbType.String);
                    parameters.Add("PremiumAmount",request.PremiumAmount, DbType.Int32);
                    _logger.Info("Policy Purchased Executed");
                    return await connection.ExecuteAsync("SP_InsertPolicyPurchase", parameters);
                }
            }
            catch(Exception ex)
            {
                _logger.Error(ex, "Error occurred while Purchasing policy");
                throw ex;
            }
        }
        public async Task<int> PolicyCancellation(int CustomerId, int PolicyId)
        {
            try
            {
                using( var connection = context.CreateConnection()) 
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("PolicyId", PolicyId);
                    parameters.Add("CustomerId", CustomerId);
                    _logger.Info("Policy cancellation Executed");
                    return await connection.ExecuteAsync("SP_PolicyCancellation", parameters);
                }
            }
            catch(Exception ex) 
            {
                _logger.Error(ex, "Error occurred while Policy cancellation");
                throw new Exception(ex.Message);
            }
        }

       
        public async Task<IEnumerable<PolicyPurchaseEntity>> AgentPolicies(int AgentId)
        {
            try
            {
                using (var connections = context.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("AgentId",AgentId, DbType.Int32);
                    _logger.Info("Agent Policies Executed");
                    return await connections.QueryAsync<PolicyPurchaseEntity>("SP_getAgentPolicies", parameters);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while Fetching policies by AgentId");
                throw ex;
            }
        }
    }
}
