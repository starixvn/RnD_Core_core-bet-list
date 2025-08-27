namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Entities;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Utils.Parsers;
    using System.Collections.Generic;

    /// <summary>Playtech Live.</summary>
    public class Choice15600 : ChoiceLCNoTableCode
    {
        protected override string RefIdForGameType => "PTL_BB";

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.betTeam = null;
            if (IsDetailTicket(ticket))
            {
                var _parsedTransDesc = ExtraInfoParser.ParseExtraInfo(ticket.TransDesc);
                if (_parsedTransDesc.ContainsKey(LiveCasinoConstants.TableCodeKey))
                {
                    Template.betTeam = ticketHelper.GetResourceData(RefIdForGameType, _parsedTransDesc[LiveCasinoConstants.TableCodeKey]);
                }
            }
        }
    }
}