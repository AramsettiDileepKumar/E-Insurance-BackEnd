using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Entities;
using ModelLayer.RequestDTO.Paymentmodels;
using ModelLayer.ResponseDTO;
using System.Security.Claims;

namespace E_Insurance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class paymentController : ControllerBase
    {
        private readonly IPaymentBL payment;
        public paymentController(IPaymentBL payment)
        {
            this.payment = payment;
        }
        [HttpPost]
        public async Task<IActionResult> Addpayment(PaymentRequest request)
        {
            try
            {
                var CustomerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var result= await payment.AddPayment(request,CustomerId);
                if (result != 0)
                {
                    return CreatedAtAction(nameof(Addpayment),new ResponseModel<string> { Success=true,Message="Payment Added Successfully",Data=null});
                }
                return Ok(new ResponseModel<string> { Success =false, Message = "Error Occured While Adding Payment"});
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
        [HttpGet("getPayment")]
        public async Task<IActionResult> ViewPayment()
        {
            var CustomerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result=await payment.getPayments(CustomerId);
            if (result != null)
            {
                return Ok(new ResponseModel<IEnumerable<PaymentEntity>> { Success = true, Message = "Payment Fetched Successfully", Data = result });
            }
            return Ok(new ResponseModel<string> { Success = false, Message = "Error Occued While Fetching Payment", Data = null });
        }
        [HttpGet("getReceipt")]
        public async Task<IActionResult> GenerateReceipt(int PurchaseId)
        {
            try
            {
                var CustomerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var result = await payment.getReceipt(PurchaseId, CustomerId);
                if (result != null)
                {
                    return Ok(new ResponseModel<PaymentEntity> { Success = true, Message = "Receipt Generated Successfully", Data =result });
                }
                return Ok(new ResponseModel<string> { Success = true, Message = "Error Occued While Generating Receipt", Data = null });
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}
