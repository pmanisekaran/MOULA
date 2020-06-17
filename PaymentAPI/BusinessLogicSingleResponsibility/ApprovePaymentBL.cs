using PaymentAPI.Data;
using PaymentAPI.Models.Requests;
using PaymentAPI.Models.Responses;
using PaymentAPI.Models.Responses.BaseClasses;
using PaymentAPI.Services.Interfaces;

namespace PaymentAPI.BusinessLogicSingleResponsibility
{
	public class ApprovePaymentBL : IPaymentAction
	{

		public IPaymentValidatorService ValidatorService { get; }

		public ApprovePaymentBL(IPaymentValidatorService validatorService)
		{
			this.ValidatorService = validatorService;
		}

		public IPaymentResponse ExecuteAction(IRequest approvePaymentRequest)
		{
			ApprovePaymentResponse response =
				ValidatorService.ValidateApprovePayment((ApprovePaymentRequest)approvePaymentRequest);
			if (response.IsValid)
			{
				response.Payment.Status = "Processed";
				InMemoryData.CurrentBalance -= response.Payment.Amount;
				response.Payment.RunningBalance = InMemoryData.CurrentBalance;
				return response;
			}

			return response;
		}
	}
}
