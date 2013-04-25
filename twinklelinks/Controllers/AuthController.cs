using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TweetSharp;

namespace twinklelinks.Controllers
{
    public class AuthController : Controller
    {
        // Based on samples taken from https://github.com/danielcrenna/tweetsharp

        public ActionResult Authorize()
        {
            // Step 1 - Retrieve an OAuth Request Token
            TwitterService service = new TwitterService(ConfigurationManager.AppSettings["TwitterConsumerKey"], ConfigurationManager.AppSettings["TwitterConsumerSecret"]);

            // This is the registered callback URL
            OAuthRequestToken requestToken = service.GetRequestToken(ConfigurationManager.AppSettings["TwitterCallbackUrl"]);

            // Step 2 - Redirect to the OAuth Authorization URL
            Uri uri = service.GetAuthorizationUri(requestToken);
            return new RedirectResult(uri.ToString(), false /*permanent*/);
        }

        // This URL is registered as the application's callback at http://dev.twitter.com
        public ActionResult AuthorizeCallback(string oauth_token, string oauth_verifier)
        {
            var requestToken = new OAuthRequestToken { Token = oauth_token, OAuthCallbackConfirmed = true };

            TwitterService service = new TwitterService(ConfigurationManager.AppSettings["TwitterConsumerKey"], ConfigurationManager.AppSettings["TwitterConsumerSecret"]);
            OAuthAccessToken accessToken = service.GetAccessToken(requestToken, oauth_verifier.ToString());

            Session["Twitter.AccessToken"] = accessToken.Token;
            Session["Twitter.AccessTokenSecret"] = accessToken.TokenSecret;

            return RedirectToAction("Index", "Home");
        }
    }
}
