using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TweetSharp;

namespace twinklelinks.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            var token = Session["Twitter.AccessToken"];
            var tokenSecret = Session["Twitter.AccessTokenSecret"];

            if (token == null || tokenSecret == null)
                return RedirectToAction("Authorize", "Auth");

            TwitterService service = new TwitterService(ConfigurationManager.AppSettings["TwitterConsumerKey"], ConfigurationManager.AppSettings["TwitterConsumerSecret"]);
            service.AuthenticateWith(token.ToString(), tokenSecret.ToString());
            
            // TODO: Check that authentication succeeded

            TwitterUser user = service.VerifyCredentials(new VerifyCredentialsOptions());
            ViewBag.TwitterUsername = user.ScreenName;

            IEnumerable<TwitterStatus> favorites = service.ListFavoriteTweets(new ListFavoriteTweetsOptions { UserId = user.Id, Count = 20, IncludeEntities = true });
            ViewBag.Favorites = favorites;

            return View();
        }
    }
}
