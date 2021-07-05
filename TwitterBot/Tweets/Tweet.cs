using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;
using TwitterBot.Utility;

namespace TwitterBot.Tweets
{
    public class Tweet
    {
        //Initialize service
        private static TwitterService service = Config.Service;

        /// <summary>
        /// Method for sending tweet to twitter account
        /// </summary>
        /// <param name="_status"></param>
        public void SendTweet(string _status)
        {
            service.SendTweet(new SendTweetOptions { Status = _status }, (tweet, response) =>
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"<{DateTime.Now}> - Tweet Send!");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"<ERROR> " + response.Error.Message);
                    Console.ResetColor();
                }
            });
        }

        /// <summary>
        /// Method to allow class to send tweet with an associated image
        /// </summary>
        /// <param name="_status"></param>
        /// <param name="imageID"></param>
        public void SendTweetWithMedia(string _status, int imageID)
        {

        }
    }
}
