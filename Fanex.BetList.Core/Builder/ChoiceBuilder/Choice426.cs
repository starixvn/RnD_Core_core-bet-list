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
            switch (ticket.BetTeam.ToLowerInvariant())
            {
                case BetTeamValue.H1:
                    Template.betTeam = $"{CoreBetList.Home} {CoreBetList.ToWinByOneGoal}";
                    break;

                case BetTeamValue.H2Over:
                    Template.betTeam = $"{CoreBetList.Home} {CoreBetList.ToWinByTwoUpGoals}";
                    break;

                case BetTeamValue.D:
                    Template.betTeam = CoreBetList.ScoreDraw;
                    break;

                case BetTeamValue.A1:
                    Template.betTeam = $"{CoreBetList.Away} {CoreBetList.ToWinByOneGoal}";
                    break;

                case BetTeamValue.A2Over:
                    Template.betTeam = $"{CoreBetList.Away} {CoreBetList.ToWinByTwoUpGoals}";
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