namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text.RegularExpressions;

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
                Template.StatusResult.refNo = "'" + ticket.BetCheck + "'";
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

        protected override void BuildResultLRF(string status, RTFHelper rtfHelper)
        {
            var htmlTagMatching = new Regex("</?div[^<>]*[^<>]*>");
            string originalStatus = htmlTagMatching.Replace(status, string.Empty);

            base.BuildResultLRF(originalStatus, rtfHelper);
        }
    }
}