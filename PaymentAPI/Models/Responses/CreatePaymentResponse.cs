using PaymentAPI.Models.Responses.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace PaymentAPI.Models.Responses
{
	public class CreatePaymentResponse : PaymentResponse
	{
		 

		public Payment Payment { get; set; }

	}
}
