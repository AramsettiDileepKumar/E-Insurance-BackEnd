using ModelLayer.Entities;
using ModelLayer.RequestDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IRegistrationRepo
    {
        Task<bool> AdminRegistration(UserEntity request);
        Task<UserEntity> Login(UserLoginRequest user);
        Task<bool> AddEmployee(UserEntity request);
        public Task<bool> AddCustomer(UserEntity request);
        Task<bool> AddAgent(UserEntity request);
    }
}
