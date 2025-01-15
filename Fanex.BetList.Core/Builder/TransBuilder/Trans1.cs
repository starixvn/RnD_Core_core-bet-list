namespace Fanex.BetList.Core.Builder.TransBuilder
{
    using App_GlobalResources;
    using Entities;
    using Fanex.BetList.Core.Utils.StringExt;    
    using BetListNPOI.SS.UserModel;
    using Templates;

    public class Trans1 : ITrans
    {
        protected virtual string ExtraInfoTag
            => "p1";

        public Trans1()
        {
            Template = new Trans_Template();
        }

        public Trans_Template Template { get; set; }

        public virtual Trans_Template Render(ITicket ticket)
        {
            BuildTransTime(ticket);

            BuildRefNo(ticket);

            BuildLicenseeTxId(ticket);

            return Template;
        }

        public virtual string RenderTranstime(ITicket ticket)
        {
            BuildTransTime(ticket);

            return Template.transTime;
        }

        public virtual string RenderRefNo(ITicket ticket)
        {
            BuildRefNo(ticket);

            return Template.TransTime.refNo;
        }

        public virtual string RenderLicenseeTxId(ITicket ticket)
        {
            BuildLicenseeTxId(ticket);

            return Template.LicenseeTxId.Value;
        }

#pragma warning disable S1449 // Culture should be specified for "string" operations

        protected virtual void BuildTransTime(ITicket ticket)
            => Template.transTime = ticket.TransDate.ToString();

#pragma warning restore S1449 // Culture should be specified for "string" operations

        protected virtual void BuildRefNo(ITicket ticket)
            => Template.TransTime.refNo = ticket.TransId.ToString(); // For Mix parlay(Trans9) to reuse

        protected virtual void BuildLicenseeTxId(ITicket ticket)
        {
            if (HasLicenseeTxId(ticket))
            {
                Template.LicenseeTxId.Value = ticket.ExtraInfo.GetXmlTagAttributeValue(ExtraInfoTag, "lt");
            }
            else
            {
                Template.LicenseeTxId.Value = string.Empty;
                Template.LicenseeTxId.Hide();
            }
        }

        public virtual IRichTextString RenderRTF(ITicket ticket, RTFHelper rtfHelper)
        {
            Render(ticket);

            var refNo = string.Join(null, new[] { CoreBetList.refno, ": ", Template.TransTime.refNo, "\n" });
            if (HasLicenseeTxId(ticket))
            {
                var licenseeTxId = string.Join(null, new[] { CoreBetList.LicTxId, ": ", Template.LicenseeTxId.Value , "\n"});
                rtfHelper.RTFRenderer.AddText(licenseeTxId);
            }

            rtfHelper.RTFRenderer.AddText(refNo);
            rtfHelper.RTFRenderer.AddText(Template.transTime);
            var transColumn = rtfHelper.RTFRenderer.Render();
            rtfHelper.RTFRenderer.Clear();

            return transColumn;
        }

        public virtual IRichTextString RenderTransTimeRTF(ITicket ticket, RTFHelper rtfHelper)
        {
            BuildTransTime(ticket);

            rtfHelper.RTFRenderer.AddText(Template.transTime);
            var transColumn = rtfHelper.RTFRenderer.Render();
            rtfHelper.RTFRenderer.Clear();

            return transColumn;
        }

        public virtual IRichTextString RenderRefNoRTF(ITicket ticket, RTFHelper rtfHelper)
        {
            BuildRefNo(ticket);

            rtfHelper.RTFRenderer.AddText(Template.TransTime.refNo);
            var transColumn = rtfHelper.RTFRenderer.Render();
            rtfHelper.RTFRenderer.Clear();

            return transColumn;
        }

        public virtual IRichTextString RenderLicenseeTxIdRTF(ITicket ticket, RTFHelper rtfHelper)
        {
            BuildLicenseeTxId(ticket);

            rtfHelper.RTFRenderer.AddText(Template.LicenseeTxId.Value);
            var transColumn = rtfHelper.RTFRenderer.Render();

            return transColumn;
        }

        protected virtual bool HasLicenseeTxId(ITicket ticket)
            => ticket.ExtraInfo != null && ticket.ExtraInfo.HasAttributeInXmlTag(ExtraInfoTag, "lt");

    }
}