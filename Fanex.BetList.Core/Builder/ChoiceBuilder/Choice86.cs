namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;

    /// <summary>
    /// Next Odd/Even.
    /// </summary>
    public class Choice86 : Choice2
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

            Template.Score = null;
            Template.Match.VS = null;

            string liveScore = string.Empty;
            string betTeam = Template.betTeam;

            string bettypeName = Template.BetType.betTypeName;
            if (!ticket.IsLive)
            {
                bettypeName = string.Join(null, new string[] { CoreBetList.ft, ". ", bettypeName });
            }
            else
            {
                string betId = ticket.BetId.ToString();
                if (betId == "0")
                {
                    betId = string.Empty;
                }

                bettypeName = CoreBetList.next + HtmlCharacters.NoneBreakingSpace + bettypeName;
                liveScore = (!ticket.IsLive || betId == string.Empty) ? " [0]" : string.Join(null, new string[] { " [", betId, "]" });
            }

            Template.betTeam = betTeam + liveScore;
            Template.BetType.betTypeName = bettypeName;

            Template.Match.homeTeam = string.Join(null, new string[] { CoreBetList.numbergameno, HtmlCharacters.NoneBreakingSpace, ticket.MatchCode });

            // Bet team name is always in red for this bettype
            Template.betTeamClassName = Favorite;
        }
    }
}