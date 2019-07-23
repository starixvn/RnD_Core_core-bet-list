namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;

    /// <summary>
    /// PHAIO 4B: 1H Correct Corners.
    /// </summary>
    public class Choice494 : Choice413
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.betTeam = BetTeamValue.AOS == ticket.BetTeam.ToLowerInvariant() ? CoreBetList.AOS : ticket.BetTeam.Trim();
        }
    }
}