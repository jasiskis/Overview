using WatiN.Core;
using WatiN.Core.Native;

namespace LiteMedia.WatinExtension.WebTests
{
    [ElementTag("li")]
    public class Li : ElementContainer<Li>
    {
        public Li(DomContainer domContainer, INativeElement nativeElement) : base(domContainer, nativeElement)
        {
        }

        public Li(DomContainer domContainer, ElementFinder finder) : base(domContainer, finder)
        {
        }
    }
}
