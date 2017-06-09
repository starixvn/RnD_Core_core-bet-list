namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using System.Collections.Generic;
    using System.Globalization;
    using App_GlobalResources;
    using Entities;

    public class Status901 : Status1
    {
        protected override void BuildResult(ITicket ticket, List<ITicketData> ticketData)
        {
            base.BuildResult(ticket, ticketData);

            var status = ticket.Status.ToLowerInvariant();

            Template.result = status.Equals(BetStatus.Void) || status.Equals(BetStatus.Refund)
                ? string.Format("{0}<br/><div style='font-weight: normal;'>({1})</div>", Template.result, GetStatus())
                : GetStatus();
        }

        protected virtual string GetStatus()
            => CoreBetList.Sold;

        protected override void BuildResultLRF(string status, RTFHelper rtfHelper)
            => rtfHelper.RTFRenderer.AddText(
                status.Replace("<br/><div style='font-weight: normal;'>", "\n").Replace("</div>", string.Empty),
                rtfHelper.PosFont);

        protected override void BuildStatusResult(ITicket ticket)
            => Template.StatusResult.Hide();
    }
}