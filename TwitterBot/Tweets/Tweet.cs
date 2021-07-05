using System;
using System.Collections.Generic;
using System.IO;
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
        private static int imageID = 0;

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
        /// 
        /// User must pass a list of the images' paths from wherever they are
        /// being currently stored on their local system
        /// 
        /// </summary>
        /// <param name="_status"></param>
        /// <param name="imageID"></param>
        public void SendTweetWithMedia(string _status, List<string> imagesList)
        {
            using(var stream = new FileStream(imagesList[imageID], FileMode.Open))
            {
                service.SendTweetWithMedia(new SendTweetWithMediaOptions
                {
                    Status = _status,
                    Images = new Dictionary<string, Stream> { { imagesList[imageID], stream} }
                });

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"<{DateTime.Now}> - Tweet Sent!");
                Console.ResetColor();


                if ((imageID + 1) == imagesList.Count)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"<BOT> - End of Image Array");
                    Console.ResetColor();
                }
                else
                {
                    imageID++;
                }
            }
        }
    }
}
