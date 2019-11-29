namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;

    /// <summary>
    /// Pool House All In One Phase 5: Extra Time Winning Margin.
    /// </summary>
    public class Choice397 : Choice1
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

                case BetTeamValue.H3Over:
                    Template.betTeam = $"{CoreBetList.Home} {CoreBetList.ToWinByThreeUpGoals}";
                    break;

                case BetTeamValue.D:
                    Template.betTeam = CoreBetList.ScoreDraw;
                    break;

                case BetTeamValue.A1:
                    Template.betTeam = $"{CoreBetList.Away} {CoreBetList.ToWinByOneGoal}";
                    break;

                case BetTeamValue.A2:
                    Template.betTeam = $"{CoreBetList.Away} {CoreBetList.ToWinByTwoGoals}";
                    break;

                case BetTeamValue.A3Over:
                    Template.betTeam = $"{CoreBetList.Away} {CoreBetList.ToWinByThreeUpGoals}";
                    break;

                case BetTeamValue.NG:
                    Template.betTeam = CoreBetList.NoGoal408;
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