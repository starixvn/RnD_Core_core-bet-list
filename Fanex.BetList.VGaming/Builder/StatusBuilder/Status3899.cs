namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Fanex.BetList.Core.Entities;

    /// <summary>
    /// Jackpot Prize
    /// </summary>
    public class Status3899 : Status1
    {
        protected override void BuildStatusResult(ITicket ticket)
        {
            base.BuildStatusResult(ticket);

            Template.StatusResult.Hide();
        }
    }
}