using System.Collections.Generic;

namespace MainWords
{
    public class Post
    {
        public List<Words> MostUsedWords { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Summary { get; set; }
        public int SummaryWordCount { get; set; }
    }
}