namespace Fanex.BetList.Core.Builder.TransBuilder
{
    using Fanex.BetList.Core.Entities;

    public class Trans3601 : Trans1
    {
        private const string NumberGameXmlTag = "p3";

        protected override void BuildRefNo(ITicket ticket)
        {
            Template.TransTime.refNo = ticket.RefNo;
        }

        public override string GetExtraInfoTag()
            => NumberGameXmlTag;
    }
}
