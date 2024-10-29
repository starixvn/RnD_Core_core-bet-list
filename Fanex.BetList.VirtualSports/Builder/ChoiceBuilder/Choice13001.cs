namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;

    /// <summary>
    /// Betby - Double Chance
    /// </summary>
    public class Choice13001 : Choice13000
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

                Template.betTeam = $"{firstChance} {CoreBetList.lblOr} {secondChance}";
            }
            else
            {
                Template.betTeam = string.Empty;
            }
        }
    }
}