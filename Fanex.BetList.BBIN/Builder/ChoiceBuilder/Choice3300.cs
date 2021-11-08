namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Resource;
    using Fanex.BetList.Core.Templates;
    using Fanex.BetList.Core.Utils;
    using Fanex.BetList.Core.Utils.StringExt;
    using Fanex.BetList.Core.Utils.TicketExt;

    public class Choice3300 : Choice1
    {
        private const string BetTeamFormat = "<div><span class='favorite'>{0}</span>&nbsp;<span class='handicap custom'>({1}{2})</span></div>";
        private const string OpenBetTypeNameTag = "<span class='bbin-bet-type-name'>";
        private const string CloseBetTypeNameTag = "</span>";
        private const string OpenBetIdTag = "<span class='bbin-bet-id'>";
        private const string CloseBetIdTag = "</span>";
        private const string OpenGameIdTag = "<span class='bbin-game-id'>";
        private const string CloseGameIdTag = "</span>";
        private const string GameTableIdFormat = "<span class=\"gamegroupname\">{0}</span>";

        private const string GameNameKey = "GameName";
        private const string ChoiceKey = "Choice";
        private const string TableCodeKey = "TableCode";

        private IDictionary<string, string> _parsedTransDesc;

        public override Choice_Template Render(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowScoreMap)
        {
            _parsedTransDesc = ticket.ParseTransDesc();

            return base.Render(ticket, ticketHelper, ticketData, isShowScoreMap);
        }

        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
            => Template.Match = null;

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
            => Template.betTeam = GetChoiceInfo(ticket, ticketHelper);

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap = null;
            Template.betTeamClassName = Underdog;
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.BetType.betTypeName = string.Empty;

            if (!ticket.IsSummaryTicket())
            {
                Template.BetType.betTypeName = $"{OpenBetIdTag}{BBinResources.BetId}: {ticket.BetCheck}{CloseBetIdTag}";
            }
        }

        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
        {
            if (ticket.IsSummaryTicket())
            {
                Template.League.LeagueName.leagueName = BuildBetTypeName(ticket, ticketHelper);
                if (ticket.IsNotRunningTicket())
                {
                    Template.League.Append(BuildDetailLink(ticket));
                }
            }
            else
            {
                var leagueName = $"{GetGameName(ticketHelper)} {GetTableCode()} {BuildGameId(ticket)}";
                Template.League.LeagueName.leagueName = leagueName;
            }
        }
        private string GetChoiceInfo(ITicket ticket, ITicketHelper ticketHelper)
        {
            var betTeam = new StringBuilder();
            var choices = _parsedTransDesc.ContainsKey(ChoiceKey) ? _parsedTransDesc[ChoiceKey].Split('@') : new string[] { };
            foreach (var choice in choices)
            {
                var gameId = _parsedTransDesc.ContainsKey(GameNameKey) ? _parsedTransDesc[GameNameKey] : string.Empty;
                var choiceInfo = choice.Split(',');
                const int minInfoLength = 3;
                if (choiceInfo.Length >= minInfoLength)
                {
                    var shortChoiceId = choiceInfo[0];
                    var choiceId = gameId + shortChoiceId.PadLeft(4, '0');
                    var amount = Cast.AsDecimal(choiceInfo[2]);
                    var choiceName = ticketHelper.GetResourceData("BBIN_BT", choiceId);

                    betTeam.AppendFormat(BetTeamFormat, choiceName, $"{ticket.Currency} ", ConvertByBetType.Stake(amount));
                }
            }

            return betTeam.ToString();
        }

        private string GetGameName(ITicketHelper ticketHelper)
            => _parsedTransDesc != null && _parsedTransDesc.ContainsKey(GameNameKey)
            ? ticketHelper.GetResourceData("BBIN_GT", _parsedTransDesc[GameNameKey])
            : string.Empty;

        private static Choice_League_Block BuildDetailLink(ITicket ticket)
        {
            var detailLinkMarkupBuilder = new StringBuilder();
            var detailFunction = BuildDetailFunction(ticket);

            detailLinkMarkupBuilder
                .Append("<div class=\"detail bbin-detail\" onclick='")
                .Append(detailFunction)
                .Append("'>")
                .Append("<a class=\"detail-link\" href=\"javascript:void(&quot;&quot;);\">")
                .Append(CoreBetList.details)
                .Append("</a>")
                .Append("</div>");

            var detailTemplateBlock = new Choice_League_Block();

            detailTemplateBlock.SetValue(detailLinkMarkupBuilder.ToString());

            return detailTemplateBlock;
        }

        private static string BuildDetailFunction(ITicket ticket)
        {
            var detailFunctionBuilder = new StringBuilder();

            detailFunctionBuilder.Append("showBBINBetDetails(&quot;")
                .Append(ticket.TransId.ToString())
                .Append("&quot;")
                .Append(",&quot;")
                .Append(ticket.WinlostDate.ToString(CultureInfo.InvariantCulture))
                .Append("&quot;")
                .Append(",&quot;")
                .Append(ticket.CustId.ToString())
                .Append("&quot;")
                .Append(",&quot;")
                .Append(ticket.GetTransactionType())
                .Append("&quot;);");

            return detailFunctionBuilder.ToString();
        }

        private static string BuildBetTypeName(ITicket ticket, ITicketHelper ticketHelper)
        {
            var betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId);

            return $"{OpenBetTypeNameTag}{betTypeName}{CloseBetTypeNameTag}";
        }

        private string GetTableCode()
            => _parsedTransDesc != null && _parsedTransDesc.ContainsKey(TableCodeKey)
            ? string.Format(GameTableIdFormat, _parsedTransDesc[TableCodeKey])
            : string.Empty;

        private static string BuildGameId(ITicket ticket)
            => $"{OpenGameIdTag}{ticket.BetTeam}{CloseGameIdTag}";

        protected override void AdjustBetTeamToRTF()
        {
            Template.betTeam = string.Join("\n", Template.betTeam.SplitHTMLIntoRows());
            Template.League.LeagueName.leagueName = string.Join(string.Empty, Template.League.LeagueName.leagueName.StripHTML());
            RemoveHtmlTagFromBetTypeName();
        }

        private void RemoveHtmlTagFromBetTypeName()
        {
            Template.BetType.betTypeName = Template.BetType.betTypeName
                .Replace(OpenBetIdTag, string.Empty)
                .Replace(CloseBetIdTag, string.Empty);
        }
    }
}
