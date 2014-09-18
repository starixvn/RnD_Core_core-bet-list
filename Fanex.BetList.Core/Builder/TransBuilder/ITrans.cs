namespace Fanex.BetList.Core.Builder.TransBuilder
{
    using Entities;
    using NPOI.SS.UserModel;
    using Templates;

    public interface ITrans
    {
        Trans_Template Template { get; set; }

        Trans_Template Render(ITicket ticket);

        IRichTextString RenderRTF(ITicket ticket, ITicketHelper ticketHelper);
    }
}