namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Entities;
    using Templates;

    /// <summary>
    /// Betby Virtual Sports - FT.1X2.
    /// </summary>
    public class Choice13000 : Choice5
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

            Template.AppendKickOffTime(ticket);
        }

        protected override void BuildBetTeamsInfo(ITicket ticket, ref RTFHelper rtfHelper)
        {
            base.BuildBetTeamsInfo(ticket, ref rtfHelper);

            rtfHelper.RTFRenderer.AddText($"\n{VirtualSportsChoiceUtils.ParseKickOffTime(ticket)}");
        }
    }
}