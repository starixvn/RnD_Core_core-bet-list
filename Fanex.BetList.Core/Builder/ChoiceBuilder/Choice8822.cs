namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;

    public class Choice8822 : Choice1
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var betTeams = new Dictionary<string, string>
            {
                    {BetTeamValue.H1, $"{CoreBetList.HomeWinBy1Run}"},
                    {BetTeamValue.H2, $"{CoreBetList.HomeWinBy2Runs}"},
                    {BetTeamValue.H3, $"{CoreBetList.HomeWinBy3Runs}"},
                    {BetTeamValue.H4Over, $"{CoreBetList.Home} {CoreBetList.HomeWinBy4_Runs}"},
                    {BetTeamValue.A1, $"{CoreBetList.Away} {CoreBetList.AwayWinBy1Runs}"},
                    {BetTeamValue.A2, $"{CoreBetList.Away} {CoreBetList.AwayWinBy2Runs}"},
                    {BetTeamValue.A3, $"{CoreBetList.Away} {CoreBetList.AwayWinBy3Runs}"},
                    {BetTeamValue.A4Over, $"{CoreBetList.Away} {CoreBetList.AwayWinBy4_Runs}"}
            };

            var betTeam = ticket.BetTeam.ToLowerInvariant();
            Template.betTeam = betTeams.ContainsKey(betTeam) ? betTeams[betTeam] : ticket.BetTeam;
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = null;
            Template.betTeamClassName = Favorite;
        }
    }
}