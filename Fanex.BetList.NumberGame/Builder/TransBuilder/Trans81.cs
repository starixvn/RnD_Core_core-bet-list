namespace Fanex.BetList.Core.Builder.TransBuilder
{
    public class Trans81 : Trans1
    {
        private const string NumberGameXmlTag = "p3";

        public override string GetExtraInfoTag()
            => NumberGameXmlTag;
    }
}
