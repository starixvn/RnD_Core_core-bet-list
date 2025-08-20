namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Entities;
    using Fanex.BetList.Core.App_GlobalResources;
    using System.Collections.Generic;

    public class Choice675 : Choice653
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var betTeamModel = new BasketballPlayerModel(ticket.TransDesc, ticketHelper);
            var betTeam = CoreBetList.ResourceManager.GetString(betTeamModel.ResourceKey());
            betTeam = string.IsNullOrEmpty(betTeam) ? betTeamModel.ResourceKey() : betTeamModel.Format(betTeam);
            betTeam = $"<span class=\"favorite\">{betTeam}</span>";
            var playerName = ticketHelper.GetTeamNameById(betTeamModel.PlayerId);

            Template.betTeam = $"{playerName} {betTeam}";
        }
    }
}