using System;
using Xamarin.Forms;
using XLabs.Ioc;
using System.Threading.Tasks;

namespace PayPalExpressCheckout
{
	public class MainPage : ContentPage
	{
		public MainPage ()
		{
			Title = "PayPal";

			var button = new Button () {
				Text = "Pay with PayPal Express Checkout!"
			};

			button.Clicked += (sender, e) => Task.Factory.StartNew (() => {
				button.IsEnabled = false;

				Resolver
					.Resolve<IPayPalApiClient> ()
					.MakePayment ("http://www.google.com", "http://www.google.com", "50.00", "USD")
					.ContinueWith((r) => {
						var result = r.Result;

						Device.BeginInvokeOnMainThread(() => {
							button.IsEnabled = true;

							if (result.DisplayError == null) {
								Navigation.PushAsync (new PayPalWebView (result.Url, result.AccessToken));
							} else {
								// display executePaymentData.DisplayError
							}
						});
					});
			});

			var layout = new StackLayout ();
			layout.VerticalOptions = LayoutOptions.Center;
			layout.Children.Add (button);

			Content = layout;
		}
	}
}

