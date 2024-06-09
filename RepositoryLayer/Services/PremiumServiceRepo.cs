using Dapper;
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
    public class PremiumServiceRepo:IPremiumRepo
    {
        private readonly DapperContext context;
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        public PremiumServiceRepo(DapperContext context) 
        {
            this.context = context;
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
                    _logger.Info("Add Premium Rate Executed");
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while adding premium rates");
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
                    parameters.Add("CoverageAmount", request.CoverageAmount, DbType.Int32);
                    parameters.Add("Tenure", request.Tenure, DbType.Int32);
                    parameters.Add("PremiumType", request.PremiumType, DbType.String);
                    parameters.Add("@Premium", dbType: DbType.Decimal, direction: ParameterDirection.Output);
                    await connection.ExecuteAsync("SP_CalculatePremium", parameters, commandType: CommandType.StoredProcedure);
                    decimal premium = parameters.Get<decimal>("@Premium");
                    _logger.Info("Calculate Premium Executed");
                    return premium;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while calcuting premium");
                throw new Exception("An error occurred while calculating the premium " + ex.Message);
            }
        }
    }
}
