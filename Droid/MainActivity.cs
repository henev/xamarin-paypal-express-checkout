using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using XLabs.Ioc;
using XLabs.Platform.Device;

namespace PayPalExpressCheckout.Droid
{
	[Activity (Label = "PayPalExpressCheckout.Droid", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetIoc ();

			global::Xamarin.Forms.Forms.Init (this, bundle);

			LoadApplication (new App ());
		}

		private void SetIoc() {
			var resolverContainer = new SimpleContainer();

			resolverContainer.Register<IDevice>(r => AndroidDevice.CurrentDevice);
			resolverContainer.RegisterSingle<IPayPalApiClient, PayPalApiClient>();

			Resolver.SetResolver(resolverContainer.GetResolver());
		}
	}
}

