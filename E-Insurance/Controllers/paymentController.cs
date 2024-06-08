using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.RequestDTO.Paymentmodels;
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
                return CreatedAtAction(nameof(Addpayment),result);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
        [HttpGet("getPayment")]
        public async Task<IActionResult> ViewPayment()
        {
            var CustomerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result=await payment.getPayments(CustomerId);
            return Ok(result);
        }
        [HttpGet("getReceipt")]
        public async Task<IActionResult> GenerateReceipt(int PolicyId)
        {
            var CustomerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result=await payment.getReceipt(PolicyId,CustomerId);
            return Ok(result);
        }
    }
}
