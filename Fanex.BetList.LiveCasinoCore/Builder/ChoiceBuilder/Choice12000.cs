namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Entities;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Utils.Parsers;
    using System.Collections.Generic;
    /// <summary>W Casino Live Casino Choice 12000.</summary>
    public class Choice12000 : ChoiceLCNoTableCode
    {
        protected override string RefIdForGameType => "WLC_BT";
        private const string RefIdForBetChoice = "WLC_BC";
        private const string ChoiceKey = "choice";
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.betTeam = null;
            if (IsDetailTicket(ticket))
            {
                var _parsedTransDesc = ExtraInfoParser.ParseExtraInfo(ticket.TransDesc);
                if (_parsedTransDesc.ContainsKey(ChoiceKey))
                {
                    Template.betTeam = ticketHelper.GetResourceData(RefIdForBetChoice, _parsedTransDesc[ChoiceKey]);
                }
            }
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.BetType.betTypeName = string.Empty;

            if (IsDetailTicket(ticket))
            {
                var _parseTransId3rd = GetTransId3rd(ticket.BetCheck);
                var gameType = ticketHelper.GetResourceData(RefIdForGameType, ticket.BetTypeId.ToString());
                var betIdSection = $"{LiveCasinoConstants.OpenBetIdTag}{CoreBetList.BetID}: {_parseTransId3rd}{LiveCasinoConstants.CloseBetIdTag}";
                Template.BetType.betTypeName = $"{gameType}{LiveCasinoConstants.BreakLine}{betIdSection}";
            }
        }

        protected override void BuildBetTypeName(ref RTFHelper rtfHelper)
        {
            var choice = string.Empty;

            if (Template.BetType != null && !string.IsNullOrEmpty(Template.BetType.betTypeName))
            {
                choice = Template.BetType.betTypeName.Replace(LiveCasinoConstants.NoneBreakingSpace, LiveCasinoConstants.Space)
                    .Replace(LiveCasinoConstants.BreakLine, LiveCasinoConstants.NewLine);
                rtfHelper.RTFRenderer.AddText(LiveCasinoConstants.NewLine + choice);
            }
        }

    }
} 