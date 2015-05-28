using System;
using Xamarin.Forms;
using System.Collections.Generic;
using XLabs.Ioc;

namespace PayPalExpressCheckout {
	
	public class PayPalWebView : ContentPage {

		WebView browser;
		string accessToken;

		public PayPalWebView (string url, string token) {
			accessToken = token;
			browser = new WebView ();
			browser.Source = url;

			browser.Navigating += HandleNavigating;

			var layout = new ContentView ();
			layout.Content = browser;

			Content = layout;
		}

		async void HandleNavigating(object sender, WebNavigationEventArgs e) {
			if (!String.IsNullOrWhiteSpace (e.Url)) {
				// convert url to uri which gives us additional functionality and more flexability over the url
				var uri = new Uri (e.Url);

				if (uri.Host == Config.ReturnHost) {
					// prevent navigating to execute again and to come back here
					browser.Navigating -= HandleNavigating;

					// get the query string GET parameters in dictionary key-value format
					var queryItems = GetQueryStringKeyValues (uri);

					string executeResonseError = null;

					if (queryItems.ContainsKey ("PayerID") && queryItems.ContainsKey ("paymentId")) {

						// execute the approved payment
						executeResonseError = await Resolver
							.Resolve<IPayPalApiClient> ()
							.ExecuteApprovedPayment (queryItems ["PayerID"], accessToken, queryItems ["paymentId"]);	
					}

					// check if the api call returned any errors
					if (executeResonseError != null) {
						// display executeResonseError

						// navigate back to the previous page
						await Navigation.PopAsync();
					} else {
						// display success

						// navigate back to the previous page
						await Navigation.PopAsync();
					}
				// check if the webview is navigating to the cancel url -- user canceled the purchase
				} else if (uri.Host == Config.CancelHost) {
					// prevent navigating to execute again and to come back here
					browser.Navigating -= HandleNavigating;
					// navigate back to the previous page
					await Navigation.PopAsync ();
				}
			} else {
				// display error message

				// navigate back to the previous page
				await Navigation.PopAsync ();
			}
		}

		Dictionary<string, string> GetQueryStringKeyValues(Uri uri) {
			var queryItems = new Dictionary<string, string>();
			// remove the dollar sign (?) from the beginning
			var query = uri.Query.Substring(1);

			string[] itemArray;
			foreach(var item in query.Split('&')) {
				itemArray = item.Split('=');

				queryItems[itemArray[0]] = itemArray[1];
			}

			return queryItems;
		}
	}
}

