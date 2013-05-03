namespace Fanex.PTEngine.BusinessObjects
{
    using System.Collections.Generic;

    public interface ISportType<T> where T : IBetTypeGroupSet<BetTypeGroup>
    {
        int Id { get; set; }

        string Name { get; set; }

        string Key { get; set; }

        string ResourceKey { get; set; }

        bool? Collapsed { get; set; }

        IList<T> BetTypeGroupSets { get; set; }
    }
}