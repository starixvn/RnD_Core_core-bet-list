namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Entities;

    public class Status1011 : Status1
    {
        protected override void BuildStatusResult(ITicket ticket)
        {
            Template.StatusResult = null;
        }
    }
}