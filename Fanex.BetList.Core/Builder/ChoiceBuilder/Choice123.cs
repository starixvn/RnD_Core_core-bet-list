namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;

    /// <summary>
    /// Draw no draw.
    /// </summary>
    public class Choice123 : Choice121
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            bool betHomeTeam = ticket.BetTeam == BetTeamValue.H;
            var betTeam = betHomeTeam ? CoreBetList.lblDraw : CoreBetList.lblNoDraw;

            Template.betTeam = betTeam;
        }
    }
}