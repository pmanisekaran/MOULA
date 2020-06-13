using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAPI.Models.Responses.BaseClasses
{
	public class PaymentResponse : IPaymentResponse
	{

		public bool IsValid { get; set; }
		public List<string> ErrorMessages { get; } = new List<string>();
	}
}
