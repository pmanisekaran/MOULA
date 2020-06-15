using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaymentAPI.Models.Requests;
using PaymentAPI.Models.Responses.BaseClasses;

namespace PaymentAPI.BusinessLogicSingleResponsibility
{
	public interface IPaymentAction
	{
		IPaymentResponse ExecuteAction(IRequest requestObject);
	}

}
