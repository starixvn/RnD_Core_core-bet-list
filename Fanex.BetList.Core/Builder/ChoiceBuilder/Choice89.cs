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
        /// <summary>
        ///  Get Bet team by ticket and live score status.
        /// </summary>
        /// <param name="ticket"> Valid ticket. </param>
        /// <param name="liveScore"> Live score. </param>
        /// <returns>Return string with format betTeam + liveScore.</returns>
        protected static string GetBetTeam(ITicket ticket, string liveScore)
        {
            string betTeam = string.Empty;
            switch (ticket.BetTeam)
            {
                case "1:1": betTeam = CoreBetList.lblOverOdd;
                    break;
                case "1:2": betTeam = CoreBetList.lblOverEven;
                    break;
                case "2:1": betTeam = CoreBetList.lblUnderOdd;
                    break;
                case "2:2": betTeam = CoreBetList.lblUnderEven;
                    break;
            }

            betTeam += " <span style='color:#555555'>37.5</span> ";
            return betTeam + liveScore;
        }

        /// <summary>
        ///  Get live score string by combining live status and bet id.
        /// </summary>
        /// <param name="ticket"> Valid ticket.</param>
        /// <returns> Live score string.</returns>
        protected static string GetLiveScore(ITicket ticket)
        {
            string betId = ticket.BetId.ToString();
            if (betId == "0")
            {
                betId = string.Empty;
            }

            string liveScore = (!ticket.IsLive || betId == string.Empty) ? " [0]" : string.Join(null, new string[] { " [", betId, "]" });

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