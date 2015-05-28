using System;
using System.Threading.Tasks;

namespace PayPalExpressCheckout
{
	public interface IPayPalApiClient
	{
		Task<PayPalGetTokenResponse> GetAccessToken();
		Task<PayPalExecutePaymentResult> MakePayment();
		Task<string> ExecuteApprovedPayment(string payerId, string accessToken, string paymentId);
	}
}

