namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Entities;

    /// <summary>
    ///  BetRadar BetType : Half Time/Full Time Correct Score - 152.
    /// </summary>
    public class Choice152 : Choice133
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.betTeam = ticket.BetTeam;
        }
    }
}