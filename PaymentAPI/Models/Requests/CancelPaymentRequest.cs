using System;

namespace PaymentAPI.Models.Requests
{
	public class CancelPaymentRequest :IRequest
	{
		public Guid PaymentId { get; set; }
		public string CancelReason { get; set; }
	}
}
