namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            for (var id = VGamingBetTypes.MinRange + 1; id < VGamingBetTypes.Blackjack; id++)
            {
                MapIdToBaseId(id, VGamingBetTypes.MinRange);
            }

            for (var id = VGamingBetTypes.Blackjack + 1; id < VGamingBetTypes.JackpotContribution; id++)
            {
                MapIdToBaseId(id, VGamingBetTypes.MinRange);
            }
        }
    }
}