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
            switch (ticket.BetTeam.ToLowerInvariant())
            {
                case BetTeamValue.H1:
                    Template.betTeam = $"{CoreBetList.Home} {CoreBetList.ToWinByOneGoal}";
                    break;

                case BetTeamValue.H2:
                    Template.betTeam = $"{CoreBetList.Home} {CoreBetList.ToWinByTwoGoals}";
                    break;

                case BetTeamValue.H3:
                    Template.betTeam = $"{CoreBetList.Home} {CoreBetList.ToWinByThreeUpGoals}";
                    break;

                case BetTeamValue.D:
                    Template.betTeam = CoreBetList.draw;
                    break;

                case BetTeamValue.A1:
                    Template.betTeam = $"{CoreBetList.Away} {CoreBetList.ToWinByOneGoal}";
                    break;

                case BetTeamValue.A2:
                    Template.betTeam = $"{CoreBetList.Away} {CoreBetList.ToWinByTwoGoals}";
                    break;

                case BetTeamValue.A3:
                    Template.betTeam = $"{CoreBetList.Away} {CoreBetList.ToWinByThreeUpGoals}";
                    break;

                default:
                    Template.betTeam = string.Empty;
                    break;
            }
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = null;
            Template.betTeamClassName = Favorite;
        }
    }
}