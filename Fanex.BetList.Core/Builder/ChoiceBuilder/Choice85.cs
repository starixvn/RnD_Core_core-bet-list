namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;

    /// <summary>
    /// Next Over/Under
    /// it supports live ball, so always 37.5.
    /// </summary>
    public class Choice85 : Choice3
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

            string liveScore = string.Empty;
            liveScore = (!ticket.IsLive || betId == string.Empty) ? " [0]" : string.Join(null, new string[] { " [", betId, "]" });

            string betTeam = this.Template.betTeam;
            betTeam += " <span style='color:#555555'>37.5</span> ";
            string bettypeName = this.Template.BetType.betTypeName;
            if (!ticket.IsLive)
            {
                bettypeName = string.Join(null, new string[] { CoreBetList.ft, ". ", bettypeName });
            }
            else
            {
                bettypeName = string.Join(null, new string[] { CoreBetList.next, HtmlCharacter.NoneBreakingSpace, bettypeName });
            }

            this.Template.betTeam = betTeam + liveScore;
            this.Template.betTeamClassName = BLFAVORITE;

            this.Template.BetType.betTypeName = bettypeName;
            this.Template.Match.homeTeam = string.Join(null, new string[] { CoreBetList.numbergameno, HtmlCharacter.NoneBreakingSpace, ticket.MatchCode });
        }
    }
}