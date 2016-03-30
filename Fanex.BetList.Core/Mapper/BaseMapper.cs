namespace Fanex.BetList.Core.Mapper
{
    using System.Collections.Generic;

    public class BaseMapper : IMapper
    {
        public IDictionary<int, int> MapperData { get; set; } = new Dictionary<int, int>();

        protected void MapIdToBaseId(int id, int baseId)
        {
            MapperData.Add(id, baseId);
        }
    }
}