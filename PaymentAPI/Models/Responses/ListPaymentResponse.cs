using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAPI.Models.Responses
{
	public class ListPaymentResponse
	{

		public bool IsValid { get; set; }
		public List<Payment> Payments { get; set; }
		public List<string> ErrorMessages { get; }
	}
}
