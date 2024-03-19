namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            for (int betTypeId = UUSlotsBetTypes.BlessingOfTu + 1; betTypeId <= UUSlotsBetTypes.HighestBetTypeIdInRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, UUSlotsBetTypes.BlessingOfTu);
            }
        }
    }
}