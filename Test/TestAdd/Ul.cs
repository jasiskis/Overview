using WatiN.Core;
using WatiN.Core.Native;

namespace LiteMedia.WatinExtension.WebTests
{
    [ElementTag("ul")]
    public class Ul : ElementContainer<Ul>
    {
        public Ul(DomContainer domContainer, INativeElement nativeElement) : base(domContainer, nativeElement)
        {
        }

        public Ul(DomContainer domContainer, ElementFinder elementFinder) : base(domContainer, elementFinder)
        {
        }

        public LiCollection Items
        {
            get
            {
                return new LiCollection(DomContainer, CreateElementFinder<Li>(
                    delegate(INativeElement nativeElement)
                {
                    return nativeElement.Children;
                }, null));
            }
        }
    }
}
