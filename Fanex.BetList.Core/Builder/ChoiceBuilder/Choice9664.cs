namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Entities;
    using Fanex.BetList.Core.Constants;
    using System.Collections.Generic;

    /// <summary>
    /// Home 1st Inns Next Man Out
    /// </summary>
    public class Choice9664 : Choice1
    {
        private const int NumberOfTeams = 2;
        private const string PlayerChoiceClass = "players-choice";
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var players = ticket.TransDesc.Split(',');
            if (players.Length >= 1)
            {
                Template.betTeam = ticketHelper.GetTeamNameById(players[0]).Replace(" ", HtmlCharacters.NoneBreakingSpace);

                if (players.Length >= NumberOfTeams)
                {
                    var choicePlayers = string.Join(null, ticketHelper.GetTeamNameById(players[0]), " & ", ticketHelper.GetTeamNameById(players[1]), " - ");
                    Template.betTeam = string.Join(null, choicePlayers, "<span class=\"favorite\">", Template.betTeam, "</span>");
                }
            }
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = null;
            Template.betTeamClassName = PlayerChoiceClass;
        }

        protected override void AdjustBetTeamToRTF()
        {
            base.AdjustBetTeamToRTF();
            
            Template.betTeam = Template.betTeam.Replace("<span class=\"players-choice\">", string.Empty);
        }
    }
}