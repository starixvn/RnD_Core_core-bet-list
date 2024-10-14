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
                SabaClubBetTypes.TexasHoldemPoker,
                SabaClubBetTypes.Pontoon,
                SabaClubBetTypes.ChineseBlackjack
            };

            for (var id = SabaClubBetTypes.MinRange + 1; id < SabaClubBetTypes.Blackjack; id++)
            {
                MapIdToBaseId(id, SabaClubBetTypes.MinRange);
            }

            for (var id = SabaClubBetTypes.Blackjack + 1; id < SabaClubBetTypes.JackpotContribution; id++)
            {
                MapIdToBaseId(id, brokenGame.Contains(id) ? SabaClubBetTypes.Blackjack : SabaClubBetTypes.MinRange);
            }

            for (var betTypeId = SabaClub2BetTypes.MinRange; betTypeId <= SabaClub2BetTypes.MaxRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, SabaClubBetTypes.MinRange);
            }
        }
    }
}