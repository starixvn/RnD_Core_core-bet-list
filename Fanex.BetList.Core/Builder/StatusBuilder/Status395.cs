namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Fanex.BetList.Core.Entities;

    /// <summary>
    /// 1H Race to X Points
    /// </summary>
    public class Status395 : Status1
    {
        protected override void BuildStatusResult(ITicket ticket)
        {
            base.BuildStatusResult(ticket);

            var betId = ticket.BetId ?? 0;

            Template.StatusResult.betId = betId.ToString();
        }
    }
}