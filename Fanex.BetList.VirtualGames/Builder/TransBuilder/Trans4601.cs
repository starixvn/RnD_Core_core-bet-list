namespace Fanex.BetList.Core.Builder.TransBuilder
{
    using Fanex.BetList.Core.Entities;

    public class Trans4601 : Trans1
    {
        protected override void BuildRefNo(ITicket ticket)
            => Template.TransTime.refNo = ticket.RefNo;
    }
}