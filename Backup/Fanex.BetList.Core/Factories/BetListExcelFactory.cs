namespace Fanex.BetList.Core.Factories
{
    using Builder;

    /// <summary>
    /// Class BetListExcelFactory.
    /// </summary>
    public class BetListExcelFactory : IBetListFactory
    {
        public BetListExcelBuilder CreateBuilder()
        {
            var builder = new BetListExcelBuilder();
            return builder;
        }
    }
}