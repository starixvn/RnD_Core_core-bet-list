namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;

    /// <summary>
    /// Next High/Low.
    /// </summary>
    public class Choice87 : Choice1
    {
        private const string ZeroInSquareBrackets = " [0]";
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
         
            string liveScore = GetLiveScore(ticket);
            this.Template.betTeam = GetBetTeam(ticket, liveScore);
            this.Template.betTeamClassName = BLFAVORITE;
            

            this.Template.Match.homeTeam = string.Join(null, new string[] { CoreBetList.numbergameno, HtmlNoneBreakingSpace, ticket.MatchCode });
        }

        public string GetBetTeam(ITicket ticket, string liveScore)
        {
            string betTeam = ticket.BetTeam == HToLower ? CoreBetList.high : CoreBetList.low;
            return string.Join(null, new string[] { betTeam, liveScore });
        }

        /// <summary>
        ///  Get live score string by combining live status and bet id.
        /// </summary>
        /// <param name="ticket"> Valid ticket.</param>
        /// <returns> Live score string.</returns>
        public virtual string GetLiveScore(ITicket ticket)
        {
            string liveScore = (!ticket.IsLive || ticket.BetId.HasValue)
                ? ZeroInSquareBrackets
                : string.Join(null, new string[] { " [", ticket.BetId.ToString(), "]" });

            return liveScore;
        }
    }
}