namespace Fanex.BetList.Core.Builder.OddsBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Templates;
    using Fanex.BetList.Core.Utils;
    using Fanex.BetList.Core.Utils.Parsers;

    public class Odds3205 : Odds1
    {
        private static readonly List<string> DoubleBetChoices = new List<string>
        {
            "5004",
            "5005",
            "5006",
            "5010",
            "5011",
            "5012"
        };

        public override Odds_Template Render(ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById funcGetOddsTypeName)
        {
            var choiceId = GetChoiceId(ticket);

            if (!string.IsNullOrEmpty(choiceId) && DoubleBetChoices.Contains(choiceId))
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

            var parsedTransDesc = ExtraInfoParser.ParseExtraInfo(ticket.TransDesc);

            if (parsedTransDesc.ContainsKey(betChoiceKey))
            {
                return parsedTransDesc[betChoiceKey];
            }

            return string.Empty;
        }
    }
}