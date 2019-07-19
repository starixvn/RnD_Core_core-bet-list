namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;

    /// <summary>
    /// Pool House All In One: Winning Margin.
    /// Its parent bet type is Winning Margin - bet type ID 171 of Bet Radar.
    /// </summary>
    public class Choice408 : Choice1
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            switch (ticket.BetTeam.ToLowerInvariant())
            {
                case BetTeamValue.H1:
                    Template.betTeam = $"{CoreBetList.Home171} {CoreBetList.ToWinByOneGoal}";
                    break;

                case BetTeamValue.H2:
                    Template.betTeam = $"{CoreBetList.Home171} {CoreBetList.ToWinByTwoGoals}";
                    break;

                case BetTeamValue.H3:
                    Template.betTeam = $"{CoreBetList.Home171} {CoreBetList.ToWinByThreeUpGoals}";
                    break;

                case BetTeamValue.D:
                    Template.betTeam = CoreBetList.ScoreDraw;
                    break;

                case BetTeamValue.A1:
                    Template.betTeam = $"{CoreBetList.Away171} {CoreBetList.ToWinByOneGoal}";
                    break;

                case BetTeamValue.A2:
                    Template.betTeam = $"{CoreBetList.Away171} {CoreBetList.ToWinByTwoGoals}";
                    break;

                case BetTeamValue.A3:
                    Template.betTeam = $"{CoreBetList.Away171} {CoreBetList.ToWinByThreeUpGoals}";
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