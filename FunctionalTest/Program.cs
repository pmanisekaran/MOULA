using Newtonsoft.Json;
using PaymentAPI.Models.Requests;
using PaymentAPI.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FunctionalTest
{
	class Program
	{

		private const string baseUrl = @"http://localhost:60770/payment/";

		private static List<KeyValuePair<Guid, string>> newlyAddedPayments = new List<KeyValuePair<Guid, string>>();


		static async Task Main(string[] args)
		{


			Console.WriteLine("Press any key to start functional testing of API");
			Console.ReadKey();
			var cleardCnt = await ClearPayments();
			Console.WriteLine("Clreaed " + cleardCnt);
			newlyAddedPayments.Clear();
			string res;
			for (int i = -5; i < 0; i++)
			{
				res = await AddPayment(100, DateTime.Today.AddDays(i));
				PrintResult(res);
			}

			// List payment and check all 6 apyments are there and in pending state
			res = await ListPayments();
			PrintResult(res);

			// aprrove first 3 payment and cancel next three
			int j = 1;
			foreach (var p in newlyAddedPayments)
			{
				if (j <= 3)
					res = await ApprovePayment(p.Key);
				else
					res = await CancelPayment(p.Key);
				PrintResult(res);
				j++;

			}

			// List payment and check all 6 apyments are there and in pending state
			res = await ListPayments();
			PrintResult(res);



			Console.WriteLine("******Press any key to close***********");
			Console.ReadKey();


		}

		private static void PrintResult(string res)
		{

			Console.WriteLine(JsonConvert.DeserializeObject(res));
			Console.WriteLine("******End of test***********");
		}

		static async Task<string> ClearPayments()
		{
			Console.WriteLine();
			Console.WriteLine("******add payment test***********");
			Console.WriteLine();
			Uri u = new Uri(baseUrl + "clearpayments");
			var response = string.Empty;
			using (var client = new HttpClient())
			{

				HttpResponseMessage res = await client.GetAsync(u);
				if (res.IsSuccessStatusCode)
				{
					int reply = await res.Content.ReadAsAsync<int>();

					return reply.ToString();
				}
			}
			return response;
		}

		static async Task<string> AddPayment(decimal amount, DateTime dateTime)
		{
			Console.WriteLine();
			Console.WriteLine("******add payment test***********");
			Console.WriteLine();
			Uri u = new Uri(baseUrl + "createpayment");

			var response = string.Empty;
			using (var client = new HttpClient())
			{
				CreatePaymentRequest payload = new CreatePaymentRequest();
				payload.Amount = amount;
				payload.PaymentDate = dateTime;
				HttpResponseMessage res = await client.PostAsJsonAsync(u, payload);
				if (res.IsSuccessStatusCode)
				{
					CreatePaymentResponse reply = await res.Content.ReadAsAsync<CreatePaymentResponse>();
					newlyAddedPayments.Add(new KeyValuePair<Guid, string>(reply.Payment.PaymentId, reply.Payment.Status));
					return JsonConvert.SerializeObject(reply);
				}
			}
			return response;
		}

		static async Task<string> ListPayments()
		{
			Console.WriteLine();
			Console.WriteLine("******Listing payments test***********");
			Console.WriteLine();
			Uri u = new Uri(baseUrl + "listpayments");
			var response = string.Empty;
			using (var client = new HttpClient())
			{
				ListPaymentsRequest payload = new ListPaymentsRequest();
				HttpResponseMessage res = await client.PostAsJsonAsync(u, payload);
				if (res.IsSuccessStatusCode)
				{
					ListPaymentResponse reply = await res.Content.ReadAsAsync<ListPaymentResponse>();

					foreach (var p in newlyAddedPayments)
					{
						if (!reply.Payments.Any(x => x.PaymentId == p.Key && x.Status == p.Value))
							Console.WriteLine("Failed to create payment with right status");
					}

					return JsonConvert.SerializeObject(reply);
				}
			}
			return response;
		}

		static async Task<string> ApprovePayment(Guid paymentId)
		{
			Console.WriteLine();
			Console.WriteLine("******Approve payment test***********");
			Console.WriteLine();
			Uri u = new Uri(baseUrl + "approvepayment");

			var response = string.Empty;
			using (var client = new HttpClient())
			{
				ApprovePaymentRequest payload = new ApprovePaymentRequest();
				payload.PaymentId = paymentId;
				HttpResponseMessage res = await client.PostAsJsonAsync(u, payload);
				if (res.IsSuccessStatusCode)
				{
					ApprovePaymentResponse reply = await res.Content.ReadAsAsync<ApprovePaymentResponse>();
					if (reply.Payment.PaymentId == paymentId && reply.Payment.Status == "Processed")
						Console.WriteLine("Approve payment failed for  payment id " + paymentId);
					return JsonConvert.SerializeObject(reply);
				}
			}
			return response;
		}
		static async Task<string> CancelPayment(Guid paymentId)
		{
			Console.WriteLine();
			Console.WriteLine("******Cacel payment test***********");
			Console.WriteLine();
			Uri u = new Uri(baseUrl + "cancelpayment");

			var response = string.Empty;
			using (var client = new HttpClient())
			{
				CancelPaymentRequest payload = new CancelPaymentRequest();
				payload.PaymentId = paymentId;
				HttpResponseMessage res = await client.PostAsJsonAsync(u, payload);
				if (res.IsSuccessStatusCode)
				{
					CancelPaymentResponse reply = await res.Content.ReadAsAsync<CancelPaymentResponse>();
					if (reply.Payment.PaymentId == paymentId && reply.Payment.Status == "Closed")
						Console.WriteLine("Cancel payment failed for  payment id " + paymentId);
					return JsonConvert.SerializeObject(reply);
				}
			}
			return response;
		}



	}
}
