namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Entities;
    using System.Collections.Generic;
    using System.Globalization;

    /// <summary>
    /// Home Over X Ball Y Exact Runs
    /// </summary>
    public class Choice9490 : Choice9434
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
            => Template.betTeam = ticket.BetTeam.ToUpperInvariant();

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = null;
            Template.betTeamClassName = Favorite;
        }
    }
}