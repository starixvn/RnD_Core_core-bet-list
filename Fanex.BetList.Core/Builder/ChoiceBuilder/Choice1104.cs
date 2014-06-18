namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;

    /// <summary>
    /// Live Casino - Live Roulette.
    /// </summary>
    public class Choice1104 : Choice1102
    {
        protected override void BuildChoice(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            base.BuildChoice(ticket, ticketHelper, ticketData);

            Template.League.LeagueName.leagueName = string.Join(null, new string[] { CoreLang.LiveRoulette, HtmlCharacters.NoneBreakingSpace, ticket.ShowTime });
        }
    }
}