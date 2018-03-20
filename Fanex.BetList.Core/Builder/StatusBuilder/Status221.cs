namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using System.Globalization;
    using Entities;

    /// <summary>
    /// Fast Markets: Next 1 minute.
    /// </summary>
    public class Status221 : Status1
    {
        protected override void BuildStatusResult(ITicket ticket)
        {
            base.BuildStatusResult(ticket);
            Template.StatusResult.betId = $"{ticket.Handicap1}, {ticket.Handicap2}";
        }
    }
}