namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Entities;

    public class Status911 : Status1
    {
        private const string ResultHtmlFormat = "{0}<br/><div style='font-weight: normal;'>({1})</div>";

        protected override void BuildResult(ITicket ticket, List<ITicketData> ticketData)
        {
            base.BuildResult(ticket, ticketData);

            var status = ticket.Status.ToLowerInvariant();

            Template.result = status.Equals(BetStatus.Void) || status.Equals(BetStatus.Refund)
                ? string.Format(ResultHtmlFormat, Template.result, GetStatus())
                : GetStatus();
        }

        protected override void BuildCashOutHistory(ITicket ticket, ITicketHelper ticketHelper)
        {
            if (ticketHelper.HideStatusCashOutHistory)
            {
                Template.CashOutHistory.Hide();
                return;
            }

            Template.CashOutHistory.transId = ticket.TransId.ToString();
            Template.CashOutHistory.refNo = ticket.RefNo;
            Template.CashOutHistory.betType = ticket.BetTypeId.ToString();
            Template.CashOutHistory.betdaqId = ticket.BetdaqId.ToString();
        }

        protected virtual string GetStatus()
        {
            return CoreBetList.CashOut;
        }

        protected override void BuildResultLRF(string status, RTFHelper rtfHelper)
        {
            var statusText = status
                .Replace("<br/><div style='font-weight: normal;'>", "\n")
                .Replace("</div>", string.Empty);

            rtfHelper.RTFRenderer.AddText(statusText, rtfHelper.PosFont);
        }

        protected override void BuildStatusResult(ITicket ticket)
        {
            Template.StatusResult.Hide();
        }
    }
}