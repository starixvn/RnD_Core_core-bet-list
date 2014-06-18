namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Constants;

    /// <summary>
    /// Live Casino - Live Roulette.
    /// </summary>
    public class Choice1104 : Choice1102
    {
        protected override void BuildChoice(Entities.ITicket ticket, Entities.ITicketHelper ticketHelper, List<Entities.ITicketData> ticketData)
        {
            base.BuildChoice(ticket, ticketHelper, ticketData);

            this.Template.League.LeagueName.leagueName = string.Join(null, new string[] { CoreLang.LiveRoulette, HtmlCharacters.NoneBreakingSpace, ticket.ShowTime });
        }
    }
}