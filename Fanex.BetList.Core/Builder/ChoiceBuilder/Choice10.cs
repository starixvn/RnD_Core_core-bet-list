namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Entities;

    /// <summary>
    /// Outright bet type.
    /// </summary>
    public class Choice10 : Choice1
    {
        protected override void BuildChoice(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            BuildScore(ticket);

            BuildBetType(ticket, ticketHelper);

            BuildBetTeamAndMatch(ticket, ticketHelper);

            BuildBetTeamClassNameAndHandicap(ticket);

            BuildLeague(ticket, ticketHelper);
        }

        protected override void BuildBetTeamAndMatch(ITicket ticket, ITicketHelper ticketHelper)
        {
            UpdateAllMatchMemberToNull();

            Template.betTeam = ticketHelper.GetTeamNameById(ticket.HomeId == 0 ? ticket.AwayId : ticket.HomeId);
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = null;
            Template.betTeamClassName = Favorite;
        }
    }
}