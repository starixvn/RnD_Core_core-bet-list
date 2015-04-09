namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Entities;
    using NPOI.SS.UserModel;
    using Templates;

    /// <summary>
    /// The top abstract layer of Choice.
    /// </summary>
    public interface IChoice
    {
        Choice_Template Template { get; set; }

        Choice_Template Render(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowScoreMap);

        List<IRichTextString> RenderRTF(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowScoreMap, RTFHelper rtfHelper);
    }
}