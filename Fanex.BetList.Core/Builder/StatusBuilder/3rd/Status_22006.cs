namespace Fanex.BetList.Core.Builder.StatusBuilder._3rd
{
    using System.Collections.Generic;
    using Entities;
    using Templates;

    /// <summary>
    /// Build Status for PlayTech Casino.
    /// </summary>
    public class Status_22006 : Status1
    {
        public override Status_Template Render(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowVNIP)
        {
            Template.result = ticket.Status.ToLower();
            string result = GetResultString(ticket.Status);

            if (!string.IsNullOrEmpty(result))
            {
                Template.result = result;
            }

            Template.StatusResult = null;
            Template.ShowIP = null;

            return Template;
        }
    }
}