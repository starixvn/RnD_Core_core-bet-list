namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;

    /// <summary>
    /// Next Over/Under
    /// it supports live ball, so always 37.5.
    /// </summary>
    public class Choice85 : Choice3
    {
        /// <summary>
        /// Builds the bet team.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        /// <param name="ticketData">The ticket data.</param>
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            base.BuildBetTeam(ticket, ticketHelper, ticketData);

            string betId = ticket.BetId.ToString();
            if (betId == "0")
            {
                betId = string.Empty;
            }

            string liveScore = string.Empty;
            liveScore = (!ticket.IsLive || string.IsNullOrEmpty(betId)) ? " [0]" : string.Join(null, new string[] { " [", betId, "]" });

            string betTeam = Template.betTeam;
            betTeam += " <span style='color:#555555'>37.5</span> ";

            Template.betTeam = betTeam + liveScore;
        }

        /// <summary>
        /// Builds the type of the bet.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        /// <param name="ticketData">The ticket data.</param>
        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            base.BuildBetType(ticket, ticketHelper, ticketData);

            string bettypeName = Template.BetType.betTypeName;
            if (!ticket.IsLive)
            {
                bettypeName = string.Join(null, new string[] { CoreBetList.ft, ". ", bettypeName });
            }
            else
            {
                bettypeName = string.Join(null, new string[] { CoreBetList.next, HtmlCharacters.NoneBreakingSpace, bettypeName });
            }

            Template.BetType.betTypeName = bettypeName;
        }

        protected override void BuildScore(ITicket ticket)
        {
            Template.Score = null;
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = null;
            Template.betTeamClassName = Favorite;
        }

        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
        {
            base.BuildMatch(ticket, ticketHelper);

            Template.Match.VS = null;
            Template.Match.homeTeam = string.Join(null, new string[] { CoreBetList.numbergameno, HtmlCharacters.NoneBreakingSpace, ticket.MatchCode });
        }
    }
}