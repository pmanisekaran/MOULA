﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAPI.Models.Requests
{
	public class CreatePaymentRequest
	{

		public decimal Amount { get; set; }
		public DateTime PaymentDate { get; set; }
	}
}
