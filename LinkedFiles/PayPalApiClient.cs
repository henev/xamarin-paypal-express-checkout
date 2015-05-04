using System;
using RestSharp;
using System.Threading.Tasks;
using System.Net;
using System.Diagnostics;
using System.Linq;

namespace PayPalExpressCheckout
{
	public class PayPalApiClient : IPayPalApiClient
	{
		private RestClient restClient { get; set; }

		// constructor
		public PayPalApiClient () {
			restClient = new RestClient ("https://api.sandbox.paypal.com/v1");
		}

		// MAKE CALLS ASYNC

		// Get access (bearer) token from paypal
		public async Task<PayPalGetTokenResponse> GetAccessToken () {
			var restRequest = new RestRequest ("/oauth2/token", Method.POST);

			// Add headers
			restRequest.AddHeader ("Accept", "application/json");
			restRequest.AddHeader ("Accept-Language", "en_US");

			// Application credentials
			string clientId = "ASpkpVUsuaE73F-3glCVNVZO31eJgWPmv0DFDUoPAozHy9qPX_HKQQ_igzvLHFe7Vz5pvr66VNItscFy";
			string secret = "EIuzZ70XfeTS_YXvoMDpu86QyrW0QtxwM6s8NvoTdVdPUL3ToimgPdiMrDTrQvCTuNewvLKhS0mwaAHU";

			// Make Authorization header
			restClient.Authenticator = new HttpBasicAuthenticator(clientId, secret);

			// add data to send
			restRequest.AddParameter ("grant_type", "client_credentials");

			var response = restClient.Execute<PayPalGetTokenResponse> (restRequest);

			response.Data.DisplayError = CheckResponseStatus (response, HttpStatusCode.OK);

			return response.Data;
		}

		// Make a payment
		// Should do validation of the returned data
		public async Task<PayPalExecutePaymentResult> MakePayment (string returnUrl, 
			string cancelUrl, 
			string totalPrice, 
			string priceCurrency) {

			var accessTokenData = await GetAccessToken();
			var executePaymentResult = new PayPalExecutePaymentResult ();

			if (accessTokenData.DisplayError == null) {
				var restRequest = new RestRequest ("/payments/payment", Method.POST);

				// Add headers
				restRequest.AddHeader ("Content-Type", "application/json");
				restRequest.AddHeader ("Authorization", "Bearer " + accessTokenData.access_token);

				// Add data to send
				restRequest.RequestFormat = DataFormat.Json;
				restRequest.AddBody (new PayPalMakePaymentData() {
					intent = "sale",
					redirect_urls = new PayPalMakePaymentRedirectUrls() {
						return_url = returnUrl,
						cancel_url = cancelUrl
					},
					payer = new PayPalPayer() {
						payment_method = "paypal"
					},
					transactions = new [] {
						new PayPalTransaction() {
							amount = new PayPalAmount() {
								total = totalPrice,
								currency = priceCurrency
							},
							item_list = new PayPalItemList() {
								items = new [] {
									new PayPalItem() {
										quantity = "1",
										name = "Booking reservation",
										price = "50.00",
										currency = "USD",
										description = "Booking reservation at ABC hotel at 24/03/2015 from 1pm to 4pm."
									}
								}
							}
						}
					}
				});

				var response = restClient.Execute<PayPalPaymentResponse> (restRequest);

				executePaymentResult.DisplayError = CheckResponseStatus (response, HttpStatusCode.Created);

				if (executePaymentResult.DisplayError == null) {
					// Get the approval url from the links provided by the response
					var links = from link in response.Data.links
								where link.rel == "approval_url"
								select link.href;

					if (!String.IsNullOrEmpty(links.First())) {
						executePaymentResult.Url = links.First ();
						executePaymentResult.AccessToken = accessTokenData.access_token;

						// Send the approval url along with the access token the paypal webview
						return executePaymentResult;
					} else {
						executePaymentResult.DisplayError = "Something went wrong. Please try again.";
					}
				}
			} else {
				executePaymentResult.DisplayError = accessTokenData.DisplayError;
			}

			return executePaymentResult;
		}

		// Validate a payment using webview for user to enter his credentials and to review the purchase
		public async Task<string> ExecuteApprovedPayment(string payerId, string accessToken, string paymentId) {
			var restRequest = new RestRequest (String.Format("/payments/payment/{0}/execute", paymentId), Method.POST);

			// Add headers
			restRequest.AddHeader ("Content-Type", "application/json");
			restRequest.AddHeader ("Authorization", "Bearer " + accessToken);

			// Add data to send
			restRequest.RequestFormat = DataFormat.Json;
			restRequest.AddBody (new {
				payer_id = payerId
			});

			var response = restClient.Execute<PayPalPaymentResponse> (restRequest);

			return CheckResponseStatus (response, HttpStatusCode.OK);
		}

		private string CheckResponseStatus(IRestResponse response, HttpStatusCode validStatusCode) {
			if (response.ResponseStatus == ResponseStatus.Completed) {
				if (response.StatusCode != validStatusCode) {
					// something went wrong
					Debug.WriteLine ("{0} -- {1}", response.StatusCode, response.StatusDescription);

					return "Something went wrong. Please try again.";
				} else {

					return null;
				}
			} else {

				// something went wrong. please check your internet connection
				return "Something went wrong. Please check your internet connection.";
			}
		}
	}
}

