namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;

    /// <summary>
    /// Next High/Low.
    /// </summary>
    public class Choice87 : Choice81
    {
        /// <summary>
        /// Builds the bet team.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        /// <param name="ticketData">The ticket data.</param>
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var betId = ticket.BetId.ToString() == "0" ? string.Empty : ticket.BetId.ToString();
            var liveScore = (!ticket.IsLive || string.IsNullOrEmpty(betId)) ? " [0]" : $" [{betId}]";
            var betTeam = ticket.BetTeam == BetTeamValue.H ? CoreBetList.high : CoreBetList.low;

            Template.betTeam = betTeam + liveScore;
        }

        protected override void BuildScore(ITicket ticket)
        {
            Template.Score = null;
        }
    }
}