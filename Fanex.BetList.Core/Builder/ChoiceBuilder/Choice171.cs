namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;

    /// <summary>
    /// Bet Radar: Winning Margin.
    /// </summary>
    public class Choice171 : Choice1
    {
        /// <summary>
        /// Builds the bet team.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        /// <param name="ticketData">The ticket data.</param>
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var betTeam = string.Empty;

            switch (ticket.BetTeam.ToLowerInvariant())
            {
                case BetTeamValue.H1:
                    betTeam = string.Format("{0} {1}", Template.Match.homeTeam, CoreBetList.ToWinByOneGoal);
                    break;

                case BetTeamValue.H2:
                    betTeam = string.Format("{0} {1}", Template.Match.homeTeam, CoreBetList.ToWinByTwoGoals);
                    break;

                case BetTeamValue.H3:
                    betTeam = string.Format("{0} {1}", Template.Match.homeTeam, CoreBetList.ToWinByThreeUpGoals);
                    break;

                case BetTeamValue.D:
                    betTeam = CoreBetList.draw;
                    break;

                case BetTeamValue.A1:
                    betTeam = string.Format("{0} {1}", Template.Match.awayTeam, CoreBetList.ToWinByOneGoal);
                    break;

                case BetTeamValue.A2:
                    betTeam = string.Format("{0} {1}", Template.Match.awayTeam, CoreBetList.ToWinByTwoGoals);
                    break;

                case BetTeamValue.A3:
                    betTeam = string.Format("{0} {1}", Template.Match.awayTeam, CoreBetList.ToWinByThreeUpGoals);
                    break;
            }

            Template.betTeam = betTeam;
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = null;
            Template.betTeamClassName = Favorite;
        }
    }
}