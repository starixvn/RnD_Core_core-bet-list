namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;

    /// <summary>
    /// Type: Warrior.
    /// </summary>
    public class Choice88 : Choice87
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.betTeam = ticket.BetTeam == BetTeamValue.H ? CoreBetList.warrior2ndBall : CoreBetList.warrior3rdBall;
        }
    }
}