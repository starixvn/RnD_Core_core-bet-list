namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Entities;

    public class Status29 : Status1
    {
        protected override void BuildStatusResult(ITicket ticket)
        {
            base.BuildStatusResult(ticket);

            Template.StatusResult.refNo_MixParlay = ticket.RefNo;
        }
    }
}