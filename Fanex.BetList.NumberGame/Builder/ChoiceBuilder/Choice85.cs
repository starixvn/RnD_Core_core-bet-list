namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;
    using Resources;

    /// <summary>
    /// Next Over/Under
    /// it supports live ball, so always 37.5.
    /// </summary>
    public class Choice85 : Choice81
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

            var betId = ticket.BetId.ToString() == "0" ? string.Empty : ticket.BetId.ToString();
            var liveScore = (!ticket.IsLive || string.IsNullOrEmpty(betId)) ? " [0]" : $" [{betId}]";

            Template.betTeam += liveScore;
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

            var bettypeName = !ticket.IsLive
                ? $"{CoreBetList.ft}. {Template.BetType.betTypeName}"
                : $"{NumberGameLang.next}{HtmlCharacters.NoneBreakingSpace}{Template.BetType.betTypeName}";

            Template.BetType.betTypeName = bettypeName;
        }

        protected override void BuildScore(ITicket ticket)
            => Template.Score = null;
    }
}