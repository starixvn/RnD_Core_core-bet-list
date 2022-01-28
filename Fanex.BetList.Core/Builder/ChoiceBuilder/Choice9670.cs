namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Entities;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Utils;
    using System.Collections.Generic;

    /// <summary>
    ///  Home 1st Inns Prematch Top Batsman
    /// </summary>
    public class Choice9670 : Choice9400
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
            => Template.betTeam = ticketHelper.GetTeamNameById(ticket.TransDesc);
    }
}