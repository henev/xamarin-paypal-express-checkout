using System;

namespace PayPalExpressCheckout
{
	public class PayPalItem {
		public string quantity { get; set; }
		public string name { get; set; }
		public string price { get; set; }
		public string currency { get; set; }
//		public string sku { get; set; }
		public string description { get; set; }
		public string tax { get; set; }
	}
}

