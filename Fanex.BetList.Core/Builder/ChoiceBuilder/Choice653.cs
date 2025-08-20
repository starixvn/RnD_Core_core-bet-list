namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Entities;
    using System.Collections.Generic;

    public class Choice653 : Choice653
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var teamName = ticket.TransDesc != null ? $"{ticketHelper.GetTeamNameById(ticket.TransDesc)}" : string.Empty;
            var betoption = $"<span class=\"favorite\">{ticket.BetTeam}</span>";
            Template.betTeam = $"{teamName} {betoption}";
        }
    }
}