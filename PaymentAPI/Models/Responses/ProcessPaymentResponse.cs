using PaymentAPI.Models.Business;
using PaymentAPI.Models.Responses.BaseClasses;

namespace PaymentAPI.Models.Responses
{
	public class ApprovePaymentResponse : PaymentResponse
	{


		public Payment Payment { get; set; }

	}
}
