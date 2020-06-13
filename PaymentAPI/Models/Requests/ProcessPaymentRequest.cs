using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAPI.Models.Requests
{
	public class ApprovePaymentRequest
	{

		public Guid PaymentId { get; set; }
		
	}
}
