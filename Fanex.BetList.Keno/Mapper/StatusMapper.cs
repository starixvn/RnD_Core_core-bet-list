namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            for (int betTypeId = KenoBetTypes.BaseBetTypeId + 1; betTypeId <= KenoBetTypes.MaxBetTypeIdRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, KenoBetTypes.BaseBetTypeId);
            }
        }
    }
}