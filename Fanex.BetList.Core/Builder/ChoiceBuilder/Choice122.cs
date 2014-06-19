namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;

    /// <summary>
    /// Away No Bet.
    /// </summary>
    public class Choice122 : Choice121
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            string homeName = GetHomeTeamName(ticket, ticketHelper);
            bool betHomeTeam = ticket.BetTeam != null && ticket.BetTeam.Contains(BetTeamValue.H);
            var betTeam = betHomeTeam ? homeName : CoreLang.lblDraw;

            Template.betTeam = betTeam;
        }
    }
}