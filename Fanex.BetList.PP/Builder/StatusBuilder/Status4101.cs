namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils;
    using Fanex.BetList.Core.Utils.TicketExt;

    public class Status4101 : Status1
    {
        protected override void BuildStatusResult(ITicket ticket)
        {
            const int daySupportResultLink = 30;
            if (string.IsNullOrEmpty(ticket.BetCheck) ||
                ticket.IsTicketHide3rdResultLink(daySupportResultLink))
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
                ticket.CurrencyId,
                $"'{ticket.BetTeam}'",
                ticket.CustId,
                $"'{ticket.BetCheck}'",
                $"'{ticket.SiteId}'"
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