namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Constants;
    using Entities;

    /// <summary>
    /// Match Correct Score.
    /// </summary>
    public class Choice1302 : Choice1
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var betTeam = string.Empty;
            if (ticket.BetTeam.Length == 2)
            {
                betTeam = string.Join(null, new string[] { ticket.BetTeam.Substring(0, 1), ":", ticket.BetTeam.Substring(1, 1) });
            }

            Template.betTeam = string.Join(null, new string[] { HtmlCharacters.NoneBreakingSpace, betTeam });
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.betTeamClassName = Favorite;
            Template.Handicap.handicap = null;
        }
    }
}