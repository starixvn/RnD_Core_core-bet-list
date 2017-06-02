namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Entities;

    /// <summary>
    /// BetRadar Virtual Sports - 1H 1X2
    /// </summary>
    public class Choice2702 : Choice2701
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
            => Template.betTeam = $"{CoreBetList.ht}.{ticket.BetTeam.ToUpperInvariant()}";
    }
}