namespace Fanex.BetList.Core.Builder.TransBuilder
{
    public class Trans1201 : Trans1
    {
        private const string VirtualSportsXmlTag = "p5";

        public override string GetExtraInfoTag()
            => VirtualSportsXmlTag;
    }
}
