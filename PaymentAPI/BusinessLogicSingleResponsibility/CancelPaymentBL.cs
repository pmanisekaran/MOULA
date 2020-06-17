using PaymentAPI.Data;
using PaymentAPI.Models.Requests;
using PaymentAPI.Models.Responses;
using PaymentAPI.Models.Responses.BaseClasses;
using PaymentAPI.Services.Interfaces;

namespace PaymentAPI.BusinessLogicSingleResponsibility
{
	public class CancelPaymentBL : IPaymentAction
	{
		public IPaymentValidatorService ValidatorService { get; }

		public CancelPaymentBL(IPaymentValidatorService validatorService)
		{
			this.ValidatorService = validatorService;
		}

		public IPaymentResponse ExecuteAction(IRequest req)
		{
			CancelPaymentResponse response = ValidatorService.ValidateCancelPayment((CancelPaymentRequest)req);
			if (response.IsValid)
			{
				response.Payment.Status = "Closed";
				//	InMemoryData.CurrentBalance -= response.Payment.Amount;
				response.Payment.RunningBalance = InMemoryData.CurrentBalance;
				response.Payment.CancellationMessage = ((CancelPaymentRequest)req).CancelReason;
				return response;
			}
			return response;
		}
	}
}
