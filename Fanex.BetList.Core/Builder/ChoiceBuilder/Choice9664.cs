namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Entities;
    using System.Collections.Generic;

    /// <summary>
    /// Home 1st Inns Next Man Out
    /// </summary>
    public class Choice9664 : Choice1
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var players = ticket.TransDesc.Split(',');
            if (players.Length >= 1)
            {
                Template.betTeam = ticketHelper.GetTeamNameById(players[0]);
            }

        }

        protected override void BuildPlayers(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData = null)
        {
            const int NumberOfTeams = 2;
            var players = ticket.TransDesc.Split(',');
            if (players.Length >= NumberOfTeams)
            {
                Template.Players.Visible = true;
                Template.Players.homePlayer = ticketHelper.GetTeamNameById(players[0]);
                Template.Players.awayPlayer = ticketHelper.GetTeamNameById(players[1]);
            }
        }
    }
}