namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;

    /// <summary>
    /// Away No Bet.
    /// </summary>
    public class Choice122 : Choice121
    {
        protected override string GetBetTeam(ITicket ticket, ITicketHelper ticketHelper)
        {
            string homeName = base.GetHomeTeamName(ticket, ticketHelper);
            bool betHomeTeam = ticket.BetTeam != null && ticket.BetTeam.Contains("h");
            var betTeam = betHomeTeam ? homeName : CoreLang.lblDraw;

            return betTeam;
        }
    }
}