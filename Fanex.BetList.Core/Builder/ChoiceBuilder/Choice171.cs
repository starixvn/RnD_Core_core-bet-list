namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;

    /// <summary>
    /// Bet Radar: Winning Margin.
    /// </summary>
    public class Choice171 : Choice1
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var betTeam = string.Empty;

            switch (ticket.BetTeam.ToLowerInvariant())
            {
                case BetTeamValue.H1:
                    betTeam = string.Format("{0} {1}", CoreBetList.Home171, CoreBetList.ToWinByOneGoal);
                    break;

                case BetTeamValue.H2:
                    betTeam = string.Format("{0} {1}", CoreBetList.Home171, CoreBetList.ToWinByTwoGoals);
                    break;

                case BetTeamValue.H3:
                    betTeam = string.Format("{0} {1}", CoreBetList.Home171, CoreBetList.ToWinByThreeUpGoals);
                    break;

                case BetTeamValue.D:
                    betTeam = CoreBetList.draw;
                    break;

                case BetTeamValue.A1:
                    betTeam = string.Format("{0} {1}", CoreBetList.Away171, CoreBetList.ToWinByOneGoal);
                    break;

                case BetTeamValue.A2:
                    betTeam = string.Format("{0} {1}", CoreBetList.Away171, CoreBetList.ToWinByTwoGoals);
                    break;

                case BetTeamValue.A3:
                    betTeam = string.Format("{0} {1}", CoreBetList.Away171, CoreBetList.ToWinByThreeUpGoals);
                    break;

                default:
                    betTeam = string.Empty;
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