namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Constants;
    using Entities;

    /// <summary>
    /// BAIO - FT Which team to score the highest quarter
    /// </summary>
    public class Choice603 : Choice1
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var betTeam = string.Empty;

            switch (ticket.BetTeam.ToLowerInvariant())
            {
                case BetTeamValue.H:
                    betTeam = Template.Match.homeTeam;
                    break;

                case BetTeamValue.A:
                    betTeam = Template.Match.awayTeam;
                    break;

                default:
                    betTeam = string.Empty;
                    break;
            }

            Template.betTeam = betTeam;
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            var isBetHomeTeam = IsBetHomeTeam(ticket);

            Template.Handicap.handicap = null;
            Template.betTeamClassName = isBetHomeTeam ? Favorite : Underdog;
        }
    }
}