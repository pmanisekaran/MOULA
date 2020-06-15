using PaymentAPI.Models.Requests;
using PaymentAPI.Models.Responses;

namespace PaymentAPI.Services.Interfaces
{
	public interface IPaymentService
	{
	
		CreatePaymentResponse CreatePayment(CreatePaymentRequest createPaymentRequest);
		CancelPaymentResponse CancelPayment(CancelPaymentRequest cancelPaymentRequest);
		ApprovePaymentResponse ApprovePayment(ApprovePaymentRequest approvePaymentRequest);
		ListPaymentResponse ListPayments(ListPaymentsRequest request);
	
	}
}