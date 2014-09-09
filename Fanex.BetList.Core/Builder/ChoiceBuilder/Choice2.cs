namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;

    /// <summary>
    /// Bet Type: Odd/Even.
    /// </summary>
    public class Choice2 : Choice1
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.betTeam = ticket.BetTeam == BetTeamValue.H ? CoreBetList.odd : CoreBetList.even;
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = null;
            Template.betTeamClassName = Template.betTeam == CoreBetList.odd ? Favorite : Underdog;
        }
    }
}