using System;

namespace PayPalExpressCheckout
{
	public class PayPalAmount {
		public string total { get; set; }
		public string currency { get; set; }
		public PayPalAmountDetails details { get; set; }
	}
		
	public class PayPalAmountDetails {
		public string subtotal { get; set; }
		public string tax { get; set; }
		public string shipping { get; set; }
	}
}

