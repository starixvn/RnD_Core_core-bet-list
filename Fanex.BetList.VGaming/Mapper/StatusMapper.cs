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
                VGamingBetTypes.CasinoHoldemPoker,
                VGamingBetTypes.TexasHoldemPoker,
                VGamingBetTypes.PaiGowPoker,
                VGamingBetTypes.HiLo,
                VGamingBetTypes.Pontoon,
                VGamingBetTypes.ChineseBlackjack
            };

            for (var id = VGamingBetTypes.MinRange + 1; id < VGamingBetTypes.Blackjack; id++)
            {
                MapIdToBaseId(id, VGamingBetTypes.MinRange);
            }

            for (var id = VGamingBetTypes.Blackjack + 1; id < VGamingBetTypes.PromotionPrize; id++)
            {
                MapIdToBaseId(id, brokenGame.Contains(id) ? VGamingBetTypes.Blackjack : VGamingBetTypes.MinRange);
            }

            MapIdToBaseId(VGamingBetTypes.PromotionPrize, VGamingBetTypes.JackpotPrize);
        }
    }
}