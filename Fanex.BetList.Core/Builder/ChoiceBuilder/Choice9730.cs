using System.Collections.Generic;
using Fanex.BetList.Core.App_GlobalResources;
using Fanex.BetList.Core.Constants;
using Fanex.BetList.Core.Entities;

namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    public class Choice9730 : Choice9702
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var teamName = $"{ticketHelper.GetTeamNameById(ticket.TransDesc)} - ";
            var overUnder = BetTeamValue.O == ticket.BetTeam.ToLowerInvariant() ?
                $"<span class=\"favorite\">{CoreBetList.over}</span>" :
                CoreBetList.under;
            Template.betTeam = $"{teamName}{overUnder}";
        }
    }
}