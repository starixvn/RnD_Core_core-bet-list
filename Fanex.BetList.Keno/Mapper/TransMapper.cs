namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class TransMapper : BaseMapper
    {
        public TransMapper()
        {
            for (int betTypeId = KenoBetTypes.BaseBetTypeId + 1; betTypeId <= KenoBetTypes.MaxBetTypeIdRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, KenoBetTypes.BaseBetTypeId);
            }
        }
    }
}