namespace Fanex.PTEngine.BusinessObjects
{
    using System.Collections.Generic;

    public interface IBetTypeGroup<T> where T : IBetType
    {
        int? Id { get; set; }

        IList<T> BetTypes { get; set; }
    }
}