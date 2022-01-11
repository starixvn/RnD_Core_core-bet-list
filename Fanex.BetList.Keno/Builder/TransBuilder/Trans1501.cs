namespace Fanex.BetList.Core.Builder.TransBuilder
{
    using Entities;

    public class Trans1501 : Trans1
    {
        private const string RnGKenoXmlTag = "p8";

        protected override void BuildRefNo(ITicket ticket)
            => Template.TransTime.refNo = ticket.RefNo;
        public override string GetExtraInfoTag()
            => RnGKenoXmlTag;
    }
}