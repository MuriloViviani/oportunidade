using System;

namespace MainWords
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Loading Data...");
            Console.WriteLine("======================================");
            var feedHandler = new FeedReader();

            Console.WriteLine("Number of retrieved itens");
            Console.WriteLine("\t" + feedHandler.PostCount());

            Console.WriteLine("\nInformation about the posts");
            Console.WriteLine("======================================");

            foreach (var feedItem in feedHandler.GetAllContent())
            {
                Console.WriteLine("Header");
                Console.WriteLine("\t" + feedItem.Title);

                Console.WriteLine("\nSummary");
                Console.WriteLine("\t" + feedItem.Summary);

                Console.WriteLine("\nNumber of words in this article -> " + feedItem.SummaryWordCount);

                Console.WriteLine("\nLink for Visualization");
                Console.WriteLine("\t" + feedItem.Link);

                Console.WriteLine("\nMost important words:");
                foreach (var word in feedItem.MostUsedWords)
                {
                    Console.WriteLine("\t" + word.KeyField + " - Apeeared " + word.Count + " time(s);");
                }

                Console.WriteLine("======================================\n\n");
            }

            Console.WriteLine("Press any key to continue...");
            Console.Read();
        }
    }
}
