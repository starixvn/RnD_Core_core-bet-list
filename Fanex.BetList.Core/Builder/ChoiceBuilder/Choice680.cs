namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Entities;
    using System.Collections.Generic;

    public class Choice680 : Choice1
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var playerIds = ticket.TransDesc.Split(',');
            
            if (playerIds.Length >= 2)
            {
                var playerId1 = playerIds[0];
                var playerName1 = ticketHelper.GetTeamNameById(playerId1);
                var playerId2 = playerIds[1];
                var playerName2 = ticketHelper.GetTeamNameById(playerId2);
                var betchoice = $"{playerName1} & {playerName2}";
                var playerOption = $"<span class=\"favorite\">{playerName1}</span>";

                Template.betTeam = $"{betchoice} - {playerOption}";
            }
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.betTeamClassName = Underdog;
            Template.Handicap.handicap = null;
        }
    }
}