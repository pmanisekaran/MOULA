using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAPI.Models
{
	public class Payment
	{
		private Guid paymentId = Guid.NewGuid();

		public Guid PaymentId { get; }
		public string Status { get; set; }
		public decimal Amount { get; set; }
		public DateTime PaymentDate { get; set; }
		public decimal RunningBalance { get; set; }
		public string CancellationMessage{ get; set; }

	}
}
