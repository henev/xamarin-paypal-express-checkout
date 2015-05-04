using System;
using System.Threading.Tasks;

namespace PayPalExpressCheckout
{
	public interface IPayPalApiClient
	{
		Task<PayPalGetTokenResponse> GetAccessToken();
		Task<PayPalExecutePaymentResult> MakePayment(string returnUrl, string cancelUrl, string totalPrice, string priceCurrency);
		Task<string> ExecuteApprovedPayment(string payerId, string accessToken, string paymentId);
	}
}

