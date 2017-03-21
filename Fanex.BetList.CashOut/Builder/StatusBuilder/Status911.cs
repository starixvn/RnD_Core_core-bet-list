namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using System.Collections.Generic;
    using System.Globalization;
    using App_GlobalResources;
    using Entities;

    public class Status911 : Status1
    {
        private const string ResultHtmlFormat = "{0}<br/><div style='font-weight: normal;'>({1})</div>";

        protected override void BuildResult(ITicket ticket, List<ITicketData> ticketData)
        {
            base.BuildResult(ticket, ticketData);

            var status = ticket.Status.ToLower(CultureInfo.InvariantCulture);

            if (status.Equals(BetStatus.Void) || status.Equals(BetStatus.Refund))
            {
                Template.result = string.Format(ResultHtmlFormat, Template.result, GetStatus());
            }
            else
            {
                Template.result = GetStatus();
            }
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
            status = status.Replace("<br/><div style='font-weight: normal;'>", "\n");
            status = status.Replace("</div>", string.Empty);

            rtfHelper.RTFRenderer.AddText(status, rtfHelper.PosFont);
        }

        protected override void BuildStatusResult(ITicket ticket)
        {
            Template.StatusResult.Hide();
        }
    }
}