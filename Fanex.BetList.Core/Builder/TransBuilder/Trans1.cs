namespace Fanex.BetList.Core.Builder.TransBuilder
{
    using System.Globalization;
    using App_GlobalResources;
    using Entities;
    using Fanex.BetList.Core.Utils;
    using NPOI.HSSF.UserModel;
    using NPOI.SS.UserModel;
    using Templates;

    /// <summary>
    /// Trans for most bet types.
    /// </summary>
    public class Trans1 : ITrans
    {
        private const string EventDateHtmlBlock = "<div class=\"event-date\">{0}</div>";

        public Trans1()
        {
            Template = new Trans_Template();
        }

        public Trans_Template Template { get; set; }

        public virtual Trans_Template Render(ITicket ticket)
        {
            BuildTransTime(ticket);

            BuildRefNo(ticket);

            return Template;
        }

        public virtual IRichTextString RenderRTF(ITicket ticket, ITicketHelper ticketHelper)
        {
            Render(ticket);

            string refNo = Template.TransTime.refNo;
            string transTime = string.Join(null, new[] { CoreBetList.refno, ": ", refNo });
            IRichTextString rtfTrans = new HSSFRichTextString(string.Join(null, new[] { transTime, "\n", Template.transTime }));

            return rtfTrans;
        }

        public virtual string RenderEventDate(ITicket ticket)
            => string.Format(EventDateHtmlBlock, Formatter.FormatDateTimeEn12ShortMonthNoSec(ticket.EventDate));

        protected virtual void BuildTransTime(ITicket ticket)
#pragma warning disable S1449 // Culture should be specified for "string" operations
            => Template.transTime = ticket.TransDate.ToString();

#pragma warning restore S1449 // Culture should be specified for "string" operations

        protected virtual void BuildRefNo(ITicket ticket)
            => Template.TransTime.refNo = ticket.TransId.ToString(); // For Mix parlay(Trans9) to reuse
    }
}