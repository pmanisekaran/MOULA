using NUnit.Framework;
using PaymentAPI.Data;
using PaymentAPI.Models.Requests;
using PaymentAPI.Models.Responses;
using PaymentAPI.Services.Implementations.Business;
using PaymentAPI.Services.Implementations.Validators;
using System;

namespace NUnitTestPaymentsAPI
{
	public class PaymentServiceTests
	{
		[SetUp]
		public void Setup()
		{
		}

		#region private Methods
		private CreatePaymentResponse CreatePaymentResponse(decimal amount, DateTime paymentDate)
		{
			CreatePaymentRequest request = new CreatePaymentRequest { Amount = amount, PaymentDate = paymentDate };
			PaymentValidatorService validatorService = new PaymentValidatorService();
			PaymentService paymentService = new PaymentService(validatorService);
			CreatePaymentResponse response = paymentService.CreatePayment(request);
			return response;
		}

		private void AssertForCreatePaymentTests(CreatePaymentResponse response, decimal initialBalance, string errMsg)
		{
			Assert.IsFalse(response.IsValid);
			Assert.IsNull(response.Payment);
			Assert.IsTrue(response.ErrorMessages.Count > 0);
			Assert.IsTrue(InMemoryData.CurrentBalance == initialBalance);
			Assert.IsTrue(response.ErrorMessages[0] == errMsg);
		}

		#endregion


		[Test]
		public void CreatePaymentTest_NotEnoughBalance()
		{
			decimal initialBalance = InMemoryData.CurrentBalance;
			CreatePaymentResponse response = CreatePaymentResponse(100001m, DateTime.Now);
			AssertForCreatePaymentTests(response, initialBalance, "Not Enough Balance");
		}


		[Test]
		public void CreatePaymentTest_FailFor_AmountNotSupplied()
		{
			decimal initialBalance = InMemoryData.CurrentBalance;
			CreatePaymentResponse response = CreatePaymentResponse(0m, DateTime.Now);
			AssertForCreatePaymentTests(response, initialBalance, "Amount must be supplied and must be positive number and date must be supplied");

		}
		[Test]
		public void CreatePaymentTest_FailFor_DateNotSupplied()
		{
			decimal initialBalance = InMemoryData.CurrentBalance;
			CreatePaymentResponse response = CreatePaymentResponse(10m, DateTime.MinValue);
			AssertForCreatePaymentTests(response, initialBalance, "Amount must be supplied and must be positive number and date must be supplied");

		}

		[Test]
		public void CreatePaymentTest_Validate_status()
		{
			CreatePaymentResponse response = CreatePaymentResponse(10m, DateTime.Now);
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
				response?.ErrorMessages.ForEach(x => msg += x + delimiter);
				Assert.Fail(msg);

			}
		}
		[Test]
		public void CreatePaymentTest_Validate_Approve_Status_Balance_Check()
		{

			decimal initialBalance = InMemoryData.CurrentBalance;
			CreatePaymentRequest request = new CreatePaymentRequest { Amount = 10, PaymentDate = DateTime.Now };
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
		[Test]
		public void CreatePaymentTest_Validate_Cancel_Status_Balance_And_Cancel_Reason_Check()
		{

			decimal initialBalance = InMemoryData.CurrentBalance;
			CreatePaymentRequest request = new CreatePaymentRequest { Amount = 10, PaymentDate = DateTime.Now };
			PaymentValidatorService validatorService = new PaymentValidatorService();
			PaymentService paymentService = new PaymentService(validatorService);
			CreatePaymentResponse response = paymentService.CreatePayment(request);
			if (response.IsValid && response.Payment != null
				&& response.Payment.RunningBalance == initialBalance
				&& response.ErrorMessages.Count == 0
				&& response.Payment.Status == "Pending")
			{
				CancelPaymentRequest cancelPaymentRequest = new CancelPaymentRequest
				{
					PaymentId = response.Payment.PaymentId,
					CancelReason = "I am testing if can cancel works or not"
				};
				CancelPaymentResponse cancelPaymentResponse = paymentService.CancelPayment(cancelPaymentRequest);
				if (cancelPaymentResponse.IsValid)
				{
					Assert.IsTrue(cancelPaymentResponse.Payment.Status == "Closed");
					Assert.IsTrue(cancelPaymentResponse.Payment.CancellationMessage == cancelPaymentRequest.CancelReason);

					Assert.IsTrue(cancelPaymentResponse.Payment.RunningBalance == initialBalance);
					Assert.IsTrue(cancelPaymentResponse.Payment.Amount == request.Amount);
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

		[Test]
		public void CreatePaymentTest_Disallow_Cancel_On_Closed_Payments()
		{

			decimal initialBalance = InMemoryData.CurrentBalance;
			CreatePaymentRequest request = new CreatePaymentRequest { Amount = 10, PaymentDate = DateTime.Now };
			PaymentValidatorService validatorService = new PaymentValidatorService();
			PaymentService paymentService = new PaymentService(validatorService);
			CreatePaymentResponse response = paymentService.CreatePayment(request);
			if (response.IsValid && response.Payment != null
				&& response.Payment.RunningBalance == initialBalance
				&& response.ErrorMessages.Count == 0
				&& response.Payment.Status == "Pending")
			{
				CancelPaymentRequest cancelPaymentRequest = new CancelPaymentRequest
				{
					PaymentId = response.Payment.PaymentId
				};
				CancelPaymentResponse cancelPaymentResponse = paymentService.CancelPayment(cancelPaymentRequest);
				if (cancelPaymentResponse.IsValid)
				{
					CancelPaymentRequest tryCancelClosedPayment = new CancelPaymentRequest();
					tryCancelClosedPayment.PaymentId = cancelPaymentResponse.Payment.PaymentId;
					CancelPaymentResponse disAllowedAction = paymentService.CancelPayment(tryCancelClosedPayment);
					Assert.IsFalse(disAllowedAction.IsValid);
					Assert.IsTrue(disAllowedAction.ErrorMessages.Count == 1);
					Assert.IsTrue(disAllowedAction.ErrorMessages[0] == "Only pending payment can be cancelled");

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

		[Test]
		public void CreatePaymentTest_Disallow_Cancel_On_Processed_Payments()
		{

			decimal initialBalance = InMemoryData.CurrentBalance;
			CreatePaymentRequest request = new CreatePaymentRequest { Amount = 10, PaymentDate = DateTime.Now };
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
					CancelPaymentRequest tryCancelClosedPayment = new CancelPaymentRequest();
					tryCancelClosedPayment.PaymentId = approvePaymentResponse.Payment.PaymentId;
					CancelPaymentResponse disAllowedAction = paymentService.CancelPayment(tryCancelClosedPayment);
					Assert.IsFalse(disAllowedAction.IsValid);
					Assert.IsTrue(disAllowedAction.ErrorMessages.Count == 1);
					Assert.IsTrue(disAllowedAction.ErrorMessages[0] == "Only pending payment can be cancelled");

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