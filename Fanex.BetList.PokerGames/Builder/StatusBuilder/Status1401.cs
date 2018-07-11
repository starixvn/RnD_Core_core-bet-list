namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using App_GlobalResources;
    using Entities;
    using Utils;

    public class Status1401 : Status1
    {
        protected override void BuildStatusResult(ITicket ticket)
        {
            base.BuildStatusResult(ticket);
            Template.StatusResult.betId = $"{ticket.BetId ?? 0}, '{ticket.BetCheck}', {ticket.CustId}";
        }

        protected override void BuildResult(ITicket ticket, List<ITicketData> ticketData)
        {
            string status = ticket.Status.ToLowerInvariant();

            if (BetStatus.Running == status)
            {
                Template.result = BuildRunningResultLink(ticket);
            }
            else
            {
                string displayingStatus = GetResultString(ticket.Status);

                Template.result = string.IsNullOrEmpty(displayingStatus) ? status : displayingStatus;
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
                $"'{ticket.UserName}'",
                $"'{Formatter.FullDateFormat(ticket.WinlostDate)}'",
                "0",
                ticket.LeagueId,
                "0",
                ticket.BetId ?? 0,
                $"'{ticket.BetCheck}'",
                ticket.CustId
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