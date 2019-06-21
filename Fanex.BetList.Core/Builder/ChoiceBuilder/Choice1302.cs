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
            const int expectedBetTeamLength = 2;

            Template.betTeam = ticket.BetTeam.Length == expectedBetTeamLength
                ? $"{HtmlCharacters.NoneBreakingSpace}{ticket.BetTeam[0]}:{ticket.BetTeam[1]}"
                : HtmlCharacters.NoneBreakingSpace;
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.betTeamClassName = Favorite;
            Template.Handicap.handicap = null;
        }
    }
}