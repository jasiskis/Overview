using WatiN.Core;

namespace LiteMedia.WatinExtension.WebTests
{
    public class LiCollection : BaseElementCollection<Li, LiCollection>
    {
        public LiCollection(DomContainer domContainer, ElementFinder elementFinder) : 
            base(domContainer, elementFinder)
        {
        }

        protected override LiCollection CreateFilteredCollection(ElementFinder elementFinder)
        {
            return new LiCollection(DomContainer, elementFinder);
        }
    }
}
