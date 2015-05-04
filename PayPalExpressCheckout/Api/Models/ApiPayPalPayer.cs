using System;

namespace PayPalExpressCheckout
{
	public class PayPalPayer {
		public string payment_method { get; set; }
		public PayPalPayerInfo payer_info { get; set; }
	}

	public class PayPalPayerInfo {
		public string email { get; set; }
		public string first_name { get; set; }
		public string last_name { get; set; }
		public string payer_id { get; set; }
	}
}

