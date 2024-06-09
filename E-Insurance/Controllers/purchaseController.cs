using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Entities;
using ModelLayer.RequestDTO.PolicyModels;
using ModelLayer.ResponseDTO;
using System.Security.Claims;

namespace E_Insurance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class purchaseController : ControllerBase
    {
        private readonly IPurchaseBL purchase;
        public purchaseController(IPurchaseBL purchase)
        {
            this.purchase = purchase;
        }
        [HttpPost("customerDetails")]
        public async Task<IActionResult> CustomerDetails(CustomerDetailsRequest request)
        {
            try
            {
                var CustomerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var result = await purchase.CustomerDetails(request,CustomerId);
                if (result)
                {
                    return CreatedAtAction(nameof(CustomerDetails), new ResponseModel<string> { Success = true, Message = "Policy Purchased Successfully", Data = null });
                }
                return Ok(new ResponseModel<bool> { Success = false, Message = "Error Occured While Purchasing Policy" });
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> ViewPolicies()
        {
            try
            {
                var CustomerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var result = await purchase.ViewPolicies(CustomerId);
                if (result != null)
                {
                    return Ok(new ResponseModel<IEnumerable<PolicyPurchaseEntity>> {Success=true,Message="Policies Fetched Successfully",Data=result });
                }
                return Ok(new ResponseModel<IEnumerable<PolicyPurchaseEntity>> { Success = false, Message = "Error Occured While Fetching Policies" });
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);  
            }
        }
       
       
        [HttpPost("purchasePolicy")]
        public async Task<IActionResult> PolicyPurchase(int PolicyId)
        {
            try
            {
                var CustomerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var result=await purchase.PurchasePolicy(CustomerId,PolicyId);
                if (result != 0)
                {
                    return CreatedAtAction(nameof(PolicyPurchase), new ResponseModel<string> { Success = true, Message = "Policies Purchased Successfully", Data = null }); 
                }
                return Ok(new ResponseModel<bool> { Success = false, Message = "Error Occured While Purchasing Policy" });
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("cancelPolicy")]
        public async Task<IActionResult> PolicyCancellation(int policyId)
        {
            try
            {
                var CustomerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var result=await purchase.PolicyCancellation(CustomerId,policyId);
                if (result != 0)
                {
                    return Ok(new ResponseModel<string> { Success = true, Message = "Policies Cancelled Successfully",Data=null});
                }
                return Ok(new ResponseModel<bool> { Success = false, Message = "Error Occured While cancelling Policy" });
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("byAgent")]
        public async Task<IActionResult> AgentPolicies()
        {
            try
            {
                var AgentId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var result = await purchase.AgentPolicies(AgentId);
                if (result != null)
                {
                    return Ok(new ResponseModel<IEnumerable<PolicyPurchaseEntity>> { Success = true, Message = "Policies Fetched by Agent Successfully",Data=result });
                }
                return Ok(new ResponseModel<string> { Success = false, Message = "Error Occured While Fetching Policies" ,Data=null});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
