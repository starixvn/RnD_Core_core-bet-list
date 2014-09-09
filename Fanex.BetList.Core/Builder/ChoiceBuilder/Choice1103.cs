namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Constants;
    using Entities;

    /// <summary>
    /// LiveCasino - Live Baccarat.
    /// </summary>
    public class Choice1103 : Choice1101
    {
        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
        {
            Template.League.LeagueName.leagueName = string.Join(null, new string[] { CoreBetList.LiveBaccarat, HtmlCharacters.NoneBreakingSpace, ticket.ShowTime });
        }
    }
}