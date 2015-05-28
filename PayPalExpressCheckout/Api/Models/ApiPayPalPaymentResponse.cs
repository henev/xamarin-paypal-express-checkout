using System;
using System.Collections.Generic;

namespace PayPalExpressCheckout
{
	public class PayPalPaymentResponse {
		
//		public string Id { get; set; }
//		public string CreatedTime { get; set; }
//		public string UpdatedTime { get; set; }
//		public string State { get; set; }
//		public string Intent { get; set; }
//
//		public PayPalPayer Payer { get; set; }
//		public List<PayPalTransaction> Transaction { get; set; }
		public List<PayPalPaymentLinksResponse> Links { get; set; }

		public string DisplayError { get; set; }
	}

	public class PayPalPaymentLinksResponse {
		public string Href { get; set; }
		public string Rel { get; set; }
		public string Method { get; set; }
	}
}

