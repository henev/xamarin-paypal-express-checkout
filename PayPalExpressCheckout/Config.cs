using System;

namespace PayPalExpressCheckout {
	
	public static class Config {

		static AppEnvironment AppEnvironment = AppEnvironment.Development;

		// Base Api url
		const string DevelopmentApiUrl = "https://api.sandbox.paypal.com/v1";
		const string ProductionApiUrl = "https://api.paypal.com/v1";

		// Client Id
		const string DevelopmentApiClientId = "ASpkpVUsuaE73F-3glCVNVZO31eJgWPmv0DFDUoPAozHy9qPX_HKQQ_igzvLHFe7Vz5pvr66VNItscFy";
		const string ProductionApiClientId = "";

		// Secret
		const string DevelopmentApiSecret = "EIuzZ70XfeTS_YXvoMDpu86QyrW0QtxwM6s8NvoTdVdPUL3ToimgPdiMrDTrQvCTuNewvLKhS0mwaAHU";
		const string ProductionApiSecret = "";

		public const string ReturnUrl = "http://www.google.com/";
		public const string CancelUrl = "http://www.google.com/";

		public static string ReturnHost = new Uri(Config.ReturnUrl).Host;
		public static string CancelHost = new Uri(Config.CancelUrl).Host;

		// PayPal Api Base Url
		public static string ApiUrl {
			get {
				if (AppEnvironment == AppEnvironment.Development) {

					return DevelopmentApiUrl;
				} else if (AppEnvironment == AppEnvironment.Production) {

					return ProductionApiUrl;
				}

				throw new NotImplementedException ();
			}
		}

		// PayPal Api Client Id
		public static string ApiClientId {
			get {
				if (AppEnvironment == AppEnvironment.Development) {

					return DevelopmentApiClientId;
				} else if (AppEnvironment == AppEnvironment.Production) {

					return ProductionApiClientId;
				}

				throw new NotImplementedException ();
			}
		}

		// PayPal Api Secret
		public static string ApiSecret {
			get {
				if (AppEnvironment == AppEnvironment.Development) {

					return DevelopmentApiSecret;
				} else if (AppEnvironment == AppEnvironment.Production) {

					return ProductionApiSecret;
				}

				throw new NotImplementedException ();
			}
		}
	}

	public enum AppEnvironment {
		Development,
		Production
	}
}

