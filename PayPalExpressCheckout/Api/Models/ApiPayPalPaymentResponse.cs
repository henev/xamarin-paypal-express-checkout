using System;
using System.Collections.Generic;

namespace PayPalExpressCheckout
{
	public class PayPalPaymentResponse {
//		public string id { get; set; }
//		public string create_time { get; set; }
//		public string update_time { get; set; }
//		public string state { get; set; }
//		public string intent { get; set; }
//
//		public PayPalPayer payer { get; set; }
//		public List<PayPalTransaction> transactions { get; set; }
		public List<PayPalPaymentLinksResponse> links { get; set; }

		public string DisplayError { get; set; }
	}

	public class PayPalPaymentLinksResponse {
		public string href { get; set; }
		public string rel { get; set; }
		public string method { get; set; }
	}
}

