namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Entities;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Constants;
    using System.Collections.Generic;

    /// <summary>
    /// Home Inns Runs OU
    /// </summary>
    public class Choice9404 : Choice3
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.betTeam = BetTeamValue.O == ticket.BetTeam.ToLowerInvariant() ? CoreBetList.over : CoreBetList.under;
        }
    }
}