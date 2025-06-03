namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Entities;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Utils.TicketExt;
    using Fanex.BetList.Core.Utils.Parsers;
    using System.Collections.Generic;
    /// <summary>Playtech Live.</summary>
    public class Choice15600 : ChoiceLCNoTableCode
    {
        private const string RefIdForTableName = "PTL_BB";
        private const string TableCodeKey = "tablecode";
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.betTeam = null;
            if (IsDetailTicket(ticket))
            {
                var _parsedTransDesc = ExtraInfoParser.ParseExtraInfo(ticket.TransDesc);
                if (_parsedTransDesc.ContainsKey(TableCodeKey))
                {
                    Template.betTeam = ticketHelper.GetResourceData(RefIdForTableName, _parsedTransDesc[TableCodeKey]);
                }
            }
        }
    }
}