namespace Fanex.BetList.Core.Builder.TransBuilder
{
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils.Parsers;
    using System.Collections.Generic;

    public class TransRNG : Trans1
    {
        private const string TxIdKey = "lt";
        private IDictionary<string, string> ExtraInfo = null;
        protected override void BuildLicenseeTxId(ITicket ticket)
        {
            if(ExtraInfo == null){
                ExtraInfo = ExtraInfoParser.ParseExtraInfo(ticket.MatchCode);
            }

            if (HasLicenseeTxId(ticket))
            {
                Template.LicenseeTxId.Value = ExtraInfo[TxIdKey];
            }
            else
            {
                Template.LicenseeTxId.Value = string.Empty;
                Template.LicenseeTxId.Hide();
            }
        }

        protected override bool HasLicenseeTxId(ITicket ticket)
            => ExtraInfo != null && ExtraInfo.ContainsKey(TxIdKey);

    }
}
