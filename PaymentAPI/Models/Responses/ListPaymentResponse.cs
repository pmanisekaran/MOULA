using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaymentAPI.Models.Business;
using PaymentAPI.Models.Responses.BaseClasses;

namespace PaymentAPI.Models.Responses
{
	public class ListPaymentResponse : PaymentResponse
	{
		public List<Payment> Payments { get; } = new List<Payment>();
	 
	}
}
