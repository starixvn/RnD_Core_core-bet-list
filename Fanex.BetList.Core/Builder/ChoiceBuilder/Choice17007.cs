namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.App_GlobalResources;
    using System.Collections.Generic;

    public class Choice17007 : Choice17000
    {
        private const string BetIdTag = "<span class='bet-id'>{0}</span>";

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        => Template.BetType.betTypeName = string.Format(BetIdTag, $"{CoreBetList.BetID}: {ticket.RefNo}");

        protected override void BuildBetTypeName(ref RTFHelper rtfHelper)
        {
            var choice = Template.BetType.betTypeName
                                                .Replace("<span class='bet-id'>", string.Empty)
                                                .Replace("</span>", string.Empty);
            rtfHelper.RTFRenderer.AddText("\n" + choice);
        }
    }
}