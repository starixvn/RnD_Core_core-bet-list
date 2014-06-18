namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Constants;
    using Entities;

    /// <summary>
    /// Away No Bet.
    /// </summary>
    public class Choice122 : Choice121
    {
        protected override string GetBetTeam(ITicket ticket, ITicketHelper ticketHelper)
        {
            string homeName = GetHomeTeamName(ticket, ticketHelper);
            bool betHomeTeam = ticket.BetTeam != null && ticket.BetTeam.Contains(BetTeamValue.H);
            var betTeam = betHomeTeam ? homeName : CoreLang.lblDraw;

            return betTeam;
        }
    }
}