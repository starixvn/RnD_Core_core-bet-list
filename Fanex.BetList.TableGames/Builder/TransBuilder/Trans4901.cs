namespace Fanex.BetList.Core.Builder.TransBuilder
{
    using Fanex.BetList.Core.Entities;

    public class Trans4901 : Trans1
    {
        private const string TableGamesXmlTag = "p40";

        protected override void BuildRefNo(ITicket ticket)
        {
            Template.TransTime.refNo = ticket.RefNo;
        }

        public override string GetExtraInfoTag()
            => TableGamesXmlTag;
    }
}
