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
            }
        }

        protected override void BuildResult(ITicket ticket, List<ITicketData> ticketData)
        {
            string status = ticket.Status.ToLowerInvariant();
            string result = GetResultString(ticket.Status);

            if (!string.IsNullOrEmpty(result))
            {
                if (!string.IsNullOrEmpty(ticket.BetCheck) && BetStatus.Running == status)
                {
                    Template.result = BuildRunningLink(ticket, result);
                }
                else
                {
                    Template.result = result;
                }
            }
            else
            {
                Template.result = status;
            }
        }

        protected override void BuildResultLRF(string status, RTFHelper rtfHelper)
        {
            var htmlTagMatching = new Regex("</?div[^<>]*[^<>]*>");
            string originalStatus = htmlTagMatching.Replace(status, string.Empty);

            base.BuildResultLRF(originalStatus, rtfHelper);
        }

        private string BuildRunningLink(ITicket ticket, string status)
        {
            var viewRunningTicketResultTag = new string[]
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
                        ticket.TransId.ToString(CultureInfo.InvariantCulture),
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
                        ", ",
                        "0, ",
                        (ticket.BetId ?? 0).ToString(CultureInfo.InvariantCulture),
                        ");\">",
                        status,
                        "</div>"
                    };

            return string.Join(null, viewRunningTicketResultTag);
        }
    }
}