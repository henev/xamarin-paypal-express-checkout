Xamarin PayPal Express Checkout
============

About
------------

**Xamarin PayPal Express Checkout** is a sample project build with Xamarin.Forms and has integrated PayPal's Express Checkout functionality using the [PayPal REST API](https://developer.paypal.com/docs/integration/direct/make-your-first-call/). The project targets only iOS and Android but I think it will work fine on Windows Phone too.

Express Checkout process
------------

1. The user clicks the pay with PayPal button
	* Access token is requested and received from the API
	* [Make payment call is sent to the API](https://developer.paypal.com/docs/api/#create-a-payment)
2. An url is received from the make payment call and sent to the webview
3. The user is prompted to login, review and confirm the order in the webview
4. [Execute approved payment call is sent to the API after the user confirms](https://developer.paypal.com/docs/api/#execute-an-approved-paypal-payment)
5. Do whatever you like after the execute payment returns success

How to use
------------

For now the best way to use this project is to change the code in it according to your needs.
Important classes are:

* **Config** - where it is essential to change the client id and the secret. Return and cancel url must be always different, but it doesn't matter really what websites they are targeting. The user never gets to those websites. He is always redirected to the preveous or next view before that.

* **PayPalApiClient** - Here are all the API calls to PayPal. It implements an interface that is placed in the PCL project. This class exists in a linked file that both the iOS and Android project have. I use dependency injection thanks to [Xlabs IOC](https://github.com/XLabs/Xamarin-Forms-Labs/wiki/IOC) so that I can call PayPalApiClient's methods in the PCL. This is done because [RestSharp](https://components.xamarin.com/view/restsharp/) component can't be installed in the PCL and we use [RestSharp as REST client for consuming HTTP APIs](https://components.xamarin.com/view/restsharp/).

* **PayPalWebView** - is used as a webview, where we load the url provided by PayPal for the user to login, review and confirm the order so that the transaction can finish.