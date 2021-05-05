namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils;

    public class Status3820 : Status3802
    {
        protected override void BuildResult(ITicket ticket, List<ITicketData> ticketData)
        {
            string status = ticket.Status.ToLowerInvariant();

            if (!string.IsNullOrEmpty(ticket.BetCheck) && BetStatus.Running == status)
            {
                Template.result = BuildRunningResultLink(ticket);
            }
            else
            {
                string displayingStatus = GetResultString(ticket.Status);

                Template.result = string.IsNullOrEmpty(displayingStatus) ? status : displayingStatus;
            }
        }

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

        private static string BuildRunningResultLink(ITicket ticket)
        {
            var parameterList = new object[]
            {
                ticket.MatchId,
                string.IsNullOrEmpty(ticket.Race) ? "0" : ticket.Race,
                ticket.BetTypeId,
                ticket.SportTypeId,
                ticket.TransId,
                $"'{ticket.Currency}'",
                $"'{Formatter.FullDateFormat(ticket.WinlostDate)}'",
                "0",
                ticket.LeagueId,
                "0",
                ticket.BetId ?? 0,
                $"'{ticket.MatchCode}'",
                ticket.CustId,
                $"'{ticket.BetCheck}'"
            };

            return $"<div class=\"result\" onclick=\"ViewResult({string.Join(", ", parameterList)});\">{CoreBetList.running}</div>";
        }

        protected override void BuildResultLRF(string status, RTFHelper rtfHelper)
        {
            string originalStatus = new Regex("</?[^<>]*>").Replace(status, string.Empty);

            base.BuildResultLRF(originalStatus, rtfHelper);
        }
    }
}