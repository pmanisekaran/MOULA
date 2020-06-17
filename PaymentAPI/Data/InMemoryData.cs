using PaymentAPI.Models.Business;
using System;
using System.Collections.Generic;

namespace PaymentAPI.Data
{
	public static class InMemoryData
	{
		static InMemoryData()
		{
			CurrentBalance = 10000m;

			//PaymentService ps = new PaymentService(new PaymentValidatorService());
			//ps.CreatePayment(new Models.Requests.CreatePaymentRequest() { Amount = 99, PaymentDate = DateTime.Now });
			//ps.CreatePayment(new Models.Requests.CreatePaymentRequest() { Amount = 99, PaymentDate = DateTime.Now });
			//ps.CreatePayment(new Models.Requests.CreatePaymentRequest() { Amount = 99, PaymentDate = DateTime.Now });
		}

		public static Decimal CurrentBalance { get; set; }

		public static List<Payment> Payments { get; } = new List<Payment>();
	}
}
