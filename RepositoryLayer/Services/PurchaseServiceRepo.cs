using Dapper;
using Microsoft.Data.SqlClient;
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
    public class PurchaseServiceRepo : IPurchaseRepo
    {
        private readonly DapperContext context;
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        public PurchaseServiceRepo(DapperContext context)
        {
            this.context = context;
        }
        public async Task<bool> purchasePolicy(PurchaseEntity request)
        {
            try
            {
                using (var connection = context.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@CustomerId", request.CustomerId, DbType.Int32);
                    parameters.Add("@PolicyId", request.PolicyId, DbType.Int32);
                    parameters.Add("@AgentId", request.AgentId, DbType.Int32);
                    parameters.Add("@PurchaseDate", request.PurchaseDate, DbType.DateTime);
                    var result =await connection.ExecuteAsync("SP_AddPurchase", parameters);
                    _logger.Info("Policy Purchased Executed");
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while Purchasing policy");
                throw ex;
            }
        }
    }
}
