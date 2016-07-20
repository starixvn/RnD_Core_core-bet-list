namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Constants;
    using Entities;
    using System.Collections.Generic;

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
                    betTeam = string.Format("{0} {1}", Template.Match.homeTeam, CoreBetList.ToWinByOneGoal);
                    break;

                case BetTeamValue.H2Plus:
                    betTeam = string.Format("{0} {1}", Template.Match.homeTeam, CoreBetList.ToWinByTwoUpGoals);
                    break;

                case BetTeamValue.D:
                    betTeam = CoreBetList.ScoreDraw;
                    break;

                case BetTeamValue.A1:
                    betTeam = string.Format("{0} {1}", Template.Match.awayTeam, CoreBetList.ToWinByOneGoal);
                    break;

                case BetTeamValue.A2Plus:
                    betTeam = string.Format("{0} {1}", Template.Match.awayTeam, CoreBetList.ToWinByTwoUpGoals);
                    break;

                case BetTeamValue.NG:
                    betTeam = CoreBetList.nogoal;
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