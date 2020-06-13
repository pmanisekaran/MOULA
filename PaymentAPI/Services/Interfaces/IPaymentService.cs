using PaymentAPI.Models.Requests;
using PaymentAPI.Models.Responses;

namespace PaymentAPI.Services.Implementations
{
	public interface IPaymentService
	{
		CancelPaymentResponse CancelPayment(CancelPaymentRequest cancelPaymentRequest);
		CreatePaymentResponse CreatePayment(CreatePaymentRequest createPaymentRequest);
		ListPaymentResponse ListPayments(ListPaymentsRequest request);
		ApprovePaymentResponse ApprovePayment(ApprovePaymentRequest approvePaymentRequest);
	}
}