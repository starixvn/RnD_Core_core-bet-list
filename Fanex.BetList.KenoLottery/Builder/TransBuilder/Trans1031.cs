namespace Fanex.BetList.Core.Builder.TransBuilder
{
    using Fanex.BetList.Core.Entities;

    public class Trans1031 : Trans1
    {
        private const string KenoLotteryXmlTag = "p28";

        protected override void BuildRefNo(ITicket ticket)
        {
            Template.TransTime.refNo = ticket.RefNo;
        }

        public override string GetExtraInfoTag()
            => KenoLotteryXmlTag;
    }
}