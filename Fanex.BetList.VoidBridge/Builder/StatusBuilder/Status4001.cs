namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text.RegularExpressions;
    using Entities;
    using Utils;

    public class Status4001 : Status1
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
                var externalLink = $"{ticket.CurrencyId}, '{ticket.BetTeam}', {ticket.CustId}, '{ticket.BetCheck}', '{ticket.SiteId}'";
                Template.StatusResult.betId = externalLink;
            }
        }

        protected override void BuildResult(ITicket ticket, List<ITicketData> ticketData)
        {
            string status = ticket.Status.ToLower(CultureInfo.InvariantCulture);
            string result = GetResultString(ticket.Status);

            if (!string.IsNullOrEmpty(result))
            {
                if (!string.IsNullOrEmpty(ticket.BetCheck) && BetStatus.Running == status)
                {
                    Template.result = BuildRunningLink(ticket, result);
                    return;
                }

                Template.result = result;
                return;
            }

            Template.result = status;
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
                        ", 0, 0, '",
                        ticket.BetCheck ?? string.Empty,
                        "','",
                        ticket.CustId.ToString(),
                        "');\">",
                        status,
                        "</div>"
                    };

            return string.Join(null, viewRunningTicketResultTag);
        }

        protected override void BuildResultLRF(string status, RTFHelper rtfHelper)
        {
            var htmlTagMatching = new Regex("</?div[^<>]*[^<>]*>");
            string originalStatus = htmlTagMatching.Replace(status, string.Empty);

            base.BuildResultLRF(originalStatus, rtfHelper);
        }
    }
}