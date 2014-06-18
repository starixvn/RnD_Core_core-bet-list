namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Constants;
    using Entities;

    /// <summary>
    /// Draw no draw.
    /// </summary>
    public class Choice123 : Choice121
    {
        protected override string GetBetTeam(ITicket ticket, ITicketHelper ticketHelper)
        {
            bool betHomeTeam = ticket.BetTeam == BetTeamValue.H;
            var betTeam = betHomeTeam ? CoreLang.lblDraw : CoreLang.lblNoDraw;

            return betTeam;
        }
    }
}