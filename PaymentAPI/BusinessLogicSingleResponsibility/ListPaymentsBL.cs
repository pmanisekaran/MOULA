using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaymentAPI.Data;
using PaymentAPI.Models.Requests;
using PaymentAPI.Models.Responses;
using PaymentAPI.Models.Responses.BaseClasses;
using PaymentAPI.Services.Interfaces;

namespace PaymentAPI.BusinessLogicSingleResponsibility
{
	public class ListPaymentsBL :IPaymentAction
	{
		public ListPaymentsBL(IPaymentValidatorService validatorService)
		{
			this.ValidatorService = validatorService;
		}

		public IPaymentValidatorService ValidatorService { get; }

		public IPaymentResponse ExecuteAction(IRequest request)
		{
			ListPaymentResponse response = new ListPaymentResponse();
			var res = InMemoryData.Payments.OrderByDescending(x => x.PaymentDate).ToList();
			response.Payments.AddRange(res);
			return response;
		}
	}
}
