using PaymentAPI.Models.Responses.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAPI.Models.Responses
{
	public class ApprovePaymentResponse : PaymentResponse
	{

		 
		public Payment Payment { get; set; }
	 
	}
}
