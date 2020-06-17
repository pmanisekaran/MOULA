using System.Collections.Generic;

namespace PaymentAPI.Models.Responses.BaseClasses
{
	public class PaymentResponse : IPaymentResponse
	{

		public bool IsValid { get; set; }
		public List<string> ErrorMessages { get; } = new List<string>();
	}
}
