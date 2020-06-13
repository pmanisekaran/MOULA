using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAPI.Models.Requests
{
	public class CancelPaymentRequest
	{
		public Guid PaymentId { get; set; }
	}
}
