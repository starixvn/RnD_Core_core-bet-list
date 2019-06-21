namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;
    using Utils;

    /// <summary>
    /// Bet Type: First Half Result/Total Goal - 143.
    /// </summary>
    public class Choice143 : Choice1
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            const int expectedBetTeamLength = 2;

            if (ticket.BetTeam.Length == expectedBetTeamLength)
            {
                var betTeam = ticket.BetTeam.ToLowerInvariant();
                var totalGoalIndicator = betTeam[1].ToString();
                var teamIndicator = betTeam[0].ToString();
                var totalGoal = totalGoalIndicator == BetTeamValue.O ? CoreBetList.over : CoreBetList.under;
                var teamName = string.Empty;

                switch (teamIndicator)
                {
                    case BetTeamValue.H:
                        teamName = CoreBetList.Home;
                        break;

                    case BetTeamValue.A:
                        teamName = CoreBetList.Away;
                        break;

                    case BetTeamValue.D:
                        teamName = CoreBetList.draw;
                        break;

                    default:
                        teamName = string.Empty;
                        break;
                }

                Template.betTeam = $"{teamName}/{totalGoal}";
            }
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.betTeamClassName = Favorite;
            Template.Handicap.handicap = ConvertByBetType.Hdp(ticket.Handicap1);
        }
    }
}