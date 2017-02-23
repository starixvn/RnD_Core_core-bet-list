namespace Fanex.BetList.Core.Factories
{
    using Builder;

    /// <summary>
    /// Interface IBetListFactory.
    /// </summary>
    public interface IBetListFactory<T>
    {
        T CreateBuilder();
    }
}