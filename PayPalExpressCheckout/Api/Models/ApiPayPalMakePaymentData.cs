using System;

namespace PayPalExpressCheckout
{
	public class PayPalMakePaymentData {
		public string intent { get; set; }
		public PayPalMakePaymentRedirectUrls redirect_urls { get; set; }
		public PayPalPayer payer { get; set; }
		public PayPalTransaction[] transactions { get; set; }
	}

	public class PayPalMakePaymentRedirectUrls {
		public string return_url { get; set; }
		public string cancel_url { get; set; }
	}
}

