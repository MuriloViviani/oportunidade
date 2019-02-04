using System.ServiceModel.Syndication;
using System.Xml;

namespace MainWords.Infra
{
    public class FeedControl
    {
        private readonly string URL = "https://www.minutoseguros.com.br/blog/feed/";
        public SyndicationFeed Feed { get; set; }

        public FeedControl()
        {
            var reader = XmlReader.Create(URL);
            Feed = SyndicationFeed.Load(reader);
        }
    }
}
