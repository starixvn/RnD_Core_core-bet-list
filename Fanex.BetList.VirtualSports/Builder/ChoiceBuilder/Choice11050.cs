namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Entities;
    using Templates;

    public class Choice11050 : Choice10400
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var betTeam = ticket.BetTeam.ToLowerInvariant();
            switch (betTeam)
            {
                case "1":
                    betTeam = ticketHelper.GetTeamName3rd(ticket.HomeId.ToString());
                    break;
                case "2":
                    betTeam = ticketHelper.GetTeamName3rd(ticket.AwayId.ToString());
                    break;
                default:
                    betTeam = CoreBetList.draw;
                    break;
            }

            Template.betTeam = betTeam;
        }

        protected override void GeneralMatchRoundInfo(ITicket ticket, ITicketHelper ticketHelper)
        => _matchRoundInfo = VirtualSportsChoiceUtils.ParseMatchInfo("VYVS_MatchCode", ticket.TransDesc, ticketHelper);

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = null;
            Template.betTeamClassName = Favorite;
        }

        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
        {
            base.BuildLeague(ticket, ticketHelper);
            Template.League.LeagueName.leagueName = ticketHelper.GetLeagueName3rd(ticket.LeagueId.ToString());
        }

        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
        {
            base.BuildMatch(ticket, ticketHelper);

            Template.Match.homeTeam = ticketHelper.GetTeamName3rd(ticket.HomeId.ToString());
            Template.Match.awayTeam = ticketHelper.GetTeamName3rd(ticket.AwayId.ToString());            
        }
    }
}