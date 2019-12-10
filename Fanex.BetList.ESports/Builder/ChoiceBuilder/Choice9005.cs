namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;

    /// <summary>
    /// E-Sports 2.0: Map X Total Kills Odd/Even.
    /// </summary>
    public class Choice9005 : Choice9002
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.betTeam = BetTeamValue.O == ticket.BetTeam.ToLowerInvariant() ? CoreBetList.odd : CoreBetList.even;
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = null;
            Template.betTeamClassName = BetTeamValue.O == ticket.BetTeam.ToLowerInvariant() ? Favorite : Underdog;
        }
    }
}