using System;
using System.Collections.Generic;
using PaymentAPI.Models.Business;

namespace PaymentAPI.Data
{
	public static class InMemoryData
	{
		static InMemoryData()
		{
			CurrentBalance = 10000m;
		}

		public static Decimal CurrentBalance { get; set; }

		public static List<Payment> Payments { get; } = new List<Payment>();
	}
}
