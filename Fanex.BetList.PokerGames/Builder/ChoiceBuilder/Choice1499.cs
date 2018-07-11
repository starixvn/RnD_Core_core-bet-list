namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using System.Xml.Linq;
    using Entities;
    using Fanex.BetList.Core.Utils;

    public class Choice1499 : Choice1401
    {
        private decimal _gemsBought;
        private decimal _totalGems;

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
            => Template.betTeam = string.Empty;

        protected override void ParseExtraInfo(XDocument extraInfoXml)
        {
            const string gemsBoughtAttributeName = "gb";
            const string totalGemsAttributeName = "tg";

            decimal.TryParse(GetExtraInfo(extraInfoXml, gemsBoughtAttributeName), out _gemsBought);
            decimal.TryParse(GetExtraInfo(extraInfoXml, totalGemsAttributeName), out _totalGems);
        }

        protected override string[] GetMatchInfo(ITicket ticket, ITicketHelper ticketHelper)
            => new[]
            {
                BuildGemsBuyingBlock(ticketHelper),
                BuildTotalGemsBeforeBuyingBlock(ticketHelper),
                BuildTotalGemsAfterBuyingBlock(ticketHelper)
            };

        private string BuildGemsBuyingBlock(ITicketHelper ticketHelper)
        {
            const string gemsResourceId = "Gems";

            var gemsPattern = ticketHelper.GetResourceData(GameInfoRefId, gemsResourceId);

            return string.Format(gemsPattern, ConvertByBetType.Stake(_gemsBought));
        }

        private string BuildTotalGemsBeforeBuyingBlock(ITicketHelper ticketHelper)
        {
            const string gemsBeforeBuyingResourceId = "BeforePurchase";

            var totalGemsBeforeBuying = _totalGems - _gemsBought;
            var gemsBeforeBuyingPattern = ticketHelper.GetResourceData(GameInfoRefId, gemsBeforeBuyingResourceId);

            return string.Format(gemsBeforeBuyingPattern, ConvertByBetType.Stake(totalGemsBeforeBuying));
        }

        private string BuildTotalGemsAfterBuyingBlock(ITicketHelper ticketHelper)
        {
            const string gemsAfterBuyingResourceId = "AfterPurchase";

            var gemsAfterBuyingPattern = ticketHelper.GetResourceData(GameInfoRefId, gemsAfterBuyingResourceId);

            return string.Format(gemsAfterBuyingPattern, ConvertByBetType.Stake(_totalGems));
        }

        protected override void BuildBetTypeName(ref RTFHelper rtfHelper)
            => rtfHelper.RTFRenderer.AddText(Template.BetType.betTypeName);
    }
}