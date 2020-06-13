using NUnit.Framework;
using PaymentAPI.Data;
using PaymentAPI.Models.Requests;
using PaymentAPI.Models.Responses;
using PaymentAPI.Services;
using PaymentAPI.Services.Implementations;
using System;
using System.Linq;

namespace NUnitTestPaymentsAPI
{
	public class PaymentServiceTests
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void CreatePaymentTest_NotEnoutBalance()
		{
			CreatePaymentRequest request = new CreatePaymentRequest();
			request.Amount = 100000000;
			request.PaymentDate = DateTime.Now;
			PaymentValidatorService validatorService = new PaymentValidatorService();
			PaymentService paymentService = new PaymentService(validatorService);
			CreatePaymentResponse response = paymentService.CreatePayment(request);
			Assert.IsFalse(response.IsValid);
			Assert.IsNull(response.Payment);
			Assert.IsTrue(response.ErrorMessages.Count > 0);
			Assert.IsTrue(InMemoryData.CurrentBalance == 10000m);
			Assert.IsTrue(response.ErrorMessages[0] == "Not Enough Balance");
		}
		[Test]
		public void CreatePaymentTest_Validate_status()
		{
			CreatePaymentRequest request = new CreatePaymentRequest();
			request.Amount = 10;
			request.PaymentDate = DateTime.Now;
			PaymentValidatorService validatorService = new PaymentValidatorService();
			PaymentService paymentService = new PaymentService(validatorService);
			CreatePaymentResponse response = paymentService.CreatePayment(request);
			if (response != null && response.IsValid && response.Payment != null
				&& response.Payment.RunningBalance == InMemoryData.CurrentBalance
				&& response.ErrorMessages.Count == 0
				&& response.Payment.Status == "Pending")
			{
				Assert.IsTrue(response.IsValid);
			}
			else
			{

				string msg = string.Empty;
				string delimiter = Environment.NewLine;
				response.ErrorMessages.ForEach(x => msg += x + delimiter);
				Assert.Fail(msg);

			}
		}
		[Test]
		public void CreatePaymentTest_Validate_Approve_Status_Balance_Check()
		{

			decimal initialBalance = InMemoryData.CurrentBalance;
			CreatePaymentRequest request = new CreatePaymentRequest();
			request.Amount = 10;
			request.PaymentDate = DateTime.Now;
			PaymentValidatorService validatorService = new PaymentValidatorService();
			PaymentService paymentService = new PaymentService(validatorService);
			CreatePaymentResponse response = paymentService.CreatePayment(request);
			if (response.IsValid && response.Payment != null
				&& response.Payment.RunningBalance == initialBalance
				&& response.ErrorMessages.Count == 0
				&& response.Payment.Status == "Pending")
			{
				ApprovePaymentRequest approvePaymentRequest = new ApprovePaymentRequest();
				approvePaymentRequest.PaymentId = response.Payment.PaymentId;
				ApprovePaymentResponse approvePaymentResponse = paymentService.ApprovePayment(approvePaymentRequest);
				if (approvePaymentResponse.IsValid)
				{
					Assert.IsTrue(approvePaymentResponse.Payment.Status == "Processed");
					Assert.IsTrue(approvePaymentResponse.Payment.RunningBalance == (initialBalance - request.Amount));
					Assert.IsTrue(approvePaymentResponse.Payment.Amount == request.Amount);
				}

			}
			else
			{

				string msg = string.Empty;
				string delimiter = Environment.NewLine;
				response.ErrorMessages.ForEach(x => msg += x + delimiter);
				Assert.Fail(msg);

			}
		}
	}
}