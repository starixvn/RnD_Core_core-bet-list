namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Constants;
    using Entities;

    /// <summary>
    /// Correct Score.
    /// </summary>
    public class Choice4 : Choice1
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            string choice = (ticket.BetTeam == "0:5" || ticket.BetTeam == "5:0") ? ticket.BetTeam + " UP" : ticket.BetTeam;

            // "&nbsp" because export format
            Template.betTeam = string.Join(null, new string[] { HtmlCharacters.NoneBreakingSpace, choice });
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = null;
            Template.betTeamClassName = Favorite;
        }
    }
}