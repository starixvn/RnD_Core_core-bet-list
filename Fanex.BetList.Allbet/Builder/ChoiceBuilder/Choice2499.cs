namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Entities;

    public class Choice2499 : Choice2401
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.betTeam = null;
        }
        public override string GetDisplayTableCode(ITicketHelper ticketHelper, string tableCode)
            => string.Empty;

        protected override bool IsDetailTicket(ITicket ticket)
            => !string.IsNullOrEmpty(ticket.MatchCode);
    }
}
