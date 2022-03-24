namespace Fanex.BetList.Core.Builder.TransBuilder
{
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils.Parsers;
    using NPOI.HSSF.UserModel;
    using NPOI.SS.UserModel;

    public class Trans3802 : Trans1
    {
        private const string TxIdKey = "lt";

        protected override void BuildLicenseeTxId(ITicket ticket)
        {
            var ticketAdditional = ExtraInfoParser.ParseExtraInfo(ticket.MatchCode);

            if (ticketAdditional.ContainsKey(TxIdKey))
            {
                Template.LicenseeTxId.Value = ticketAdditional[TxIdKey];
            }
            else
            {
                Template.LicenseeTxId.Value = string.Empty;
                Template.LicenseeTxId.Hide();
            }
        }

        public override IRichTextString RenderRTF(ITicket ticket, ITicketHelper ticketHelper)
        {
            Render(ticket);

            var refNo = string.Join(null, new[] { CoreBetList.refno, ": ", Template.TransTime.refNo });
            var rtfTrans = new HSSFRichTextString(string.Join(null, new[] { refNo, "\n", Template.transTime }));

            var ticketAdditional = ExtraInfoParser.ParseExtraInfo(ticket.MatchCode);
            if (ticketAdditional.ContainsKey(TxIdKey))
            {
                var licenseeTxId = string.Join(null, new[] { CoreBetList.TxId, ": ", Template.LicenseeTxId.Value });
                rtfTrans = new HSSFRichTextString(string.Join(null, new[] { licenseeTxId, "\n", refNo, "\n", Template.transTime }));
            }

            return rtfTrans;
        }

    }
}
