namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Entities;

    /// <summary>
    /// Bet Type: Outright.
    /// </summary>
    public class Status10 : Status1
    {
        protected override void BuildStatusResult(ITicket ticket)
        {
            Template.StatusResult.Hide();
            Template.StatusResult.isoutright = "1";
        }
    }
}