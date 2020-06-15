using PaymentAPI.Models.Responses.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using PaymentAPI.Models.Business;

namespace PaymentAPI.Models.Responses
{
	public class CreatePaymentResponse : PaymentResponse
	{
		public Payment Payment { get; set; }

	}
}
