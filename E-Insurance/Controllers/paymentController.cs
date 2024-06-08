using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.RequestDTO.Paymentmodels;

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
                var result= await payment.AddPayment(request);
                return CreatedAtAction(nameof(Addpayment),result);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}
