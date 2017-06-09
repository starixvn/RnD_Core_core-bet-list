namespace Fanex.BetList.Core.Factories
{
    using Builder;

    /// <summary>
    /// Class BetListExcelFactory.
    /// </summary>
    public class BetListExcelFactory : IBetListFactory<BetListExcelBuilder>
    {
        public BetListExcelBuilder CreateBuilder() => new BetListExcelBuilder();
    }
}