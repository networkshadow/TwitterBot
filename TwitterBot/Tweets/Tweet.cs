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

        private static List<long> currentIDs = new List<long>();

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
                    Console.WriteLine($"<{DateTime.Now}> - Tweet Sent: {_status}");
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

        /// <summary>
        /// Replies to tweets when they contain a particular word or phrase.
        /// </summary>
        public void ReplyToTweets()
        {
            var count = 0;
            List<TwitterStatus> allTweets = GetAllTweetsFromHome(10).ToList();
            var total = allTweets.Count;

            var findTweetContains = "Trump";

            

            foreach (var tweet in allTweets)
            {
                if (tweet.Text.Contains(findTweetContains))
                {
                    var isValidToReply = true;
                    foreach(var id in currentIDs)
                    {
                        if(id == tweet.Id)
                        {
                            isValidToReply = false;
                        }
                    }

                    if (isValidToReply)
                    {
                        var tweetString = "What a clown that man is. (Sent Courtesy an Automated Reply Bot searching for Tweets with T*U*P)";

                        service.SendTweet(new SendTweetOptions
                        {
                            Status = tweetString,
                            InReplyToStatusId = tweet.Id
                        });

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"<{DateTime.Now}> - Replying to Tweet: {tweet.Text}");
                        Console.WriteLine($"<{DateTime.Now}> - Replied with: {tweetString}");
                        Console.ResetColor();
                        count--;
                    }

                    currentIDs.Add(tweet.Id);
                }

                count++;
            }

            if(count >= total)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"<{DateTime.Now}> - No tweets found with '{findTweetContains}'");
                Console.ResetColor();
            }

            allTweets.Clear();
        }

        /// <summary>
        /// Collects all tweets on the user's home page
        /// </summary>
        /// <returns>Tweets from user's home page</returns>
        private List<TwitterStatus> GetAllTweetsFromHome(int numTweets)
        {
            var currentTweets = service.ListTweetsOnHomeTimeline(new ListTweetsOnHomeTimelineOptions
            {
                Count = numTweets
            }).ToList();

            return currentTweets;
        }
    }
}
