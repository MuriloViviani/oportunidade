using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MainWords.Tests
{
    [TestClass()]
    public class FeedReaderTests
    {
        [TestMethod()]
        [Timeout(2000)]
        public void GetContentInfo_ContentReturning_ReturnValue()
        {
            var feedHandler = new FeedReader();
            var result = feedHandler.GetContentInfo(1);
            
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void GetContentInfo_ContentOutOfRange_ReturnWarn()
        {
            var feedHandler = new FeedReader();
            var result = feedHandler.GetContentInfo(100);

            var expected = new Post()
            {
                Title = "Post not found"
            };

            Assert.AreEqual(result.Title , expected.Title);
        }
    }
}