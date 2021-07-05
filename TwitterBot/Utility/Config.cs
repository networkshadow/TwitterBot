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
        private static string consumer_key = "ISM76ikl4dIdnkPpMJH9Z2Jk7";
        private static string consumer_key_secret = "ZA2UsNw11OsZXeBNtlHcirHMhtRGaEIsWiz1oprmSnlfSp6WVu";

        //Access Token and Secret
        private static string access_token = "26886074-buqiYzy9EnCVlZoTxtOQzEGim7bzFu40A4v2mbCvR";
        private static string access_token_secret = "P1nCBAxZaMyOC0RWsmvk96MfXOAKiXk1uYipSPNM8uGD0";

        //Callable TwitterService object
        public static TwitterService Service { get; } = new TwitterService(consumer_key, consumer_key_secret, access_token, access_token_secret);
    }
}
