namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Entities;

    /// <summary>
    /// P2P Transfer.
    /// </summary>
    public class Choice23101 : Choice23001
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.betTeam = string.Join(null, new string[] { CoreBetList.p2pGame, " - ", CoreBetList.transfer });
        }
    }
}