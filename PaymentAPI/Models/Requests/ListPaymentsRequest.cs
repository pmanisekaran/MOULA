using System;

namespace PaymentAPI.Models.Requests
{
	public class ListPaymentsRequest : IRequest
	{
		public DateTime FromDate { get; set; }
		public DateTime ToDate { get; set; }

	}
}
