 
using Microsoft.AspNetCore.Mvc;
using PaymentAPI.Models.Requests;
using PaymentAPI.Models.Responses;
using PaymentAPI.Services.Implementations.Business;

namespace PaymentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {

        private readonly PaymentService paymentService;
        public PaymentController(PaymentService paymentService)
        {
            this.paymentService=paymentService;
        }

        // GET: api/Payment
        [HttpPost]
        public ListPaymentResponse ListPayments( ListPaymentsRequest request)
        {
            return paymentService.ListPayments(request);
        }

        // POST: api/CreatePayment
        [HttpPost]
        public CreatePaymentResponse CreatePayment([FromBody] CreatePaymentRequest request)
        {

            return paymentService.CreatePayment(request);

        }
        // POST: api/CreatePayment
        [HttpPost]
        public CancelPaymentResponse CancelPayment([FromBody] CancelPaymentRequest request)
        {

            return paymentService.CancelPayment(request);

        }
        // POST: api/CreatePayment
        [HttpPost]
        public ApprovePaymentResponse ApprovePayment([FromBody] ApprovePaymentRequest request)
        {

            return paymentService.ApprovePayment(request);

        }
    }
}
