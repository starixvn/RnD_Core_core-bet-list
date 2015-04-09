namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Constants;
    using Entities;

    /// <summary>
    /// Live Casino - Live Roulette.
    /// </summary>
    public class Choice1104 : Choice1102
    {
        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
        {
            Template.League.LeagueName.leagueName = string.Join(null, new string[] { CoreBetList.LiveRoulette, HtmlCharacters.NoneBreakingSpace, ticket.ShowTime });
        }
    }
}