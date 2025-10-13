namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Entities;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using Utils;

    public class Status2101 : StatusRNG
    {
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
            var parameterList = new object[]
            {
                ticket.MatchId,
                string.IsNullOrEmpty(ticket.Race) ? "0" : ticket.Race,
                ticket.BetTypeId,
                ticket.SportTypeId,
                $"'{ticket.TransId}'",
                $"'{ticket.UserName}'",
                $"'{Formatter.FullDateFormat(ticket.WinlostDate)}'",
                "0",
                ticket.LeagueId,
                "0",
                ticket.CurrencyId,
                $"'{ticket.BetTeam}'",
                ticket.CustId,
                $"'{ticket.BetCheck}'",
                $"'{ticket.SiteId}'"
            };

            return $"<div class=\"result\" onclick=\"ViewResult({string.Join(", ", parameterList)});\">{status}</div>";
        }
    }
}