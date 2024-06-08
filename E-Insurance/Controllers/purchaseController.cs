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
        [HttpPost]
        public async Task<IActionResult> PurchasePolicy(purchaseRequest request)
        {
            try
            {
                var result = await purchase.purchasePolicy(request);
                if (result)
                {
                    return CreatedAtAction(nameof(PurchasePolicy), new ResponseModel<bool> { Success = true, Message = "Policy Purchased Successfully", Data = result });
                }
                return Ok(new ResponseModel<bool> { Success = false, Message = "Error Occured While Purchasing Policy", Data = result });

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
                return Ok(result);
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);  
            }
        }
        [HttpPost("calculatePremium")]
        public async Task<IActionResult> CalculatePremium(CalculatePremiumRequest request)
        {
            try
            {
                var premium = await purchase.CalculatePremium(request);
                return Ok(new { Premium = premium });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("addPremiumRate")]
        public async Task<IActionResult> AddPremiumRate( PremiumRates premiumRate)
        {
 
            try
            {
               var result= await purchase.AddPremiumRate(premiumRate);
                return CreatedAtAction(nameof(AddPremiumRate),result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("addPremium")]
        public async Task<IActionResult> AddPremium(PremiumRequest request)
        {
            try
            {
                var result = await purchase.AddPremium(request);
                return CreatedAtAction(nameof(AddPremium), result);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}
