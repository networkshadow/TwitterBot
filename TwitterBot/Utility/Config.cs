using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;

namespace TwitterBot.Utility
{
    public class Config
    {
        //API Key and Secret
        private static string consumer_key = "";
        private static string consumer_key_secret = "";

        //Access Token and Secret
        private static string access_token = "";
        private static string access_token_secret = "";

        //Callable TwitterService object
        public static TwitterService Service = new TwitterService(consumer_key, consumer_key_secret, access_token, access_token_secret);
    }
}
