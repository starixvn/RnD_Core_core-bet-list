namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;

    /// <summary>
    /// 1st ball OU.
    /// </summary>
    public class Choice81 : Choice1
    {
        private const string HandicapValue = "37.5";

        /// <summary>
        /// Builds the choice.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        /// <param name="ticketData">The ticket data.</param>
        protected override void BuildChoice(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            base.BuildChoice(ticket, ticketHelper, ticketData);

            Template.Match.VS = null;
            Template.betTeam = ticket.BetTeam == BetTeamValue.H ? CoreBetList.over : CoreBetList.under;

            Template.betTeamClassName = Favorite;

            Template.Handicap.handicap = HandicapValue;
            Template.Match.homeTeam = string.Join(null, new string[] { CoreBetList.numbergameno, HtmlCharacters.NoneBreakingSpace, ticket.MatchCode });
        }
    }
}