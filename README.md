Xamarin PayPal Express Checkout
============

About
------------

**Xamarin PayPal Express Checkout** is a sample project build with Xamarin.Forms and has integrated PayPal's Express Checkout functionality using the [PayPal REST API](https://developer.paypal.com/docs/integration/direct/make-your-first-call/). The project targets only iOS and Android but I think it will work fine on Windows Phone too.

Express Checkout process
------------

1. The user clicks the pay with PayPal button
	* Access token is received from the API
	* Make payment call is sent to the API
2. An url is received from the make payment call and sent to the webview
3. The user is prompted to login, review and confirm the order in the webview
4. Execute approved payment call is sent to the API after the user confirms
5. Do whatever you like after the execute payment returns success

The project has a main view with a single button. The button triggers the API calls and after that the user is redirected to a webview (PayPal website) where he is prompted to login, review and confirm the transaction. All transaction information is static.
WebView
API File

How to use
------------