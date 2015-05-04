using System;

namespace PayPalExpressCheckout
{
	public class PayPalGetTokenResponse
	{
//		public string scope { get; set; }
		public string access_token { get; set; }
//		public string token_type { get; set; }
//		public string app_id { get; set; }
//		public int expires_in { get; set; }

		public string DisplayError { get; set; }
	}
}

