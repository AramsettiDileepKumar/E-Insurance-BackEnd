﻿using Dapper;
using ModelLayer.Entities;
using ModelLayer.RequestDTO.Paymentmodels;
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
     public class PaymentServiceRepo:IPaymentRepo
    {
        private readonly DapperContext context;
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        public PaymentServiceRepo(DapperContext context) 
        {
            this.context = context;
        }
        public async Task<int> AddPayment(PaymentRequest paymentRequest, int CustomerId)
        {
            try
            {
                using (var connection = context.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("PaymentMethod", paymentRequest.PaymentMethod);
                    parameters.Add("PurchaseId", paymentRequest.PurchaseId);
                    parameters.Add("PaymentDate", DateTime.Now);
                    parameters.Add("CustomerId", CustomerId);
                    parameters.Add("PaymentId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    _logger.Info("Add payment Executed");
                    await connection.ExecuteAsync("SP_InsertPayment", parameters, commandType: CommandType.StoredProcedure);
                    int paymentId = parameters.Get<int>("PaymentId");
                    return paymentId;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while Adding Payment");
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<PaymentEntity>> getPayments(int CustomerId)
        {
            try
            {
                using(var connection=context.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("CustomerId", CustomerId);
                    _logger.Info("Get Payments Executed");
                    return await connection.QueryAsync<PaymentEntity>("SP_getPayments", parameters);
                }
            }
            catch(Exception ex) 
            {
                _logger.Error(ex, "Error occured while Getting Payments ");
                throw new Exception(ex.Message); 
            }
        }
        public async Task<PaymentEntity> getReceipt(int PaymentId, int CustomerId)
        {
            try
            {
                using( var connection=context.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("PaymentId", PaymentId);
                    parameters.Add("CustomerId", CustomerId);
                    _logger.Info("Get Receipt Executed");
                    return await connection.QuerySingleAsync<PaymentEntity>("SP_getPolicyPayment", parameters);
                }
            }
            catch(Exception ex)
            {
                _logger.Error(ex, "Error occured while Getting Receipt ");
                throw new Exception(ex.Message);
            }
        }
    }
}
