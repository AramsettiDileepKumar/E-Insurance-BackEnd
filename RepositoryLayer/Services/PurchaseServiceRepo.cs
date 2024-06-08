using Dapper;
using Microsoft.Data.SqlClient;
using ModelLayer.Entities;
using ModelLayer.RequestDTO.PolicyModels;
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
                    parameters.Add("@PurchaseDate",DateTime.Now, DbType.DateTime);
                    _logger.Info("Policy Purchased Executed");
                    var result = await connection.ExecuteAsync("SP_AddPurchase", parameters, commandType: CommandType.StoredProcedure);
                    return result <0;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while Purchasing policy");
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
                    _logger.Info("Policy Purchased Executed");
                    return await connections.QueryAsync<PolicyPurchaseEntity>("SP_getCustomerPolicies", parameters);
                }
            }
            catch(Exception ex) 
            {
                _logger.Error(ex, "Error occurred while Fetching policy");
                throw ex;
            }
        }
        public async Task<int> AddPremiumRate(PremiumRates premiumRate)
        {
            try
            {
                using (var connection = context.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@PolicyType", premiumRate.PolicyType, DbType.String);
                    parameters.Add("@AgeGroup", premiumRate.AgeGroup, DbType.String);
                    parameters.Add("@Rate", premiumRate.Rate, DbType.Decimal);
                    var result = await connection.ExecuteAsync("SP_AddPremiumRate", parameters, commandType: CommandType.StoredProcedure);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<decimal> CalculatePremium(CalculatePremiumRequest request)
        {
            try
            {
                using (var connection = context.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("PolicyId", request.PolicyId, DbType.Int32);
                    parameters.Add("CoverageAmount",request.CoverageAmount, DbType.Int32);
                    parameters.Add("Tenure",request.Tenure, DbType.Int32);
                    parameters.Add("PremiumType", request.PremiumType, DbType.String);
                    parameters.Add("@Premium", dbType: DbType.Decimal, direction: ParameterDirection.Output);
                    await connection.ExecuteAsync("SP_CalculatePremium", parameters, commandType: CommandType.StoredProcedure);
                    decimal premium = parameters.Get<decimal>("@Premium");
                    return premium;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while calculating the premium "+ex.Message);
            }
        }
        public async Task<int> PurchasePolicy(int CustomerId, int PolicyId)
        {
            try
            {
                using(var connection = context.CreateConnection()) 
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("CustomerId",CustomerId);
                    parameters.Add("PolicyId", PolicyId);
                    return await connection.ExecuteAsync("SP_InsertPolicyPurchase", parameters);
                }
            }
            catch(Exception ex)
            {
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
                    return await connection.ExecuteAsync("SP_PolicyCancellation", parameters);
                }
            }
            catch(Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
