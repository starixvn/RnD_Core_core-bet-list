namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;

    /// <summary>
    /// Next Over/Under
    /// it supports live ball, so always 37.5.
    /// </summary>
    public class Choice85 : Choice3
    {
        private const string ZeroInSquareBrackets = " [0]";
        private const string SpanWithValue37point5 = " <span style='color:#555555'>37.5</span> ";
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
            this.Template.betTeam += GetBetTeam(GetLiveScore(ticket));
            this.Template.betTeamClassName = BLFAVORITE;            
            this.Template.BetType.betTypeName = GetBetTypeName(ticket);
            this.Template.Match.homeTeam = string.Join(null, 
                new string[] { CoreBetList.numbergameno, HtmlNoneBreakingSpace, ticket.MatchCode });
        }

        private static string GetBetTeam(string liveScore)
        {
            return SpanWithValue37point5 + liveScore;
        }

        private string GetBetTypeName(ITicket ticket)
        {
            string bettypeName = this.Template.BetType.betTypeName;
            if (!ticket.IsLive)
            {
                bettypeName = string.Join(null, new string[] { CoreBetList.ft, ". ", bettypeName });
            }
            else
            {
                bettypeName = string.Join(null, new string[] { CoreBetList.next, HtmlNoneBreakingSpace, bettypeName });
            }
            return bettypeName;
        }

        /// <summary>
        ///  Get live score string by combining live status and bet id.
        /// </summary>
        /// <param name="ticket"> Valid ticket.</param>
        /// <returns> Live score string.</returns>
        public virtual string GetLiveScore(ITicket ticket)
        {
            string liveScore = (!ticket.IsLive || !ticket.BetId.HasValue)
                ? ZeroInSquareBrackets
                : string.Join(null, new string[] { " [", ticket.BetId.ToString(), "]" });

            return liveScore;
        }
    }
}