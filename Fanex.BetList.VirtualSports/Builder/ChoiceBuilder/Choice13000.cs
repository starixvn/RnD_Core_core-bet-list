namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Entities;

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

        protected override void BuildScore(ITicket ticket)
        {
            if (Template.Score == null)
            {
                return;
            }

            Template.Score.SetValue($"<span class=\"{Favorite}\"> [{CoreBetList.Live}]</span>");
            Template.Score.Visible = ticket.IsLive;
        }

        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
        => Template.League.LeagueName.leagueName = ticketHelper.GetLeagueName3rd(ticket.LeagueId.ToString());

        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
        {
            Template.Match.home_firstGoal_lastGoal = string.Empty;
            Template.Match.away_firstGoal_lastGoal = string.Empty;
            Template.Match.homeTeam = ticketHelper.GetTeamName3rd(ticket.HomeId.ToString());
            Template.Match.awayTeam = ticketHelper.GetTeamName3rd(ticket.AwayId.ToString());

            Template.AppendKickOffTime(ticket);
        }

        protected override void BuildBetTeamsInfo(ITicket ticket, ref RTFHelper rtfHelper)
        {
            base.BuildBetTeamsInfo(ticket, ref rtfHelper);

            var kickoffTime = VirtualSportsChoiceUtils.ParseKickOffTime(ticket);
            if (!string.IsNullOrWhiteSpace(kickoffTime))
            {
                rtfHelper.RTFRenderer.AddText($"\n{kickoffTime}");
            }
        }
    }
}