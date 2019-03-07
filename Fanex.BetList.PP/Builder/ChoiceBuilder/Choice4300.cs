namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Entities;

    public class Choice4300 : Choice4101
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.betTeam =
                $"<span class=\"handicap\">{ticketHelper.GetResourceData(GameTypeRefId, ticket.TransDesc)}</span>";

            if (!string.IsNullOrEmpty(ticket.BetCheck)
                    && !ticket.Status.ToLowerInvariant().Equals(BetStatus.Won))
            {
                Template.betTeam += " " + ticket.BetCheck;
            }
        }
    }
}