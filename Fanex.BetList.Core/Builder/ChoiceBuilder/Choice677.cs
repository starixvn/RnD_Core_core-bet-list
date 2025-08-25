namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Entities;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Constants;
    using System.Collections.Generic;

    public class Choice677 : Choice653
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var playerInfo = new BasketballPlayerInfo(ticket.TransDesc);
            var yesNo = !string.IsNullOrEmpty(ticket.BetTeam) && BetTeamValue.N == ticket.BetTeam.ToLowerInvariant() ? 
                CoreBetList.lblNo : 
                CoreBetList.lblYes;

            Template.betTeam = $"{ticketHelper.GetTeamNameById(playerInfo.PlayerId)} ({ticketHelper.GetTeamNameById(playerInfo.TeamId)}) - <span class=\"favorite\">{yesNo}</span>";
        }
    }
}