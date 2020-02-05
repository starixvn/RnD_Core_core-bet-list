namespace Fanex.BetList.Core.Builder.OddsBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Templates;
    using Fanex.BetList.Core.Utils;

    public class Odds2401 : Odds1
    {
        private const string Lucky6ChoiceId = "1100";

        public override Odds_Template Render(ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById funcGetOddsTypeName)
        {
            var choiceId = GetChoiceId(ticket);

            if (!string.IsNullOrEmpty(choiceId) && Lucky6ChoiceId.Equals(choiceId))
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