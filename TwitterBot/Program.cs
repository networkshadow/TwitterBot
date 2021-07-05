using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;
using TwitterBot.Tweets;
using TwitterBot.Utility;

namespace TwitterBot
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"<{DateTime.Now}> - Twitter Bot Started");
            Console.ResetColor();
        }
    }
}
