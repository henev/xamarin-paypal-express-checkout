using System;
using Xamarin.Forms;
using System.Collections.Generic;
using XLabs.Ioc;

namespace PayPalExpressCheckout
{
	public class PayPalWebView : ContentPage
	{
		public PayPalWebView (string url, string accessToken)
		{
			var browser = new WebView ();
			browser.Source = url;

			browser.Navigating += async (sender, e) => {
				var uri = new Uri(e.Url);

				if (uri.Host == "www.google.com") {
					var getValues = new Dictionary<string, string>();
					var query = uri.Query.Substring(1);

					string[] itemArray;
					foreach(var item in query.Split('&')) {
						itemArray = item.Split('=');

						getValues[itemArray[0]] = itemArray[1];
					}

					string executeResonseError = null;

					if (getValues.ContainsKey("PayerID") && getValues.ContainsKey("paymentId")) {

						executeResonseError = await Resolver
							.Resolve<IPayPalApiClient>()
							.ExecuteApprovedPayment(getValues["PayerID"], accessToken, getValues["paymentId"]);	
					}

					if (executeResonseError != null) {
						// display executeResponseError
					}

					Navigation.PopAsync();
				}
			};

			var layout = new ContentView ();
			layout.Content = browser;

			Content = layout;
		}
	}
}

