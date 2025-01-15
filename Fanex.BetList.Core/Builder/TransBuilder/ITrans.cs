namespace Fanex.BetList.Core.Builder.TransBuilder
{
    using Entities;
    using BetListNPOI.SS.UserModel;
    using Templates;

    public interface ITrans
    {
        Trans_Template Template { get; set; }

        Trans_Template Render(ITicket ticket);

        string RenderTranstime(ITicket ticket);

        string RenderRefNo(ITicket ticket);

        string RenderLicenseeTxId(ITicket ticket);

        IRichTextString RenderRTF(ITicket ticket, RTFHelper rtfHelper);

        IRichTextString RenderTransTimeRTF(ITicket ticket, RTFHelper rtfHelper);

        IRichTextString RenderRefNoRTF(ITicket ticket, RTFHelper rtfHelper);

        IRichTextString RenderLicenseeTxIdRTF(ITicket ticket, RTFHelper rtfHelper);
    }
}