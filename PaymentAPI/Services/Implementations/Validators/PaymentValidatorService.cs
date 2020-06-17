using PaymentAPI.Data;
using PaymentAPI.Models.Business;
using PaymentAPI.Models.Requests;
using PaymentAPI.Models.Responses;
using PaymentAPI.Services.Interfaces;
using System;
using System.Linq;

namespace PaymentAPI.Services.Implementations.Validators
{
	public class PaymentValidatorService : IPaymentValidatorService
	{

		public PaymentValidatorService()
		{ }

		public CreatePaymentResponse ValidateCreatePayment(CreatePaymentRequest req)
		{
			CreatePaymentResponse response = new CreatePaymentResponse();
			response.IsValid = false;
			if (req.Amount <= 0m || req.PaymentDate == DateTime.MinValue)
				response.ErrorMessages.Add("Amount must be supplied and must be positive number and date must be supplied");

			else if (InMemoryData.CurrentBalance - req.Amount < 0m)
				response.ErrorMessages.Add("Not Enough Balance");
			else
				response.IsValid = true;
			return response;


		}

		public ApprovePaymentResponse ValidateApprovePayment(ApprovePaymentRequest approvePaymentRequest)
		{
			ApprovePaymentResponse response = new ApprovePaymentResponse();

			Payment payment = InMemoryData.Payments.FirstOrDefault(x => x.PaymentId == approvePaymentRequest.PaymentId);
			if (payment == null)
				response.ErrorMessages.Add("Such a payment does not exists!!!!");
			else if (payment.Status == "Pending")
			{
				response.IsValid = true;
				response.Payment = payment;
			}
			else
				response.ErrorMessages.Add("You cannot approve/cancel processed or closed payments");
			return response;

		}

		public CancelPaymentResponse ValidateCancelPayment(CancelPaymentRequest cancelPaymentRequest)
		{
			CancelPaymentResponse response = new CancelPaymentResponse();
			Payment payment = InMemoryData.Payments.FirstOrDefault(x => x.PaymentId == cancelPaymentRequest.PaymentId);
			if (payment == null)
				response.ErrorMessages.Add("No such payment exists!!!??");
			else if (payment.Status != "Pending")
				response.ErrorMessages.Add("Only pending payment can be cancelled");
			else
			{
				response.IsValid = true;
				response.Payment = payment;
			}
			return response;
		}
	}
}
