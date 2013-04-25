TwinkleLinks
============

Extract URLs from your favorite tweets

Inspired by [@coridrew](https://twitter.com/coridrew/status/327216326584172544), I took this as an opportunity to create a quick-n-dirty proof-of-concept and to explore the Twitter API (thru [Tweetsharp](https://github.com/danielcrenna/tweetsharp)) and familarize myself with OAuth authentication.

Create an application at https://dev.twitter.com/ then input your assigned Consumer Key and Consumer Secret in the appSettings section within Web.config. You'll also need to update the CallbackUrl appropriately as configured with Twitter. (Note: As of this writing, Twitter doesn't allow "localhost" as the domain in your callback URL)

