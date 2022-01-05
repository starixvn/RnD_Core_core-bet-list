namespace Fanex.BetList.Core.Builder.TransBuilder
{
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils.StringExt;

    public class Trans3601 : Trans1
    {
        private const string NumberGameXmlTag = "p3";

        protected override void BuildRefNo(ITicket ticket)
        {
            Template.TransTime.refNo = ticket.RefNo;
        }

        protected override void BuildLicenseeTxId(ITicket ticket)
        {
            if (HasLicenseeTxId(ticket))
            {
                Template.LicenseeTxId.Value = ticket.ExtraInfo.GetXmlTagAttributeValue(NumberGameXmlTag, "lt");
            }
            else
            {
                Template.LicenseeTxId.Value = string.Empty;
                Template.LicenseeTxId.Hide();
            }
        }

        private static bool HasLicenseeTxId(ITicket ticket)
            => ticket.ExtraInfo != null && ticket.ExtraInfo.HasAttributeInXmlTag(NumberGameXmlTag, "lt");
    }
}
