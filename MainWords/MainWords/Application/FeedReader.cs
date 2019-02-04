using MainWords.Infra;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel.Syndication;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace MainWords
{
    public class FeedReader : IFeedReader
    {
        private List<Post> posts;

        public FeedReader()
        {
            FeedControl control = new FeedControl();

            posts = ProcessContent(control.Feed);
        }

        /// <summary>
        /// Get all of the content Info that was retrieved form the Feed URL
        /// </summary>
        /// <param name="postOrder">The index that the post you want is in</param>
        /// <returns>A single post</returns>
        public Post GetContentInfo(int postOrder)
        {
            if (posts.Count() > postOrder)
                return posts[postOrder];
            else
                return new Post() { Title = "Post not found" };
        }

        /// <summary>
        /// Return every post that was gathered form the Feed
        /// </summary>
        /// <returns>List of posts</returns>
        public List<Post> GetAllContent()
        {
            return posts;
        }

        /// <summary>
        /// Return the total of itens that was retrieved from the WebSite
        /// </summary>
        /// <returns></returns>
        public int PostCount()
        {
            return posts.Count();
        }

        /// <summary>
        /// This action will process all of the content that is allocated in the FeedControl ("Database") that is going to be requested by the user
        /// </summary>
        /// <param name="posts"></param>
        /// <returns></returns>
        private List<Post> ProcessContent(SyndicationFeed posts)
        {
            var processedPosts = new List<Post>();
            Post cleanedPost;

            foreach (var feedItem in posts.Items)
            {
                var cleanSummary = CleanTextHTMLTags(feedItem.Summary.Text);
                cleanSummary = CleanTextSpecialCharacters(cleanSummary);

                cleanedPost = new Post
                {
                    Title = feedItem.Title.Text,
                    Summary = cleanSummary,
                    SummaryWordCount = cleanSummary.Split(' ').Count(),
                    Link = feedItem.Id,
                    MostUsedWords = new List<Words>()
                };

                cleanedPost.MostUsedWords.AddRange(CleanTextPrepositions(cleanSummary).Trim().Split(' ').GroupBy(x => x)
                    .Select(x => new Words
                    {
                        KeyField = x.Key,
                        Count = x.Count()
                    })
                    .OrderByDescending(x => x.Count)
                    .Take(10));

                processedPosts.Add(cleanedPost);
            }

            return processedPosts;
        }

        #region RegexFunctions
        /// <summary>
        /// This action will remove every ptoposition from the inputted text
        /// </summary>
        /// <param name="text">The text to be cleaned</param>
        /// <returns>Returns the text cleaned</returns>
        private string CleanTextPrepositions(string text)
        {
            // Clen all prepositions
            var regex = new Regex(@"( [A-Za-zÈÉÃÁéêóôãõá]?[A-Za-zÈÉÃÁéêóôãõá]?[A-Za-zÈÉÃÁéêóôãõá]?[A-Za-zÈÉÃÁéêóôãõá]?[,.?]? )");
            text = regex.Replace(text, " ");
            // Double run to remove possible missing propositions
            text = regex.Replace(text, " ");

            //Clean Blank Spaces
            regex = new Regex(@"\s+");
            text = regex.Replace(text, " ");

            return text;
        }

        /// <summary>
        /// This action will remove all HTML tags from the inputted text
        /// </summary>
        /// <param name="text">The text to get all the TAGs Removed</param>
        /// <returns>The text eithout any HTML Tag</returns>
        private string CleanTextHTMLTags(string text)
        {
            //Clean all HTML Tags
            var regex = new Regex("<.*?>");
            text = regex.Replace(text, " ");

            return text;
        }

        /// <summary>
        /// This actions will clean every dot, comma and special tags, like line breaking and others from the inserted text
        /// </summary>
        /// <param name="text">Text to be cleaned</param>
        /// <returns>Cleaned Text</returns>
        private string CleanTextSpecialCharacters(string text)
        {
            //Clean all dots and commas from text
            var regex = new Regex("[.,]");
            text = regex.Replace(text, " ");

            //Clean all scaped characters
            regex = new Regex(@"\G(.+)[\t\u007c](.+)\r?\n");
            text = regex.Replace(text, " ");

            return text;
        }
        #endregion
    }
}
