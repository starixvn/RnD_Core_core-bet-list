namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            for (var betTypeId = Indo4DBetTypes.FourD + 1; betTypeId <= Indo4DBetTypes.MaxRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, Indo4DBetTypes.FourD);
            }
        }
    }
}