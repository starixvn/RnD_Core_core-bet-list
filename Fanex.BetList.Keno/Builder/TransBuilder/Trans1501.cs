namespace Fanex.BetList.Core.Builder.TransBuilder
{
    using Entities;

    public class Trans1501 : Trans1
    {
        protected override string ExtraInfoTag => "p8";

        protected override void BuildRefNo(ITicket ticket)
            => Template.TransTime.refNo = ticket.RefNo;
    }
}