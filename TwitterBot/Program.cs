using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TweetSharp;
using TwitterBot.Tweets;
using TwitterBot.Utility;

namespace TwitterBot
{
    class Program
    {
        private static List<string> starWarsQuotes = new List<string>
        {
            "Try not. Do or do not. There is no try.",
            "Your eyes can deceive you; don't trust them.",
            "Who's the more foolish: the fool or the fool who follows him?",
            "Your focus determines your reality.",
            "No longer certain that one ever does win a war, I am.",
            "Train yourself to let go of everything you fear to lose."
        };
        static void Main(string[] args)
        {
            Banner();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"<{DateTime.Now}> - Twitter Bot Started");
            Console.ResetColor();

            Thread autoTweet = new Thread(AutoTweet);
            Thread autoReply = new Thread(AutoReply);
            autoTweet.Start();
            autoReply.Start();

            Console.ReadLine();
        }

        public static void AutoTweet()
        {
            var flag = true;
            var random = new Random();
            var timeout = new TimeSpan(0, 13, 45);//(hours, minutes, seconds)

            while (flag)
            {
                try
                {
                    var tweet = new Tweet();
                    tweet.SendTweet(starWarsQuotes[random.Next(0, starWarsQuotes.Count)]);
                    Thread.Sleep(timeout);
                }
                catch (Exception e)
                {
                    flag = false;
                    Console.WriteLine($"<{DateTime.Now}> - {e.Message}");
                }                
            }
        }

        public static void AutoReply()
        {
            var flag = true;
            var timeout = new TimeSpan(0, 1, 10);//(hours, minutes, seconds)

            while (flag)
            {
                try
                {
                    var tweet = new Tweet();
                    tweet.ReplyToTweets();
                    Thread.Sleep(timeout);
                }
                catch(Exception e)
                {
                    flag = false;
                    Console.WriteLine($"<{DateTime.Now}> - {e.Message}");
                }
            }
        }

        public static void Banner()
        {
            Console.WriteLine("###############################");
            Console.WriteLine("########TwitterBot#############");
            Console.WriteLine("########Version 1.0############");
            Console.WriteLine("########By: NetworkShadow######");
            Console.WriteLine("###############################");
        }
    }
}
