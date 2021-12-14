namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text.RegularExpressions;
    using Entities;
    using Utils;

    public class Status2101 : Status1
    {
        protected override void BuildStatusResult(ITicket ticket)
        {
            if (string.IsNullOrEmpty(ticket.BetCheck))
            {
                Template.StatusResult.Hide();
            }
            else
            {
                base.BuildStatusResult(ticket);
                Template.StatusResult.betId = $"{ticket.CurrencyId}, '{ticket.BetTeam}', {ticket.CustId}, '{ticket.BetCheck}', '{ticket.SiteId}'";
            }
        }

        protected override void BuildResult(ITicket ticket, List<ITicketData> ticketData)
        {
            string status = ticket.Status.ToLowerInvariant();
            string result = GetResultString(ticket.Status);

            Template.result = BetStatus.Running == status && !string.IsNullOrEmpty(ticket.BetCheck)
                ? BuildRunningLink(ticket, result)
                : result;
        }

        protected override void BuildResultLRF(string status, RTFHelper rtfHelper)
        {
            var htmlTagMatching = new Regex("</?div[^<>]*[^<>]*>");
            string originalStatus = htmlTagMatching.Replace(status, string.Empty);

            base.BuildResultLRF(originalStatus, rtfHelper);
        }

        private static string BuildRunningLink(ITicket ticket, string status)
        {
            var viewRunningTicketResultTag = new[]
                {
                    "<div class=\"result\" onclick=\"ViewResult(",
                    ticket.MatchId.ToString(CultureInfo.InvariantCulture),
                    ", ",
                    string.IsNullOrEmpty(ticket.Race) ? "0" : ticket.Race,
                    ", ",
                    ticket.BetTypeId.ToString(CultureInfo.InvariantCulture),
                    ", ",
                    ticket.SportTypeId.ToString(CultureInfo.InvariantCulture),
                    ", ",
                    "'",
                    ticket.BetCheck ?? string.Empty,
                    "'",
                    ", ",
                    "'",
                    ticket.UserName ?? string.Empty,
                    "'",
                    ", ",
                    "'",
                    Formatter.FullDateFormat(ticket.WinlostDate),
                    "'",
                    ", ",
                    "0, ",
                    ticket.LeagueId.ToString(CultureInfo.InvariantCulture),
                    ", 0, 0);\">",
                    status,
                    "</div>"
                };

            return string.Join(null, viewRunningTicketResultTag);
        }
    }
}