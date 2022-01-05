namespace Fanex.BetList.Core.Builder.TransBuilder
{
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils.StringExt;

    public class Trans4901 : Trans1
    {
        private const string TableGamesXmlTag = "p40";

        protected override void BuildRefNo(ITicket ticket)
        {
            Template.TransTime.refNo = ticket.RefNo;
        }

        protected override void BuildLicenseeTxId(ITicket ticket)
        {
            if (HasLicenseeTxId(ticket))
            {
                Template.LicenseeTxId.Value = ticket.ExtraInfo.GetXmlTagAttributeValue(TableGamesXmlTag, "lt");
            }
            else
            {
                Template.LicenseeTxId.Value = string.Empty;
                Template.LicenseeTxId.Hide();
            }
        }

        private static bool HasLicenseeTxId(ITicket ticket)
            => ticket.ExtraInfo != null && ticket.ExtraInfo.HasAttributeInXmlTag(TableGamesXmlTag, "lt");
    }
}
