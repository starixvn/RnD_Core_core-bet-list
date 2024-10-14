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
                VGamingRange1BetTypes.CasinoHoldemPoker,
                VGamingRange1BetTypes.TexasHoldemPoker,
                VGamingRange1BetTypes.PaiGowPoker,
                VGamingRange1BetTypes.HiLo,
                VGamingRange1BetTypes.Pontoon,
                VGamingRange1BetTypes.ChineseBlackjack
            };

            for (var id = VGamingRange1BetTypes.MinRange + 1; id < VGamingRange1BetTypes.Blackjack; id++)
            {
                MapIdToBaseId(id, VGamingRange1BetTypes.MinRange);
            }

            for (var id = VGamingRange1BetTypes.Blackjack + 1; id < VGamingRange1BetTypes.PromotionPrize; id++)
            {
                MapIdToBaseId(id, brokenGame.Contains(id) ? VGamingRange1BetTypes.Blackjack : VGamingRange1BetTypes.MinRange);
            }

            MapIdToBaseId(VGamingRange1BetTypes.PromotionPrize, VGamingRange1BetTypes.JackpotPrize);

            for (var betTypeId = VGamingRange2BetTypes.MinRange; betTypeId <= VGamingRange2BetTypes.MaxRange; betTypeId++)
            {
                MapIdToBaseId(betTypeId, VGamingRange1BetTypes.MinRange);
            }
        }
    }
}