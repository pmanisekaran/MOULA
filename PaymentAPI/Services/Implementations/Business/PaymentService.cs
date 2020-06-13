using Microsoft.Extensions.Logging;
using PaymentAPI.Data;
using PaymentAPI.Models;
using PaymentAPI.Models.Requests;
using PaymentAPI.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace PaymentAPI.Services.Implementations
{
	public class PaymentService : IPaymentService
	{
		private IPaymentValidatorService paymentValidatorService;
		public PaymentService(IPaymentValidatorService validatorService)
		{
			paymentValidatorService = validatorService;
		}
		public CreatePaymentResponse CreatePayment(CreatePaymentRequest createPaymentRequest)
		{
			var response = paymentValidatorService.ValidateCreatePayment(createPaymentRequest);
			if (response.IsValid)
			{
				Payment payment = new Payment();
				payment.Amount = Convert.ToDecimal(createPaymentRequest.Amount);
				payment.CancellationMessage = string.Empty;
				payment.PaymentDate = Convert.ToDateTime(createPaymentRequest.PaymentDate);
				payment.RunningBalance = InMemoryData.CurrentBalance;
				payment.Status = "Pending";// normally I would have enum rather than string. 

				InMemoryData.Payments.Add(payment);
				response.Payment = payment;
				return response;
			}
			else
				return response;
		}

		public ApprovePaymentResponse ApprovePayment(ApprovePaymentRequest approvePaymentRequest)
		{
			ApprovePaymentResponse response = paymentValidatorService.ValidateApprovePayment(approvePaymentRequest);
			if (response.IsValid)
			{
				response.Payment.Status = "Processed";
				InMemoryData.CurrentBalance -= response.Payment.Amount;
				response.Payment.RunningBalance = InMemoryData.CurrentBalance;
				return response;
			}
			else
				return response;
		}

		public CancelPaymentResponse CancelPayment(CancelPaymentRequest cancelPaymentRequest)
		{
			CancelPaymentResponse response = paymentValidatorService.ValidateCancelPayment(cancelPaymentRequest);
			if (response.IsValid)
			{
				response.Payment.Status = "Closed";
				InMemoryData.CurrentBalance -= response.Payment.Amount;
				response.Payment.RunningBalance = InMemoryData.CurrentBalance;
				return response;
			}
			else
				return response;
		}

		public ListPaymentResponse ListPayments(ListPaymentsRequest request)
		{
			// request object not used as search criteria not part of the specification

			ListPaymentResponse response = new ListPaymentResponse();
			response.Payments.AddRange(InMemoryData.Payments.OrderByDescending(x => x.PaymentDate).ToList());
			return response;
		}





	}
}
