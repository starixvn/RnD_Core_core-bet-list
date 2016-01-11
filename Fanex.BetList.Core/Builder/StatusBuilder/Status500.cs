namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Fanex.BetList.Core.Entities;

    /// <summary>
    /// Charge Commission.
    /// </summary>
    public class Status500 : Status1
    {
        protected override void BuildStatusResult(ITicket ticket)
        {
            Template.StatusResult = null;
        }
    }
}