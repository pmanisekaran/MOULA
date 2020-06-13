﻿using PaymentAPI.Models.Requests;
using PaymentAPI.Models.Responses;

namespace PaymentAPI.Services
{
	public interface IPaymentValidatorService
	{
		CancelPaymentResponse ValidateCancelPayment(CancelPaymentRequest cancelPaymentRequest);
		CreatePaymentResponse ValidateCreatePayment(CreatePaymentRequest req);
		ApprovePaymentResponse ValidateApprovePayment(ApprovePaymentRequest approvePaymentRequest);
	}
}