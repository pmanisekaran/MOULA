using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAPI.Models.Requests
{
	public class ListPaymentsRequest
	{
		// it is place holder for search criteri. Not used s requirement did not ask for.
		public DateTime FromDate { get; set; }
		public DateTime ToDate { get; set; }

	}
}
