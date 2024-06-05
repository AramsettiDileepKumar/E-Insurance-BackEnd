using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.RequestDTO.PolicyModels;
using ModelLayer.ResponseDTO;

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
                return Ok(new ResponseModel<bool> { Success = true, Message = "Error Occured While Purchasing Policy", Data = result });

            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
