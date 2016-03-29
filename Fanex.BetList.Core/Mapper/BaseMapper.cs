using System;
using System.Collections.Generic;

namespace Fanex.BetList.Core.Mapper
{
    public class BaseMapper : IMapper
    {
        public IDictionary<int, int> MapperData { get; set; } = new Dictionary<int, int>();

        protected virtual void MapIdToBaseId(int id, int baseId) {
            MapperData.Add(id, baseId);
        }
    }
}