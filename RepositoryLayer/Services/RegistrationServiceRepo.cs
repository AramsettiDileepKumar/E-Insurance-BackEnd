using Azure.Core;
using Dapper;
using Microsoft.Data.SqlClient;
using ModelLayer.Entities;
using ModelLayer.MailSender;
using ModelLayer.RequestDTO.Registration;
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
    public class RegistrationServiceRepo:IRegistrationRepo
    {
        private readonly DapperContext context;
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        public RegistrationServiceRepo(DapperContext context)
        {
            this.context = context; 
        }
        
        public async Task<bool> AdminRegistration(UserEntity request)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("FullName", request.FullName);
                parameters.Add("EmailId", request.EmailId);
                parameters.Add("Password",request.Password);
                parameters.Add("Role", request.Role);
                _logger.Info("Admin Registration Executed");
                using (var connection = context.CreateConnection())
                {
                    return await connection.ExecuteAsync("SP_RegisterAdmin", parameters) < 0;
                }
            }
            catch (Exception ex) 
            {
                _logger.Error(ex, "Error occured while Admin Registration ");
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> AddEmployee(UserEntity request)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("FullName", request.FullName);
                parameters.Add("EmailId", request.EmailId);
                parameters.Add("Password", request.Password);
                parameters.Add("Role", request.Role);
                _logger.Info("Employee Registration Executed");
                return await context.CreateConnection().ExecuteAsync("SP_AddEmployee", parameters) < 0;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occured while Employee Registration ");
                throw new Exception(ex.StackTrace); }
        }
        public async Task<bool> AddAgent(UserEntity request)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("FullName", request.FullName);
                parameters.Add("EmailId", request.EmailId);
                parameters.Add("Password", request.Password);
                parameters.Add("Location", request.Location);
                parameters.Add("Role", request.Role);
                _logger.Info("Agent Registration Executed");
                var result= await context.CreateConnection().ExecuteAsync("SP_AddAgent", parameters);
                return result < 0;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occured while Agent Registration ");
                throw new Exception(ex.StackTrace); }
        }
        public async Task<bool> AddCustomer(UserEntity request)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("FullName", request.FullName);
                parameters.Add("EmailId", request.EmailId);
                parameters.Add("Password", request.Password);
                parameters.Add("Role", request.Role);
                _logger.Info("Customer Registration Executed");
                return await context.CreateConnection().ExecuteAsync("SP_AddCustomer", parameters) < 0;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occured while customer Registration ");
                throw new Exception(ex.StackTrace); }
        }
        public async Task<UserEntity> Login(UserLoginRequest userLogin)
        {
            try
            {
                string storedProcedure ;
                var parameters = new DynamicParameters();
                parameters.Add("@Email", userLogin.EmailId);
                if (userLogin.Role == "Admin"){ storedProcedure = "AdminLogin_sp"; }
                else if(userLogin.Role == "Employee") { storedProcedure = "EmployeeLogin_SP"; }
                else if( userLogin.Role == "Agent") { storedProcedure = "AgentLogin_SP"; }
                else if(userLogin.Role == "Customer") { storedProcedure = "CustomerLogin_SP"; }
                else { storedProcedure = null; }
                var user = await context.CreateConnection().QueryFirstOrDefaultAsync<UserEntity>(storedProcedure, parameters);
                if (user == null)
                {
                    throw new Exception($"User with email '{userLogin.EmailId}' not found.");
                }
                _logger.Info("Login API Executed");
                return user;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occured while Login ");
                throw new Exception(ex.Message);
            }
        }
    }
}
 