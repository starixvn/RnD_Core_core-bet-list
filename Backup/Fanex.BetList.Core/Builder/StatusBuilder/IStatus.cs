namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using System.Collections.Generic;
    using Entities;
    using NPOI.SS.UserModel;
    using Templates;

    public interface IStatus
    {
        Status_Template Template { get; set; }

        Status_Template Render(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowVNIP);

        IRichTextString RenderRTF(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowVNIP, RTFHelper rtfHelper);
    }
}