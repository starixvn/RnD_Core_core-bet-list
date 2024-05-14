namespace Fanex.BetList.Core.Builder.OddsBuilder
{
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Templates;
    using System.Collections.Generic;
    using Fanex.BetList.Core.Utils.Parsers;

    public class Odds2401 : Odds1
    {
        public virtual List<string> HideOddsForBetChoices => new List<string> {
            "1100",
            "1231",
            "1232",
            "1503",
            "1504",
            "1401",
            "1404",
            "1301",
            "1302",
            "1303",
            "1304",
            "BAC_LUCKY_SIX",
            "BAC_BANKER_BINGO",
            "BAC_PLAYER_BINGO",
            "BAC_TIGER",
            "BAC_TIGER_PAIR",
            "BAC_BANKER_PCP",
            "BAC_PLAYER_PCP",
            "BAC_BANKER_IN1",
            "BAC_BANKER_IN2",
            "BAC_PLAYER_IN2",
            "BAC_PLAYER_IN1"
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
            const string betChoiceKey = "choice";

            var parsedTransDesc = ExtraInfoParser.ParseExtraInfo(ticket.TransDesc);

            if (parsedTransDesc.ContainsKey(betChoiceKey))
            {
                return parsedTransDesc[betChoiceKey];
            }

            return string.Empty;
        }
    }
}