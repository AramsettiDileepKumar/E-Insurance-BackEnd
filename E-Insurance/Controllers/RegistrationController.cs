using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
                if (result)
                {
                    var responseModel = new ResponseModel<string>
                    {
                        Success = true,
                        Message = "Admin Added Successfully",
                        Data = null
                    };
                    return CreatedAtAction(nameof(AdminRegistration), responseModel);
                }
                return Ok(new ResponseModel<string> { Success = false, Message = "Error Occured While Admin Registration" });
            }
            catch (Exception ex) 
            {
                return Ok(ex.Message);
            }
        }
        [HttpPost("employee")]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> AddEmployee(UserRequest request)
        {
            try
            {
                var result = await user.AddEmployee(request);
                if (result)
                {
                    return CreatedAtAction(nameof(AddEmployee), new ResponseModel<string> { Success = true, Message = "Employee Added Successfully",Data=null });
                }
                return Ok(new ResponseModel<bool> { Success = false, Message = "Error Occured While Employee Regstration" });
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);  
            }
        }
        [HttpPost("agent")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddAgent(AgentRequest request)
        {
            try
            {
                var result = await user.AddAgent(request);
                if (result)
                {
                    return CreatedAtAction(nameof(AddAgent), new ResponseModel<string> { Success = true, Message = "Agent Added Successfully", Data = null });
                }
                return Ok(new ResponseModel<bool> { Success = false, Message = "An Error Occured While Agent Registration" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("customers")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddCustomers(UserRequest request)
        {
            try
            {
                var result = await user.AddCustomer(request);
                if (result)
                {
                    return CreatedAtAction(nameof(AddCustomers), new ResponseModel<string> { Success = true, Message = "Customer Added Successfully", Data = null });
                }
                return Ok(new ResponseModel<bool> { Success = false, Message = "An Error Occured while Customer Registration" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("login")]
        public async Task<IActionResult> Login(UserLoginRequest userLogin)
        {
            try
            {
                var token = await user.Login(userLogin);
                if (token != null)
                {
                    return Ok(new ResponseModel<string> { Success = true, Message = "Login Successfull", Data = token });
                }
                return Ok(new ResponseModel<string> { Success=false,Message="SomeThing Went wrong while Login"});
            }
            catch (SqlException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
