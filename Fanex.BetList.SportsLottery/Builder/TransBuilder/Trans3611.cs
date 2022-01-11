namespace Fanex.BetList.Core.Builder.TransBuilder
{
    using Fanex.BetList.Core.Entities;

    public class Trans3611 : Trans1
    {
        private const string SportsLotteryXmlTag = "p63";

        protected override void BuildRefNo(ITicket ticket)
        {
            Template.TransTime.refNo = ticket.RefNo;
        }

        public override string GetExtraInfoTag()
            => SportsLotteryXmlTag;
    }
}
