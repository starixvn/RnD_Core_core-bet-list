namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Entities;
    using Fanex.BetList.Core.Utils;

    public class Choice11054 : Choice11050
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var awayTeamName = ticketHelper.GetTeamName3rd(ticket.AwayId.ToString());
            var homeTeamName = ticketHelper.GetTeamName3rd(ticket.HomeId.ToString());

            var isBetHomeTeam = IsBetHomeTeam(ticket);
            Template.betTeam = isBetHomeTeam ? homeTeamName : awayTeamName;
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            var isBetHomeTeam = IsBetHomeTeam(ticket);

            string betTeamClassName;

            if (isBetHomeTeam)
            {
                betTeamClassName = ticket.Handicap2 >= ticket.Handicap1 ? Underdog : Favorite;
            }
            else
            {
                betTeamClassName = ticket.Handicap2 <= ticket.Handicap1 ? Underdog : Favorite;
            }

            Template.betTeamClassName = betTeamClassName;
            

            decimal handicap = ticket.Handicap2 > ticket.Handicap1 ? ticket.Handicap2 : ticket.Handicap1;
            handicap = betTeamClassName == Favorite ? -handicap : handicap;

            Template.Handicap.handicap = ConvertByBetType.Hdp(handicap);
        }
    }
}