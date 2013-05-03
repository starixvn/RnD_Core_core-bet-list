namespace Fanex.PTEngine.BusinessObjects
{
    using System.Collections.Generic;

    public interface IBetTypeGroupSet<T> where T : IBetTypeGroup<BetType>
    {
        bool IsLive { get; set; }

        bool? ShowGroupSetLabel { get; set; }

        IList<T> BetTypeGroups { get; set; }
    }
}