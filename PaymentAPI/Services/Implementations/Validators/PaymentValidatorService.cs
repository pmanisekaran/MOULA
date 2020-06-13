using Microsoft.AspNetCore.Authorization.Policy;
using PaymentAPI.Data;
using PaymentAPI.Models;
using PaymentAPI.Models.Requests;
using PaymentAPI.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAPI.Services
{
	public class PaymentValidatorService : IPaymentValidatorService
	{

		public PaymentValidatorService()
		{ }

		public CreatePaymentResponse ValidateCreatePayment(CreatePaymentRequest req)
		{
			CreatePaymentResponse response = new CreatePaymentResponse();
			response.IsValid = false;

			if (InMemoryData.CurrentBalance - req.Amount < 0)
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
				response.ErrorMessages.Add("Such a payment does not exists!!!!");
			return response;

		}

		public CancelPaymentResponse ValidateCancelPayment(CancelPaymentRequest cancelPaymentRequest)
		{
			CancelPaymentResponse response = new CancelPaymentResponse();
			Payment payment = InMemoryData.Payments.FirstOrDefault(x => x.PaymentId == x.PaymentId);
			if (payment == null)
				response.ErrorMessages.Add("No such payment exists!!!??");
			else if (payment.Status != "Pending")
				response.ErrorMessages.Add("Only Pending payment can be processed");
			else
				response.IsValid = true;
			return response;
		}
	}
}
