using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainWords
{
    public interface IFeedReader
    {
        Post GetContentInfo(int postOrder);
        List<Post> GetAllContent();
        int PostCount();
    }
}
