namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;

    /// <summary>
    /// Next Combo
    /// do NOT show odds type because this bet type just offer for DEC
    /// it supports for live ball, so always 37.5.
    /// </summary>
    public class Choice89 : Choice1
    {
        private const string OneColonOne = "1:1";
        private const string OneColonTwo = "1:2";
        private const string TwoColonOne = "2:1";
        private const string TwoColonTwo = "2:2";
        private const string SpanWithValue37point5 = " <span style='color:#555555'>37.5</span> ";
        private const string ZeroInSquareBrackets = " [0]";

        /// <summary>
        ///  Get Bet team by ticket and live score status.
        /// </summary>
        /// <param name="ticket"> Valid ticket. </param>
        /// <param name="liveScore"> Live score. </param>
        /// <returns>Return string with format betTeam + liveScore.</returns>
        public string GetBetTeam(ITicket ticket, string liveScore)
        {
            string betTeam = string.Empty;
            betTeam = GetBetTeamFromResource(ticket);

            betTeam += SpanWithValue37point5;
            return betTeam + liveScore;
        }

        /// <summary>
        ///  Get bet team name from resource.
        /// </summary>
        /// <param name="ticket"> A valid ticket. </param>
        /// <returns>Resource string value.</returns>
        public virtual string GetBetTeamFromResource(ITicket ticket)
        {
            switch (ticket.BetTeam)
            {
                case OneColonOne:
                    return CoreBetList.lblOverOdd;                    
                case OneColonTwo:
                    return CoreBetList.lblOverEven;                    
                case TwoColonOne:
                    return CoreBetList.lblUnderOdd;                    
                case TwoColonTwo:
                    return CoreBetList.lblUnderEven;                    
            }

            return string.Empty;
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

            string betTeam = GetBetTeam(ticket, liveScore);

            this.Template.betTeam = betTeam;
            this.Template.betTeamClassName = BLFAVORITE;
            
            this.Template.Match.homeTeam = string.Join(null, new string[] { CoreBetList.numbergameno, "&nbsp;", ticket.MatchCode });
        }       
    }
}