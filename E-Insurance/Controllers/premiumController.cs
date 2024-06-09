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
    public class premiumController : ControllerBase
    {
        private readonly IPremiumBL premiumBL;
        public premiumController(IPremiumBL premium)
        {
            premiumBL = premium;
        }
        [HttpPost("addPremiumRate")]
        public async Task<IActionResult> AddPremiumRate(PremiumRates premiumRate)
        {

            try
            {
                var result = await premiumBL.AddPremiumRate(premiumRate);
                if (result != 0)
                {
                    return CreatedAtAction(nameof(AddPremiumRate),new ResponseModel<string> {Success=true,Message="Premium Rates Added successfully",Data=null});
                }
                return Ok(new ResponseModel<string> { Success =false, Message = "Error Occured While Adding Premium Rates"});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("calculatePremium")]
        public async Task<IActionResult> CalculatePremium(CalculatePremiumRequest request)
        {
            try
            {
                var premium = await premiumBL.CalculatePremium(request);
                if (premium != 0)
                {
                    return Ok(new ResponseModel<decimal>{Success=true,Message="Premium Calculated Successfully",Data=premium });
                }
                return Ok(new ResponseModel<string> { Success = false, Message = "Error Occured While Caluclating Premium", Data = null });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
