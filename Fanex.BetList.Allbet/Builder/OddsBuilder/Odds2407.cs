namespace Fanex.BetList.Core.Builder.OddsBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Templates;
    using Fanex.BetList.Core.Utils;

    public class Odds2407 : Odds1
    {
        private static readonly List<string> HideOddsForBetChoices = new List<string>
        {
            "8011",
            "8111",
            "8012",
            "8112",
            "8013",
            "8113",
            "8021",
            "8022",
            "8023",
            "8121",
            "8122",
            "8123"
        };

        public override Odds_Template Render(ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById funcGetOddsTypeName)
        {
            var choiceId = GetChoiceId(ticket);

            if (!string.IsNullOrEmpty(choiceId) && HideOddsForBetChoices.Contains(choiceId))
            {
                Template.odds = "-";
                Template.oddsType = string.Empty;

                return Template;
            }

            return base.Render(ticket, ticketData, funcGetOddsTypeName);
        }

        private static string GetChoiceId(ITicket ticket)
        {
            const string betChoiceKey = "Choice";

            var parsedTransDesc = ticket.ParseTransDesc();

            if (parsedTransDesc.ContainsKey(betChoiceKey))
            {
                return parsedTransDesc[betChoiceKey];
            }

            return string.Empty;
        }
    }
}