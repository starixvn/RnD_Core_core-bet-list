namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;

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