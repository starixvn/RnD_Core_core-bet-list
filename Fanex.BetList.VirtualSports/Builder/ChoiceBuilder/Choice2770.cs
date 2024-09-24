namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Entities;

    public class Choice2770 : Choice5
    {        
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        => Template.betTeam = ticketHelper.GetTeamName3rd(ticket.BetTeam);

        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
        {
            base.BuildLeague(ticket, ticketHelper);

            Template.League.LeagueName.leagueName = ticketHelper.GetLeagueName3rd(ticket.LeagueId.ToString());
        }

        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
        {
            var _matchRoundInfo = VirtualSportsChoiceUtils.ParseMatchInfo("BVS_MatchCode", ticket.TransDesc, ticketHelper);
            Template.Match.homeTeam = _matchRoundInfo;
            Template.Match.awayTeam = string.Empty;
            Template.Match.home_firstGoal_lastGoal = string.Empty;
            Template.Match.away_firstGoal_lastGoal = string.Empty;
            Template.Match.VS = null;
        }       
    }
}