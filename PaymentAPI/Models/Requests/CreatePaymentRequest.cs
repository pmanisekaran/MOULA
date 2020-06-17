using System;

namespace PaymentAPI.Models.Requests
{
	public class CreatePaymentRequest : IRequest
	{

		public decimal Amount { get; set; }
		public DateTime PaymentDate { get; set; }
	}


}
