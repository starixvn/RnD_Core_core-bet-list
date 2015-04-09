namespace Fanex.BetList.Core.Builder.OddsBuilder
{
    using System.Collections.Generic;
    using Entities;
    using NPOI.SS.UserModel;
    using Templates;

    /// <summary>
    /// Interface IOdds.
    /// </summary>
    public interface IOdds
    {
        Odds_Template Template { get; set; }

        Odds_Template Render(ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById funcGetOddsTypeName);

        IRichTextString RenderRTF(ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById funcGetOddsTypeName, RTFHelper rtfHelper);
    }
}