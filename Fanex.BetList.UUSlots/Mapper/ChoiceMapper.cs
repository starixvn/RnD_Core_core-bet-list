namespace Fanex.BetList.Core.Mapper
{
    using Constants;

    public class ChoiceMapper : BaseMapper
    {
        public ChoiceMapper()
        {
            for (int betTypeId = UUSlotsBetTypes.BlessingOfTu + 1; betTypeId <= UUSlotsBetTypes.HighestBetTypeIdInRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, UUSlotsBetTypes.BlessingOfTu);
            }
        }
    }
}