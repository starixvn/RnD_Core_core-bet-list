namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Entities;

    /// <summary>
    /// Mix Parlay.
    /// </summary>
    public class Status9 : Status1
    {
        protected override void BuildStatusResult(ITicket ticket)
        {
            base.BuildStatusResult(ticket);

            Template.StatusResult.refNo_MixParlay = ticket.RefNo;
        }
    }
}