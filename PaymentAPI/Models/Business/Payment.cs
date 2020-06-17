using System;

namespace PaymentAPI.Models.Business
{
	public class Payment
	{

		public Guid PaymentId { get; set; }
		public string Status { get; set; }
		public decimal Amount { get; set; }
		public DateTime PaymentDate { get; set; }
		public decimal RunningBalance { get; set; }
		public string CancellationMessage { get; set; }

	}
}
