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

            BuildBetType(ticket, ticketHelper, ticketData);

            BuildBetTeamAndMatch(ticket, ticketHelper, ticketData);

            BuildBetTeamClassNameAndHandicap(ticket);

            BuildLeague(ticket, ticketHelper);

            BuildSport(ticket, ticketHelper);
        }

        protected override void BuildBetTeamAndMatch(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
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