namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Entities;
    using System.Collections.Generic;

    public class Choice221 : Choice4
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var situationName = ticketHelper.GetFastMarketSituation(
                ticket.BetTypeId.ToString(),
                ticket.BetTeam,
                ticket.Handicap1,
                ticket.Handicap2);

            Template.betTeam = situationName;
        }
    }
}