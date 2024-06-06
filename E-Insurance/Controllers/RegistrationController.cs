using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ModelLayer.RequestDTO.Registration;
using ModelLayer.ResponseDTO;

namespace E_Insurance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class registrationController : ControllerBase
    {
        private readonly IRegistrationBL user;
        public registrationController(IRegistrationBL user) 
        {
            this.user = user;
        }
        [HttpPost("admin")]
        public async Task<IActionResult> AdminRegistration(UserRequest request)
        {
            try
            {
                var result = await user.AdminRegistration(request);
                var responseModel = new ResponseModel<bool>
                {
                    Success = true,
                    Message = "Admin Added Successfully",
                    Data = result
                };
                return CreatedAtAction(nameof(AdminRegistration), responseModel);
            }
            catch (Exception ex) 
            {
                return Ok(ex.Message);
            }
        }
        [HttpPost("employee")]
        public async Task<IActionResult> AddEmployee(UserRequest request)
        {
            try
            {
                var result = await user.AddEmployee(request);
                return Ok(new ResponseModel<bool> {Success=true,Message="Employee Added Successfully"}); ;
            }
            catch (Exception ex) 
            {
                return Ok(ex.Message);  
            }
        }
        [HttpPost("agent")]
        public async Task<IActionResult> AddAgent(AgentRequest request)
        {
            try
            {
                var result = await user.AddAgent(request);
                return Ok(new ResponseModel<bool> { Success = true, Message = "Agent Added Successfully" });
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
        [HttpPost("customers")]
        public async Task<IActionResult> AddCustomers(UserRequest request)
        {
            try
            {
                var result = await user.AddCustomer(request);
                return Ok(new ResponseModel<bool> { Success = true, Message = "Customer Added Successfully" });
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UserLoginRequest userLogin)
        {
            try
            {
                var token = await user.Login(userLogin);
                return Ok(new ResponseModel<string> { Success = true, Message = "Login Successfull",Data=token });
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
