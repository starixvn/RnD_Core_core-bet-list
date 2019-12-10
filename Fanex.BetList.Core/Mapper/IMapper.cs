namespace Fanex.BetList.Core.Mapper
{
    using System.Collections.Generic;

    public interface IMapper
    {
        IDictionary<int, int> MapperData { get; }
    }
}