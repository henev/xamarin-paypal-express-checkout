using System;
using Xamarin.Forms;
using XLabs.Ioc;

namespace PayPalExpressCheckout {
	
	public class MainPage : ContentPage {

		Button paypalButton;

		public MainPage () {
			Title = "PayPal";

			paypalButton = new Button {
				Text = "Pay with PayPal Express Checkout!"
			};

			paypalButton.Clicked += HandlePayPalExpressCheckoutButtonClicked;

			var layout = new StackLayout ();
			layout.VerticalOptions = LayoutOptions.Center;
			layout.Children.Add (paypalButton);

			Content = layout;
		}

		async void HandlePayPalExpressCheckoutButtonClicked(object sender, EventArgs e) {
			Device.BeginInvokeOnMainThread( () => paypalButton.IsEnabled = false );

			await Resolver
				.Resolve<IPayPalApiClient>()
				.MakePayment()
				.ContinueWith((r) => {
					var result = r.Result;

					Device.BeginInvokeOnMainThread(() => {
						paypalButton.IsEnabled = true;

						if (result.DisplayError == null) {
							Navigation.PushAsync (new PayPalWebView (result.Url, result.AccessToken));
						} else {
							// display executePaymentData.DisplayError
						}
					});
				});
		}
	}
}

