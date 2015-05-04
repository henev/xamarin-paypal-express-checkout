using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using XLabs.Ioc;
using XLabs.Platform.Device;

namespace PayPalExpressCheckout.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init ();

			SetIoc ();

			// Code for starting up the Xamarin Test Cloud Agent
			#if ENABLE_TEST_CLOUD
			Xamarin.Calabash.Start();
			#endif

			LoadApplication (new App ());

			return base.FinishedLaunching (app, options);
		}

		private void SetIoc() {
			var resolverContainer = new SimpleContainer();

			resolverContainer.Register<IDevice>(r => AppleDevice.CurrentDevice);
			resolverContainer.RegisterSingle<IPayPalApiClient, PayPalApiClient>();

			Resolver.SetResolver(resolverContainer.GetResolver());
		}
	}
}

