using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Entities;
using ModelLayer.RequestDTO.PolicyModels;
using ModelLayer.ResponseDTO;

namespace E_Insurance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PoliciesController : ControllerBase
    {
        private readonly IPolicyBL policy;
        public PoliciesController(IPolicyBL policy)
        {
            this.policy = policy;
        }
        [HttpPost]
        public async Task<IActionResult> AddPolicy(PolicyRequest request)
        {
            try
            {
                var result = await policy.AddPolicy(request);
                if (result)
                {
                    return CreatedAtAction(nameof(AddPolicy),new ResponseModel<bool> {Success=true,Message="Policy added Successfully",Data=result } ); 
                }
                return Ok(new ResponseModel<bool> { Success = false, Message = "Error Occured While Creating Policy", Data = result });
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> getAllPolicies()
        {
            try
            {
                var result = await policy.getAllPolicies();
                if(result!=null)
                {
                    return Ok(new ResponseModel<IEnumerable<PolicyEntity>> { Success = true, Message = "Policies Fetched Successfully", Data = result });
                }
                return Ok(new ResponseModel<IEnumerable<PolicyEntity>> { Success = false, Message = "Error Occured while fetching Policies", Data = result });
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);  
            }
        }
    }
}
