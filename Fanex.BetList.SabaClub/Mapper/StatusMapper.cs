namespace Fanex.BetList.Core.Mapper
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Constants;

    public class StatusMapper : BaseMapper
    {
        public StatusMapper()
        {
            var brokenGame = new List<int>
            {
                SabaClubBetTypes.CasinoHoldemPoker,
                SabaClubBetTypes.HiLo,
            };

            for (var id = SabaClubBetTypes.MinRange + 1; id < SabaClubBetTypes.Blackjack; id++)
            {
                MapIdToBaseId(id, SabaClubBetTypes.MinRange);
            }

            for (var id = SabaClubBetTypes.Blackjack + 1; id < SabaClubBetTypes.JackpotContribution; id++)
            {
                MapIdToBaseId(id, brokenGame.Contains(id) ? SabaClubBetTypes.Blackjack : SabaClubBetTypes.MinRange);
            }
        }
    }
}