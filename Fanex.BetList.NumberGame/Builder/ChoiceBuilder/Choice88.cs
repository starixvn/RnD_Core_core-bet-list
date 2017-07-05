namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Constants;
    using Entities;
    using Resources;

    /// <summary>
    /// Type: Warrior.
    /// </summary>
    public class Choice88 : Choice87
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.betTeam = ticket.BetTeam == BetTeamValue.H ? NumberGameLang.warrior2ndBall : NumberGameLang.warrior3rdBall;
        }
    }
}