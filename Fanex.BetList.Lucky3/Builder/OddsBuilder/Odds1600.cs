namespace Fanex.BetList.Core.Builder.OddsBuilder
{
    using System.Collections.Generic;
    using Entities;
    using NPOI.HSSF.UserModel;
    using NPOI.SS.UserModel;
    using Templates;

    public class Odds1600 : Odds1
    {
        public override Odds_Template Render(ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById funcGetOddsTypeName)
        {
            Template.SetValue("-");

            return Template;
        }

        public override IRichTextString RenderRTF(ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById funcGetOddsTypeName, RTFHelper rtfHelper)
        {
            return new HSSFRichTextString("-");
        }
    }
}