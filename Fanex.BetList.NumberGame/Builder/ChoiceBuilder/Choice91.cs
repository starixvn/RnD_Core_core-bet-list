namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Constants;
    using Entities;
    using Resources;

    public class Choice91 : Choice81
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var betId = ticket.BetId.ToString();
            var numberBall = ticket.BetId.HasValue ? $" [{betId}]" : string.Empty;
            var colorBall = ticket.BetTeam.ToLowerInvariant() == BetTeamValue.R ? NumberGameLang.Red : NumberGameLang.Blue;

            Template.betTeam = colorBall + numberBall;
        }

        protected override void BuildScore(ITicket ticket)
        {
            Template.Score = null;
        }
    }
}