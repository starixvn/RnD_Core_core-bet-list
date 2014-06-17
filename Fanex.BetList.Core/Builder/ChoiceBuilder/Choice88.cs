namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;

    /// <summary>
    /// Type: Warrior.
    /// </summary>
    public class Choice88 : Choice1
    {
        /// <summary>
        /// Builds the choice.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        /// <param name="ticketData">The ticket data.</param>
        protected override void BuildChoice(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            base.BuildChoice(ticket, ticketHelper, ticketData);

            this.Template.Score = null;
            this.Template.Match.VS = null;
            this.Template.Handicap.handicap = null;
            this.Template.betTeam = ticket.BetTeam == HToLower ? CoreBetList.warrior2ndBall : CoreBetList.warrior3rdBall;
            this.Template.betTeamClassName = BLFAVORITE;

            this.Template.Match.homeTeam = string.Join(null, new string[] { CoreBetList.numbergameno, HtmlNoneBreakingSpace, ticket.MatchCode });
        }
    }
}