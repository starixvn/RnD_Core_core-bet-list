namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Constants;
    using Entities;
    using System.Collections.Generic;

    /// <summary>
    /// Bet type name: 2ND Over/Under.
    /// </summary>
    public class Choice18 : Choice3
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.betTeam = IsOver(ticket) ? CoreBetList.over : CoreBetList.under;
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            base.BuildBetTeamClassNameAndHandicap(ticket);

            Template.betTeamClassName = IsOver(ticket) ? Favorite : Underdog;
        }

        private bool IsOver(ITicket ticket)
        {
            return ticket.BetTeam.ToLowerInvariant() == BetTeamValue.O;
        }
    }
}