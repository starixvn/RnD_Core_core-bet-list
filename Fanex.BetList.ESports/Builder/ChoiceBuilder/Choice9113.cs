namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Entities;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Constants;

    /// <summary>
    /// E-Sports 4.0: Map X Lane With Most Turrets Destroyed
    /// </summary>
    public class Choice9113 : Choice9114
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
            => Template.betTeam = BetTeamValue.T == ticket.BetTeam.ToLowerInvariant() ? CoreBetList.Top : CoreBetList.Bottom;
    }
}