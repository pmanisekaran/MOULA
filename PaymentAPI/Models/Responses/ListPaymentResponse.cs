using PaymentAPI.Models.Business;
using PaymentAPI.Models.Responses.BaseClasses;
using System.Collections.Generic;

namespace PaymentAPI.Models.Responses
{
	public class ListPaymentResponse : PaymentResponse
	{
		public List<Payment> Payments { get; } = new List<Payment>();

	}
}
