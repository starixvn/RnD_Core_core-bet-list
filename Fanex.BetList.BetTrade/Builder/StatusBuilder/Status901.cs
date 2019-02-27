namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Entities;

    public class Status901 : Status1
    {
        protected override void BuildResult(ITicket ticket, List<ITicketData> ticketData)
        {
            base.BuildResult(ticket, ticketData);

            var status = ticket.Status.ToLowerInvariant();

            Template.result = status.Equals(BetStatus.Void) || status.Equals(BetStatus.Refund)
                ? $"{Template.result}<br/><div style='font-weight: normal;'>({GetStatus()})</div>"
                : GetStatus();
        }

        protected override bool DetectAbnormalBet(ITicket ticket) => false;

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