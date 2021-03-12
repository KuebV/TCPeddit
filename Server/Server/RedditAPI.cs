using Reddit;
using Reddit.Controllers;
using Reddit.Controllers.EventArgs;
using System;
using System.Linq;
using System.Threading;

namespace Server
{
    public class RedditAPI
    {
        public static string getUsername()
        {
            var r = new RedditClient(appId: Config.getAppID(), refreshToken: Config.getRefreshToken(), appSecret: Config.getSecretToken());
            return r.Account.Me.Name;
        }

        public static void getPosts(string Sub)
        {
            var r = new RedditClient(appId: Config.getAppID(), refreshToken: Config.getRefreshToken(), appSecret: Config.getSecretToken());
            var SubReddit = r.Subreddit(name: Sub);
            var risingPosts = SubReddit.Posts.GetHot();
            string[] list = new string[] { };
            foreach (var post in risingPosts)
            {
                list.Append(post.Permalink);
                CC.Message(ConsoleColor.Green, post.Permalink);
                post.i
            }



        }


    }
}
