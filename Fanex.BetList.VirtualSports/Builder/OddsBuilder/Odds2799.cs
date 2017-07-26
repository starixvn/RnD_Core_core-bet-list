namespace Fanex.BetList.Core.Builder.OddsBuilder
{
    using System.Collections.Generic;
    using Entities;
    using Templates;
    using Utils;

    public class Odds2799 : Odds1
    {
        public override Odds_Template Render(ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById funcGetOddsTypeName)
        {
            Template.odds = ShowOdds(ticket) ? FormatOdds(ticket.MasterOdds) : "-";
            Template.oddsType = string.Empty;

            return Template;
        }

        private static bool ShowOdds(ITicket ticket)
        {
            string statusId = ticket.StatusId != null ? ticket.StatusId.ToString() : string.Empty;
            string status = ticket.Status ?? string.Empty;

            status = status.Trim().ToLowerInvariant();

            bool isWin = statusId == BetStatusId.Won || status == BetStatus.Won;

            return isWin && ticket.MasterOdds != 0;
        }

        private static string FormatOdds(decimal odds)
            => odds < 0
                ? string.Format(ConvertByBetType.Minus, string.Format(Formatter.DECIMALFORMAT40, odds))
                : string.Format(Formatter.DECIMALFORMAT40, odds);
    }
}