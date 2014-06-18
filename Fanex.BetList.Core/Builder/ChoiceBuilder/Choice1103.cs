namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;

    /// <summary>
    /// LiveCasino - Live Baccarat.
    /// </summary>
    public class Choice1103 : Choice1101
    {
        protected override void BuildChoice(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            base.BuildChoice(ticket, ticketHelper, ticketData);

            Template.League.LeagueName.leagueName = string.Join(null, new string[] { CoreLang.LiveBaccarat, HtmlCharacters.NoneBreakingSpace, ticket.ShowTime });
        }
    }
}