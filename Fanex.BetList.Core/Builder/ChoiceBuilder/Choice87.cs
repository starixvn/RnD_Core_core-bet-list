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

            string betId = ticket.BetId.ToString();
            if (betId == "0")
            {
                betId = string.Empty;
            }

            string liveScore = (!ticket.IsLive || betId == string.Empty) ? " [0]" : string.Join(null, new string[] { " [", betId, "]" });

            string betTeam = ticket.BetTeam == "h" ? CoreBetList.high : CoreBetList.low;            

            this.Template.betTeam = string.Join(null, new string[] { betTeam, liveScore });

            this.Template.betTeamClassName = BLFAVORITE;            
            this.Template.Match.homeTeam = string.Join(null, new string[] { CoreBetList.numbergameno, "&nbsp;", ticket.MatchCode });
        }

        static object[] BetTeamNames = 
        {
            // ticket.Live, betId, ticket.BetTeam, expectedValues
            new object[] {true, 0, "h", string.Join(null, CoreBetList.high, " [0]")},
            new object[] {false, 0, "h", string.Join(null, CoreBetList.high, " [0]")},            
            new object[] {true, 1, "h", string.Join(null, CoreBetList.high, " [1]")},
            new object[] {false, 1, "h", string.Join(null, CoreBetList.high, " [0]")},
            new object[] {true, 0, "is not h", string.Join(null, CoreBetList.low, " [0]")},
            new object[] {false, 0, "is not h", string.Join(null, CoreBetList.low, " [0]")},
            new object[] {true, 1, "is not h", string.Join(null, CoreBetList.low, " [1]")},
            new object[] {false, 1, "is not h", string.Join(null, CoreBetList.low, " [0]")},
        };
    }
}