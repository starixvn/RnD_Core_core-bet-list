namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;

    /// <summary>
    ///  Progressive Jackpot
    /// </summary>
    public class Choice2199 : Choice2101
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            base.BuildBetTeam(ticket, ticketHelper, ticketData);

            if (!string.IsNullOrEmpty(ticket.BetCheck))
            {
                Template.betTeam += $"<div>{CoreBetList.forRefNo}: {ticket.BetdaqId}</div>";
            }
        }
    }
}