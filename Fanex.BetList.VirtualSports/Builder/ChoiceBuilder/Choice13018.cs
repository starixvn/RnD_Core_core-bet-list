namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Entities;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Constants;

    public class Choice13018 : Choice13000
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        => Template.betTeam = ticket.BetTeam != null && ticket.BetTeam.ToLowerInvariant().Equals(BetTeamValue.H) ? CoreBetList.odd : CoreBetList.even;

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.betTeamClassName = ticket.BetTeam != null && ticket.BetTeam.ToLowerInvariant().Equals(BetTeamValue.H) ? Favorite : Underdog;
            Template.Handicap.handicap = null;
        }
    }
}