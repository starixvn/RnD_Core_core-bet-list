
namespace Fanex.BetList.Core.Builder.OddsBuilder
{
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Templates;
    using Fanex.BetList.Core.Utils;

    public class Odds3101 : Odds1
    {
        private readonly IList<string> BetChoicesHideOdds = new List<string>
        {
            "1004",
            "1005",
            "1006",
            "1007",
            "1008",
            "1009",
            "1010",
            "1011"
        };
        private const string BetChoiceDataKey = "Choice";

        public override Odds_Template Render(ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById funcGetOddsTypeName)
        {
            if (IsNotShowOdds(ticket))
            {
                Template.odds = "-";
                Template.oddsType = string.Empty;

                return Template;
            }

            return base.Render(ticket, ticketData, funcGetOddsTypeName);
        }

        private bool IsNotShowOdds(ITicket ticket)
        {
            var parsedTransDesc = ticket.ParseTransDesc();
            var betChoice = parsedTransDesc != null && parsedTransDesc.ContainsKey(BetChoiceDataKey) ?
                parsedTransDesc[BetChoiceDataKey].Trim() :
                string.Empty;

            return string.IsNullOrEmpty(ticket.TransDesc) || BetChoicesHideOdds.Any(i => i == betChoice);
        }
    }
}
