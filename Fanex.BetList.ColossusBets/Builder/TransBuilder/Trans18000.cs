namespace Fanex.BetList.Core.Builder.TransBuilder
{
    using Entities;

    public class Trans18000 : Trans1
    {
        protected override void BuildRefNo(ITicket ticket)
        {
            Template.TransTime.refNo = ticket.RefNo;
        }
    }
}