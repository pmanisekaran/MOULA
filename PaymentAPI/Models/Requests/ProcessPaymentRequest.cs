using System;

namespace PaymentAPI.Models.Requests
{
	public class ApprovePaymentRequest : IRequest
	{

		public Guid PaymentId { get; set; }

	}
}
