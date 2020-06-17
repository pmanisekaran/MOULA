using PaymentAPI.Models.Business;
using PaymentAPI.Models.Responses.BaseClasses;

namespace PaymentAPI.Models.Responses
{
	public class CreatePaymentResponse : PaymentResponse
	{
		public Payment Payment { get; set; }

	}
}
