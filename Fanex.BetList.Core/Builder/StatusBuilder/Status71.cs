namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Entities;

    /// <summary>
    /// Casino Games.
    /// </summary>
    public class Status71 : Status1
    {
        protected override void BuildStatusResult(ITicket ticket)
        {
            base.BuildStatusResult(ticket);

            Template.StatusResult.Hide();
        }
    }
}