namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Entities;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Utils;
    using System.Collections.Generic;

    /// <summary>
    /// Home 1st Inns Next Man Out
    /// </summary>
    public class Choice9664 : Choice1
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            const int NumberOfTeams = 2;
            var players = ticket.TransDesc.Split(',');
            if (players.Length >= 1)
            {
                Template.betTeam = ticketHelper.GetTeamNameById(players[0]);
                if (players.Length >= NumberOfTeams)
                {
                    Template.betTeam += $"<div class='match'>{ticketHelper.GetTeamNameById(players[0])} - vs - {ticketHelper.GetTeamNameById(players[1])}";
                }
            }

        }
    }
}