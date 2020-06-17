
using Microsoft.AspNetCore.Mvc;
using PaymentAPI.Data;
using PaymentAPI.Models.Requests;
using PaymentAPI.Models.Responses;
using PaymentAPI.Services.Interfaces;

namespace PaymentAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {

        private readonly IPaymentService paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            this.paymentService = paymentService;
        }
        [HttpGet]
        [Route("List")]
        public ListPaymentResponse List()
        {
            return this.ListPayments(new ListPaymentsRequest());
        }

        /// <summary>
        /// Utility function needed for functinal API testing
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("ClearPayments")]

        public int ClearPayments()
        {
            int i = InMemoryData.Payments.Count;
            InMemoryData.Payments.Clear();
            return i;
        }

        [HttpPost]
        [Route("ListPayments")]
        public ListPaymentResponse ListPayments(ListPaymentsRequest request)
        {
            return paymentService.ListPayments(request);
        }

        // POST: api/CreatePayment
        [HttpPost]
        [Route("CreatePayment")]
        public CreatePaymentResponse CreatePayment([FromBody] CreatePaymentRequest request)
        {

            return paymentService.CreatePayment(request);

        }
        // POST: api/CreatePayment
        [HttpPost]
        [Route("CancelPayment")]
        public CancelPaymentResponse CancelPayment([FromBody] CancelPaymentRequest request)
        {

            return paymentService.CancelPayment(request);

        }
        // POST: api/CreatePayment
        [HttpPost]
        [Route("ApprovePayment")]
        public ApprovePaymentResponse ApprovePayment([FromBody] ApprovePaymentRequest request)
        {

            return paymentService.ApprovePayment(request);

        }
    }
}
