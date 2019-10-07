namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Constants;
    using Entities;
    using Resources;

    /// <summary>
    /// Next High/Low.
    /// </summary>
    public class Choice87 : Choice81
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var betId = ticket.BetId.ToString() == "0" ? string.Empty : ticket.BetId.ToString();
            var liveScore = (!ticket.IsLive || string.IsNullOrEmpty(betId)) ? " [0]" : $" [{betId}]";
            var betTeam = ticket.BetTeam == BetTeamValue.H ? NumberGameLang.high : NumberGameLang.low;

            Template.betTeam = betTeam + liveScore;
        }

        protected override void BuildScore(ITicket ticket)
        {
            Template.Score = null;
        }
    }
}