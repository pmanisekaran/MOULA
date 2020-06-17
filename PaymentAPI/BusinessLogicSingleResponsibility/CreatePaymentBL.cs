using PaymentAPI.Data;
using PaymentAPI.Models.Business;
using PaymentAPI.Models.Requests;
using PaymentAPI.Models.Responses;
using PaymentAPI.Models.Responses.BaseClasses;
using PaymentAPI.Services.Interfaces;
using System;

namespace PaymentAPI.BusinessLogicSingleResponsibility
{
	public class CreatePaymentBL : IPaymentAction
	{
		public IPaymentValidatorService ValidatorService { get; }

		public CreatePaymentBL(IPaymentValidatorService validatorService)
		{
			this.ValidatorService = validatorService;
		}

		/// <summary>
		/// This method is for user story 1
		/// </summary>
		/// <param name="createPaymentRequest"></param>
		/// <returns></returns>
		public IPaymentResponse ExecuteAction(IRequest createPaymentRequest)
		{
			CreatePaymentRequest request = (CreatePaymentRequest)createPaymentRequest;
			CreatePaymentResponse response = (CreatePaymentResponse)ValidatorService.ValidateCreatePayment(request);
			if (response.IsValid)
			{

				Payment payment = new Payment
				{
					PaymentId = Guid.NewGuid(),
					Amount = Convert.ToDecimal(request.Amount),
					CancellationMessage = string.Empty,
					PaymentDate = Convert.ToDateTime(request.PaymentDate),
					RunningBalance = InMemoryData.CurrentBalance,
					Status = "Pending"// normally I would have enum rather than string. 
				};

				InMemoryData.Payments.Add(payment);
				response.Payment = payment;
				return response;
			}
			return response;
		}


	}
}
