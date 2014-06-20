namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using System.Collections.Generic;
    using Entities;
    using Templates;

    /// <summary>
    /// Bet Type: Outright.
    /// </summary>
    public class Status10 : Status1
    {
        public override Status_Template Render(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowVNIP)
        {
            base.Render(ticket, ticketHelper, ticketData, isShowVNIP);
            Template.StatusResult.Hide();
            Template.StatusResult.isoutright = "1";
            return Template;
        }
    }
}