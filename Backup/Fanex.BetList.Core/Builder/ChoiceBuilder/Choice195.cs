namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;

    /// <summary>
    /// Bet type name: Home Team Exact Corners.
    /// </summary>
    public class Choice195 : Choice1
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var isSevenOver = ticket.BetTeam.ToLowerInvariant() == BetTeamValue.SevenOver;

            Template.betTeam = isSevenOver ? CoreBetList.SevenAndOverCorners : string.Format("{0} {1}", ticket.BetTeam, CoreBetList.Corners);
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = null;
            Template.betTeamClassName = Favorite;
        }
    }
}