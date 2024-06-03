using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ModelLayer.RequestDTO;

namespace E_Insurance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationBL user;
        public RegistrationController(IRegistrationBL user) 
        {
            this.user = user;
        }
        [HttpPost("AdminRegistration")]
        public async Task<IActionResult> AdminRegistration(UserRequest request)
        {
            try
            {
              return Ok( await user.AdminRegistration(request));
            }
            catch (Exception ex) 
            {
                return Ok(ex.Message);
            }
        }
        [HttpPost("AddEmployee")]
        public async Task<IActionResult> AddEmployee(UserRequest request)
        {
            try
            {
                return Ok(await user.AddEmployee(request));
            }
            catch (Exception ex) 
            {
                return Ok(ex.Message);  
            }
        }
        [HttpPost("AddAgent")]
        public async Task<IActionResult> AddAgent(AgentRequest request)
        {
            try
            {
                return Ok(await user.AddAgent(request));
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
        [HttpPost("AddCustomers")]
        public async Task<IActionResult> AddCustomers(CustomerRequest request)
        {
            try
            {
                return Ok(await user.AddCustomer(request));
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginRequest userLogin)
        {
            try
            {
                var token = await user.Login(userLogin);

                return Ok(token);
            }
            catch (SqlException ex)
            {
                return Ok(ex.Message);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
    }
}
