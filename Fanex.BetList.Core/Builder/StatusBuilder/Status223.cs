namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using System.Globalization;
    using Entities;

    /// <summary>
    /// Fast Markets: What will happen first in next 1 minute
    /// </summary>
    public class Status223 : Status1
    {
        protected override void BuildStatusResult(ITicket ticket)
        {
            base.BuildStatusResult(ticket);
            Template.StatusResult.betId = ticket.Handicap1.ToString(CultureInfo.InvariantCulture);
        }
    }
}