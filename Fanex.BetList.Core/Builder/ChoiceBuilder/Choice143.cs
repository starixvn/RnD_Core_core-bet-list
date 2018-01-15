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
            var betTeam = ticket.BetTeam.ToLowerInvariant();

            // Bet team pattern length is always 2
            if (betTeam.Length == 2)
            {
                var totalGoal = betTeam[1].ToString() == BetTeamValue.O ? CoreBetList.over : CoreBetList.under;
                var teamName = string.Empty;

                switch (betTeam[0])
                {
                    case 'h':
                        teamName = CoreBetList.Home;
                        break;

                    case 'a':
                        teamName = CoreBetList.Away;
                        break;

                    case 'd':
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