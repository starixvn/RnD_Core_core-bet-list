namespace Fanex.BetList.Core.Builder.TransBuilder
{
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils.Parsers;

    public class Trans3802 : Trans1
    {
        private const string TxIdKey = "lt";

        protected override void BuildLicenseeTxId(ITicket ticket)
        {
            if (HasLicenseeTxId(ticket))
            {
                var ticketAdditional = ExtraInfoParser.ParseExtraInfo(ticket.MatchCode);
                Template.LicenseeTxId.Value = ticketAdditional[TxIdKey];
            }
            else
            {
                Template.LicenseeTxId.Value = string.Empty;
                Template.LicenseeTxId.Hide();
            }
        }

        protected override bool HasLicenseeTxId(ITicket ticket)
        {
            var ticketAdditional = ExtraInfoParser.ParseExtraInfo(ticket.MatchCode);

            return ticketAdditional.ContainsKey(TxIdKey);
        }

    }
}
