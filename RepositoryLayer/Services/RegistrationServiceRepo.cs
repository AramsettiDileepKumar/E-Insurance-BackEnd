using Azure.Core;
using Dapper;
using Microsoft.Data.SqlClient;
using ModelLayer.Entities;
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
                parameters.Add("UserId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                await context.CreateConnection().ExecuteAsync("SP_RegisterAdmin",parameters);
                var userId = parameters.Get<int>("UserId");
                var paramtrs=new DynamicParameters();   
                paramtrs.Add("UserId",userId);
                paramtrs.Add("FullName",request.FullName);
                return await context.CreateConnection().ExecuteAsync("SP_AddAdmin",paramtrs) > 0; 
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> AddEmployee(UserEntity request)
        {
            try {
                var parameters = new DynamicParameters();
                parameters.Add("FullName", request.FullName);
                parameters.Add("EmailId", request.EmailId);
                parameters.Add("Password", request.Password);
                parameters.Add("Role", request.Role);
                parameters.Add("UserId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                await context.CreateConnection().ExecuteAsync("SP_RegisterAdmin", parameters);
                var userId = parameters.Get<int>("UserId");
                var paramtrs = new DynamicParameters();
                paramtrs.Add("UserId", userId);
                paramtrs.Add("FullName", request.FullName);
                return await context.CreateConnection().ExecuteAsync("SP_AddEmployee", paramtrs) > 0;
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
                parameters.Add("UserId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                await context.CreateConnection().ExecuteAsync("SP_RegisterAdmin", parameters);
                var userId = parameters.Get<int>("UserId");
                var paramtrs = new DynamicParameters();
                paramtrs.Add("UserId", userId);
                paramtrs.Add("FullName", request.FullName);
                paramtrs.Add("AgentCommissionRate",request.AgentCommissionRate);    
                return await context.CreateConnection().ExecuteAsync("SP_AddAgent", paramtrs) > 0;
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
                parameters.Add("UserId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                await context.CreateConnection().ExecuteAsync("SP_RegisterAdmin", parameters);
                var userId = parameters.Get<int>("UserId");
                var paramtrs = new DynamicParameters();
                paramtrs.Add("UserId", userId);
                paramtrs.Add("FullName", request.FullName);
                paramtrs.Add("Age", request.age);
                paramtrs.Add("PhoneNumber",request.PhoneNumber);
                paramtrs.Add("Address",request.Address);
                return await context.CreateConnection().ExecuteAsync("SP_AddCustomer", paramtrs) > 0;
            }
            catch (Exception ex) { throw new Exception(ex.StackTrace); }
        }
        public async Task<UserEntity> Login(UserLoginRequest userLogin)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Email", userLogin.EmailId);
                var user = await context.CreateConnection().QueryFirstOrDefaultAsync<UserEntity>("login_sp", parameters);
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
 