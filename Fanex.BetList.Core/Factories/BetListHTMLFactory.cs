namespace Fanex.BetList.Core.Factories
{
    using Builder;

    /// <summary>
    /// Class BetListHTMLFactory.
    /// </summary>
    public class BetListHTMLFactory : IBetListFactory<BetListHTMLBuilder>
    {
        public BetListHTMLBuilder CreateBuilder() => new BetListHTMLBuilder();
    }
}