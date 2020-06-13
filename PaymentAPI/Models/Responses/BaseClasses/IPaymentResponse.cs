using System.Collections.Generic;

namespace PaymentAPI.Models.Responses.BaseClasses
{
	public interface IPaymentResponse
	{
		List<string> ErrorMessages { get; }
		bool IsValid { get; set; }
	}
}