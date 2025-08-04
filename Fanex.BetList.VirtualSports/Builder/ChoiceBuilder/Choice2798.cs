namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Entities;
    using System.Collections.Generic;

    public class Choice2798 : Choice10
    {
        protected override void BuildSport(ITicket ticket, ITicketHelper ticketHelper)
        => Template.League.sportTypeName = ticketHelper.GetResourceData("DummySport", "9908");

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        => Template.betTeam = string.Empty;

    }
}