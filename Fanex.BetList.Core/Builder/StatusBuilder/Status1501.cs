namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using System.Collections.Generic;
    using Entities;
    using Templates;

    /// <summary>
    /// Keno games.
    /// </summary>
    public class Status1501 : Status1
    {
        public override Status_Template Render(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowVNIP)
        {
            base.Render(ticket, ticketHelper, ticketData, isShowVNIP);
            Template.StatusResult.Hide();

            return Template;
        }
    }
}