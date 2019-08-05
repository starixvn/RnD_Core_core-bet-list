namespace Fanex.BetList.Core.Builder.OddsBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Templates;
    using Fanex.BetList.Core.Utils;

    public class Odds1037 : Odds1
    {
        protected static readonly List<string> HidingOddsBetChoices = new List<string>
        {
            "101",
            "102",
            "103",
            "104",
            "105",
            "106"
        };

        public override Odds_Template Render(ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById funcGetOddsTypeName)
        {
            if (!string.IsNullOrEmpty(ticket.TransDesc))
            {
                var parsedTransDesc = TransDescParser.ParseTransDesc(ticket.TransDesc);
                var choiceId = parsedTransDesc.Item1;

                if (HidingOddsBetChoices.Contains(choiceId))
                {
                    Template.odds = "-";
                    Template.oddsType = string.Empty;

                    return Template;
                }
            }

            return base.Render(ticket, ticketData, funcGetOddsTypeName);
        }
    }
}