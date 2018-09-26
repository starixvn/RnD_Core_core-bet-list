namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Entities;

    /// <summary>
    /// E-Sports 2.0: Map X Moneyline.
    /// </summary>
    public class Status9001 : Status1
    {
        protected override void BuildStatusResult(ITicket ticket)
        {
            base.BuildStatusResult(ticket);

            var betId = ticket.BetId ?? 0;

            Template.StatusResult.betId = betId.ToString();
        }
    }
}