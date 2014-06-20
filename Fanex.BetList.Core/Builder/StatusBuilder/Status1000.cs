namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using System.Collections.Generic;
    using Entities;
    using Templates;

    /// <summary>
    /// P2P Games.
    /// </summary>
    public class Status1000 : Status1
    {
        public override Status_Template Render(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowVNIP)
        {
            base.Render(ticket, ticketHelper, ticketData, isShowVNIP);

            // Use sessionId instead of refNo to call API service
            Template.StatusResult.refNo = ticket.BetCheck;

            return Template;
        }
    }
}