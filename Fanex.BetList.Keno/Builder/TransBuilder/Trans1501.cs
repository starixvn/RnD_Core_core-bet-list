namespace Fanex.BetList.Core.Builder.TransBuilder
{
    using Entities;
    using Fanex.BetList.Core.Utils.StringExt;

    public class Trans1501 : Trans1
    {
        private const string RnGKenoXmlTag = "p8";

        protected override void BuildRefNo(ITicket ticket)
            => Template.TransTime.refNo = ticket.RefNo;

        protected override void BuildLicenseeTxId(ITicket ticket)
        {
            if (HasLicenseeTxId(ticket))
            {
                Template.LicenseeTxId.Value = ticket.ExtraInfo.GetXmlTagAttributeValue(RnGKenoXmlTag, "lt");
            }
            else
            {
                Template.LicenseeTxId.Value = string.Empty;
                Template.LicenseeTxId.Hide();
            }
        }

        private static bool HasLicenseeTxId(ITicket ticket)
            => ticket.ExtraInfo != null && ticket.ExtraInfo.HasAttributeInXmlTag(RnGKenoXmlTag, "lt");
    }
}