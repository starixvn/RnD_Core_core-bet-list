namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using System.Collections.Generic;
    using System.Globalization;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;

    public class Status901 : Status1
    {
        protected override void BuildResult(ITicket ticket, List<ITicketData> ticketData)
        {
            base.BuildResult(ticket, ticketData);

            var status = ticket.Status.ToLower(CultureInfo.InvariantCulture);

            if (status.Equals(BetStatus.Void) || status.Equals(BetStatus.Refund))
            {
                Template.result = string.Format("{0}<br/><div style='font-weight: normal;'>({1})</div>", Template.result, GetStatus());
            }
            else
            {
                Template.result = GetStatus();
            }
        }

        protected virtual string GetStatus()
        {
            return CoreBetList.Sold;
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
