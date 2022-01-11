namespace Fanex.BetList.Core.Builder.TransBuilder
{
    using Entities;
    using NPOI.SS.UserModel;
    using Templates;

    public interface ITrans
    {
        Trans_Template Template { get; set; }

        Trans_Template Render(ITicket ticket);

        string RenderTranstime(ITicket ticket);

        string RenderRefNo(ITicket ticket);

        string RenderLicenseeTxId(ITicket ticket);

        IRichTextString RenderRTF(ITicket ticket, ITicketHelper ticketHelper);

        IRichTextString RenderTransTimeRTF(ITicket ticket, ITicketHelper ticketHelper);

        IRichTextString RenderRefNoRTF(ITicket ticket, ITicketHelper ticketHelper);

        IRichTextString RenderLicenseeTxIdRTF(ITicket ticket, ITicketHelper ticketHelper);
    }
}