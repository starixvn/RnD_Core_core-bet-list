namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;

    /// <summary>
    /// Bet type name: Exact 2nd Half Goal
    /// </summary>
    public class Choice429 : Choice1
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var betTeam = string.Empty;

            switch (ticket.BetTeam.ToLowerInvariant())
            {
                case BetTeamValue.NumberZero:
                    betTeam = CoreBetList.nogoal;
                    break;

                case BetTeamValue.NumberOne:
                    betTeam = CoreBetList.OneGoal;
                    break;

                case BetTeamValue.NumberTwo:
                    betTeam = CoreBetList.TwoGoals;
                    break;

                case BetTeamValue.ThreeOver:
                    betTeam = CoreBetList.ThreeOver;
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