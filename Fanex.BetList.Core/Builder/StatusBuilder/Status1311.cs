namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using System.Collections.Generic;
    using Entities;
    using Templates;

    /// <summary>
    ///  Set x Winner.
    /// </summary>
    public class Status1311 : Status1
    {
        public override Status_Template Render(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowVNIP)
        {
            base.Render(ticket, ticketHelper, ticketData, isShowVNIP);

            var refNo = "0";
            if (ticket.BetId.ToString().Length > 2)
            {
                refNo = ticket.BetId.ToString().Substring(1, 2);
            }

            Template.StatusResult.refNo = refNo;

            return Template;
        }
    }
}