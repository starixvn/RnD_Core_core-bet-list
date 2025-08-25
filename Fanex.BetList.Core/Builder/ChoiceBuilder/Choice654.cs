namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Entities;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Constants;
    using System.Collections.Generic;

    public class Choice654 : Choice653
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var playerInfo = new BasketballPlayerInfo(ticket.TransDesc);
            var overUnder = BetTeamValue.O == ticket.BetTeam.ToLowerInvariant() ?
               $"<span class=\"favorite\">{CoreBetList.over}</span>" :
               CoreBetList.under;

            Template.betTeam = $"{ticketHelper.GetTeamNameById(playerInfo.PlayerId)} ({ticketHelper.GetTeamNameById(playerInfo.TeamId)}) - {overUnder}";
        }
    }
}