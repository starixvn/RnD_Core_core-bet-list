namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Constants;
    using Entities;

    /// <summary>
    /// BAIO - 1H Winning Margin 12 Way
    /// </summary>
    public class Choice608 : Choice602
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            base.BuildBetTeam(ticket, ticketHelper, ticketData);

            if (ticket.BetTeam.ToLowerInvariant() == BetTeamValue.D)
            {
                Template.betTeam = ticketHelper.GetResourceData(BasketballRefId, "D");
            }
        }
    }
}