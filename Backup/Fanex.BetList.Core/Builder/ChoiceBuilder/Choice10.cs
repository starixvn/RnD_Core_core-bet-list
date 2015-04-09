namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Entities;

    /// <summary>
    /// Outright bet type.
    /// </summary>
    public class Choice10 : Choice1
    {
        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
        {
            UpdateAllMatchMemberToNull();
        }

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var teamId = ticket.HomeId == 0 ? ticket.AwayId : ticket.HomeId;

            Template.betTeam = ticketHelper.GetTeamNameById(teamId);
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = null;
            Template.betTeamClassName = Favorite;
        }

        protected override void BuildTicketStatus(ITicket ticket)
        {
            Template.ticketStatus = string.Empty;
        }
    }
}