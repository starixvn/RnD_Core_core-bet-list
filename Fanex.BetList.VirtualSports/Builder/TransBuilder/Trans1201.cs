namespace Fanex.BetList.Core.Builder.TransBuilder
{
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils.StringExt;

    public class Trans1201 : Trans1
    {
        private const string VirtualSportsXmlTag = "p5";

        protected override void BuildLicenseeTxId(ITicket ticket)
        {
            if (HasLicenseeTxId(ticket))
            {
                Template.LicenseeTxId.Value = ticket.ExtraInfo.GetXmlTagAttributeValue(VirtualSportsXmlTag, "lt");
            }
            else
            {
                Template.LicenseeTxId.Value = string.Empty;
                Template.LicenseeTxId.Hide();
            }
        }

        private static bool HasLicenseeTxId(ITicket ticket)
            => ticket.ExtraInfo != null && ticket.ExtraInfo.HasAttributeInXmlTag(VirtualSportsXmlTag, "lt");
    }
}
