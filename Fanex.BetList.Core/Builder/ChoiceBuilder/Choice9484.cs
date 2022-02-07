namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Entities;
    using Fanex.BetList.Core.Utils;
    using System.Collections.Generic;
    using System.Globalization;

    /// <summary>
    /// Home Over X Exact Runs.
    /// </summary>
    public class Choice9484 : Choice9434
    {
        private const string BetTeamLessThan = "0";
        private const string BetTeamGreater = "7";

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.betTeam = ticket.Handicap1.ToString(CultureInfo.InvariantCulture);
            if (ticket.BetTeam == BetTeamLessThan)
            {
                Template.betTeam = "< " + Template.betTeam;
            }

            if (ticket.BetTeam == BetTeamGreater)
            {
                Template.betTeam = "> " + Template.betTeam;
            }
        }
            

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = null;
            Template.betTeamClassName = Favorite;
        }
    }
}