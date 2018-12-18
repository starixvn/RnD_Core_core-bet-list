namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Entities;

    /// <summary>
    /// E-Sports 2.0: Map X Total Kills Handicap.
    /// </summary>
    public class Choice9002 : Choice1
    {
        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.BetType.betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId, ticket.RefNo, ticket.BetId, ticket.BetCheck);
        }

        protected override void BuildScore(ITicket ticket)
        {
            base.BuildScore(ticket);

            Template.Score.SetValue($"<span class=\"{Favorite}\"> [{Choice9001.GetScoreText(ticket.ExtraInfo)}]</span>");
        }
    }
}