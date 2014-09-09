namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Entities;

    /// <summary>
    /// Keno games.
    /// </summary>
    public class Choice1501 : Choice1
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            string kenoBetTypeId;

            Template.betTeam = ticketHelper.GetKenoBetChoiceName(ticket.BetTeam, out kenoBetTypeId);
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            string kenoBetTypeId;

            ticketHelper.GetKenoBetChoiceName(ticket.BetTeam, out kenoBetTypeId);

            Template.BetType.betTypeName = ticketHelper.GetKenoBetTypeName(kenoBetTypeId);
        }

        protected override void BuildSport(ITicket ticket, ITicketHelper ticketHelper)
        {
            string kenoLeagueInfo = string.Join(null, new string[] { ticketHelper.GetBetTypeNameById(ticket.BetTypeId), " [", ticket.MatchId.ToString(), "]" });

            Template.League.LeagueName.leagueName = kenoLeagueInfo;
        }

        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
        {
            Template.League.sportTypeName = ticketHelper.GetSportNameById(ticket.SportTypeId);
        }

        protected override void BuildScore(ITicket ticket)
        {
            Template.Score = null;
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            // Does not show handicap, match, and score.
            Template.Handicap = null;
            Template.betTeamClassName = Favorite;
        }

        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
        {
            UpdateAllMatchMemberToNull();
        }
    }
}