using PaymentAPI.BusinessLogicSingleResponsibility;
using PaymentAPI.Models.Requests;
using PaymentAPI.Models.Responses;
using PaymentAPI.Services.Interfaces;

namespace PaymentAPI.Services.Implementations.Business
{
	public class PaymentService : IPaymentService
	{
		public IPaymentValidatorService PaymentValidatorService { get; }

		public PaymentService(IPaymentValidatorService validatorService)// dependency injection
		{
			PaymentValidatorService = validatorService;
		}

		/// <summary>
		/// This method is for user story 1
		/// </summary>
		/// <param name="createPaymentRequest"></param>
		/// <returns></returns>
		public CreatePaymentResponse CreatePayment(CreatePaymentRequest createPaymentRequest)
		{

			return (CreatePaymentResponse)new CreatePaymentBL(PaymentValidatorService).ExecuteAction(createPaymentRequest);



		}

		/// <summary>
		/// This method is for user story 2
		/// </summary>
		/// <param name="cancelPaymentRequest"></param>
		/// <returns></returns>

		public CancelPaymentResponse CancelPayment(CancelPaymentRequest cancelPaymentRequest)
		{
			return (CancelPaymentResponse)new CancelPaymentBL(PaymentValidatorService).ExecuteAction(cancelPaymentRequest);
		}

		/// <summary>
		/// This method is for user story 3
		/// </summary>
		/// <param name="approvePaymentRequest"></param>
		/// <returns></returns>
		public ApprovePaymentResponse ApprovePayment(ApprovePaymentRequest approvePaymentRequest)
		{
			return (ApprovePaymentResponse)new ApprovePaymentBL(this.PaymentValidatorService).ExecuteAction(approvePaymentRequest);
		}
		/// <summary>
		/// This method is for user story 4
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public ListPaymentResponse ListPayments(ListPaymentsRequest request)
		{
			// request object not used as search criteria not part of the specification

			return (ListPaymentResponse)new ListPaymentsBL(this.PaymentValidatorService).ExecuteAction(request);
		}






	}
}
