namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Entities;

    /// <summary>
    /// Fast Markets: What will happen first in next 1 minute
    /// </summary>
    public class Choice223 : Choice1
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var choice = ticketHelper.GetFastMarketSituation(
                ticket.BetTypeId.ToString(),
                ticket.BetTeam,
                ticket.Handicap1,
                ticket.Handicap2);

            Template.betTeam = choice;
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = null;
            Template.betTeamClassName = Favorite;
        }
    }
}