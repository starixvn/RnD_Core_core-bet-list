namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.App_GlobalResources;
    using Entities;
    using Templates;
    
    public class Choice11051 : Choice11050
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            const int expectedBetTeamLength = 2;

            if (!string.IsNullOrEmpty(ticket.BetTeam) && ticket.BetTeam.Length == expectedBetTeamLength)
            {
                var betTeam = ticket.BetTeam.ToLowerInvariant();
                var firstChanceIndicator = betTeam[0].ToString();
                var secondChanceIndicator = betTeam[1].ToString();
                var firstChance = BetTeamValue.NumberOne == firstChanceIndicator ? CoreBetList.Home : CoreBetList.Away;
                var secondChance = BetTeamValue.X == secondChanceIndicator ? CoreBetList.draw : CoreBetList.Away;

                Template.betTeam = $"{firstChance}<span class=\"{Underdog}\"> {CoreBetList.lblOr} </span>{secondChance}";
            }
            else
            {
                Template.betTeam = string.Empty;
            }
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.betTeamClassName = Favorite;
            Template.Handicap.handicap = null;
        }
    }
}