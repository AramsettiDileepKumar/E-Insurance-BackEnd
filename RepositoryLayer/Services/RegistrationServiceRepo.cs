using Azure.Core;
using Dapper;
using Microsoft.Data.SqlClient;
using ModelLayer.Entities;
using ModelLayer.MailSender;
using ModelLayer.RequestDTO;
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
               return await context.CreateConnection().ExecuteAsync("SP_RegisterAdmin", parameters) > 0;
            }
            catch (Exception ex) 
            {
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
               return await context.CreateConnection().ExecuteAsync("SP_AddEmployee", parameters) > 0;
            }
            catch (Exception ex) { throw new Exception(ex.StackTrace); }
        }
        public async Task<bool> AddAgent(UserEntity request)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("FullName", request.FullName);
                parameters.Add("EmailId", request.EmailId);
                parameters.Add("Password", request.Password);
                parameters.Add("Role", request.Role);
                return await context.CreateConnection().ExecuteAsync("SP_AddAgent", parameters) > 0;
            }
            catch (Exception ex) { throw new Exception(ex.StackTrace); }
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
                parameters.Add("Age", request.Age);
                parameters.Add("PhoneNumber", request.PhoneNumber);
                parameters.Add("Address", request.Address);
                parameters.Add("AgentId", request.AgentId);
                return await context.CreateConnection().ExecuteAsync("SP_AddCustomer", parameters) > 0;
            }
            catch (Exception ex) { throw new Exception(ex.StackTrace); }
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
                else { storedProcedure = "CustomerLogin_SP"; }           
                var user = await context.CreateConnection().QueryFirstOrDefaultAsync<UserEntity>(storedProcedure, parameters);
                if (user == null)
                {
                    throw new Exception($"User with email '{userLogin.EmailId}' not found.");
                }
                return user;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
 