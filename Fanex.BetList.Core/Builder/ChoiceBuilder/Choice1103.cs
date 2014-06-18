namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Constants;

    /// <summary>
    /// LiveCasino - Live Baccarat.
    /// </summary>
    public class Choice1103 : Choice1101
    {
        protected override void BuildChoice(Entities.ITicket ticket, Entities.ITicketHelper ticketHelper, List<Entities.ITicketData> ticketData)
        {
            base.BuildChoice(ticket, ticketHelper, ticketData);

            this.Template.League.LeagueName.leagueName = string.Join(null, new string[] { CoreLang.LiveBaccarat, HtmlCharacters.NoneBreakingSpace, ticket.ShowTime });
        }
    }
}