using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Entities;
using ModelLayer.ResponseDTO;
using System.Security.Claims;
using static MassTransit.ValidationResultExtensions;

namespace E_Insurance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class commissionController : ControllerBase
    {
        private readonly ICommissionBL commissionBL;
        public commissionController(ICommissionBL commissionBL)
        {
            this.commissionBL = commissionBL;
        }
        [HttpPost("addCommisionRate")]
        public async Task<IActionResult> AddCommisionRate(CommissionRates commissionRate)
        {

            try
            {
                var result = await commissionBL.AddCommissionRate(commissionRate);
                if (result!=0)
                {
                    return CreatedAtAction(nameof(AddCommisionRate),new ResponseModel<string> {Success=true,Message="Commission Rate Added Successfully",Data=null });
                }
                return Ok(new ResponseModel<int> { Success = false, Message = "Error Occued While Adding Commission Rates" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("calculateCommission")]
        public async Task<IActionResult> CalculateCommission(int purchaseId)
        {
            try
            {

                var result = await commissionBL.CalculateCommission(purchaseId);
                if (result)
                {
                    return Ok(new ResponseModel<string> { Success=true,Message="Commission calculated Successfully",Data = null });
                }
                return Ok(new ResponseModel<int> { Success = false, Message = "Something Went Wrong" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("commission")]
        public async Task<IActionResult> getCommission()
        {
            try
            {
                var AgentId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var result = await commissionBL.getCommission(AgentId);
                if (result != null)
                {
                    return Ok(new ResponseModel<IEnumerable<CommissionDetails>> { Success=true,Message="Commission Fetched Successfully",Data=result});
                }
                return Ok(new ResponseModel<IEnumerable<CommissionDetails>> { Success = false, Message = "Error Occured While Fetching Commission", Data =null });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CommissionPayment(int AgentId)
        {
            try
            {
                var result=await commissionBL.CommissionPayment(AgentId);
                if (result != 0) {
                    return Ok(new ResponseModel<int> { Success = true, Message = "Commission Fetched Successfully", Data = result });
                }
                return Ok(new ResponseModel<string> { Success = false, Message = "Error Occured While Commission Payment", Data =null });
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
