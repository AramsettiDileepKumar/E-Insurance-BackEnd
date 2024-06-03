using ModelLayer.RequestDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IRegistrationBL
    {
       public Task<bool> AdminRegistration(UserRequest user);
        public Task<bool> AddEmployee(UserRequest request);
        public Task<bool> AddAgent(AgentRequest request);
        public Task<bool> AddCustomer(CustomerRequest request);
        public Task<string> Login(UserLoginRequest user);
    }
}
