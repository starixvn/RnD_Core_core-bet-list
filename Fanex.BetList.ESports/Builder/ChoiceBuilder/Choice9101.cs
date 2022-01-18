namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Entities;
    using Fanex.BetList.Core.Constants;

    /// <summary>
    /// E-Sports 2.0:  Map X 1st Kill
    /// </summary>
    public class Choice9101 : Choice9001
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
            => Template.betTeam = BetTeamValue.H == ticket.BetTeam.ToLowerInvariant() ?
                ticketHelper.GetTeamNameById(ticket.HomeId) :
                ticketHelper.GetTeamNameById(ticket.AwayId);
    }
}