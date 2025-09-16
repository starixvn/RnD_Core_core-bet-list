using Fanex.BetList.Core.Constants;
using Fanex.BetList.Core.Entities;
using Fanex.BetList.Core.Utils.Parsers;
using System.Collections.Generic;

namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    /// <summary>OFA.</summary>
    public class Choice12250 : ChoiceLCNoTableCode
    {
        protected override string RefIdForGameType => "OFA_BC";

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.betTeam = null;
            if (IsDetailTicket(ticket))
            {
                var _parsedTransDesc = ExtraInfoParser.ParseExtraInfo(ticket.TransDesc);
                if (_parsedTransDesc.ContainsKey(LiveCasinoConstants.ChoiceKey))
                {
                    Template.betTeam = ticketHelper.GetResourceData(RefIdForGameType, _parsedTransDesc[LiveCasinoConstants.ChoiceKey]);
                }
            }
        }
    }
}