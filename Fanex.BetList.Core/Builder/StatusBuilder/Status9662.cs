namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Entities;

    /// <summary>
    /// Home Next Man Out
    /// </summary>
    public class Status9662 : Status1
    {
        protected override void BuildStatusResult(ITicket ticket)
        {
            base.BuildStatusResult(ticket);

            Template.StatusResult.betId = $"{ticket.BetId.ToString()}, '{ticket.TransDesc}'";
        }
    }
}