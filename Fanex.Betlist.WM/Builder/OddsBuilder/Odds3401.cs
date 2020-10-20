
namespace Fanex.BetList.Core.Builder.OddsBuilder
{
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Templates;
    using Fanex.BetList.Core.Utils;

    public class Odds3401 : Odds1
    {
        private const string BetChoiceDataKey = "Choice";

        private readonly IList<string> BetChoicesHideOdds = new List<string>
        {
            "14002",
            "14004",
            "14006"
        };       

        public override Odds_Template Render(ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById funcGetOddsTypeName)
        {
            if (IsHiddenOdds(ticket))
            {
                Template.odds = "-";
                Template.oddsType = string.Empty;

                return Template;
            }

            return base.Render(ticket, ticketData, funcGetOddsTypeName);
        }

        private bool IsHiddenOdds(ITicket ticket)
        {
            var parsedTransDesc = ticket.ParseTransDesc();
            var betChoice = parsedTransDesc != null && parsedTransDesc.ContainsKey(BetChoiceDataKey) ?
                parsedTransDesc[BetChoiceDataKey].Trim() :
                string.Empty;

            return string.IsNullOrEmpty(ticket.TransDesc) || BetChoicesHideOdds.Any(i => i == betChoice);
        }
    }
}
