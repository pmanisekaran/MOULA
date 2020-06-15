using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAPI.Models.Requests
{
	public class ListPaymentsRequest :IRequest
	{
		public DateTime FromDate { get; set; }
		public DateTime ToDate { get; set; }

	}
}
