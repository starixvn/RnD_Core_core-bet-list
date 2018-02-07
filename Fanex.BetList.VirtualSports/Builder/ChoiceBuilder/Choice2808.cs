namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using Constants;
    using Entities;

    /// <summary>
    /// BetRadar Virtual Basketball - 1H Winning Margin 7 Way.
    /// </summary>
    public class Choice2808 : Choice2807
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            if (ticket.BetTeam.Equals(BetTeamValue.D, StringComparison.CurrentCultureIgnoreCase))
            {
                Template.betTeam = ticketHelper.GetResourceData(BasketballRefId, "D");
            }
            else
            {
                base.BuildBetTeam(ticket, ticketHelper, ticketData);
            }
        }
    }
}