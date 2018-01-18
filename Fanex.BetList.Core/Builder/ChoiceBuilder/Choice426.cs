namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;

    /// <summary>
    /// Bet type name: 1H Winning Margin
    /// </summary>
    public class Choice426 : Choice1
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var betTeam = string.Empty;

            switch (ticket.BetTeam.ToLowerInvariant())
            {
                case BetTeamValue.H1:
                    betTeam = string.Format("{0} {1}", CoreBetList.Home171, CoreBetList.ToWinByOneGoal);
                    break;

                case BetTeamValue.H2Plus:
                    betTeam = string.Format("{0} {1}", CoreBetList.Home171, CoreBetList.ToWinByTwoUpGoals);
                    break;

                case BetTeamValue.D:
                    betTeam = CoreBetList.ScoreDraw;
                    break;

                case BetTeamValue.A1:
                    betTeam = string.Format("{0} {1}", CoreBetList.Away171, CoreBetList.ToWinByOneGoal);
                    break;

                case BetTeamValue.A2Plus:
                    betTeam = string.Format("{0} {1}", CoreBetList.Away171, CoreBetList.ToWinByTwoUpGoals);
                    break;

                case BetTeamValue.NG:
                    betTeam = CoreBetList.NoGoal408;
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