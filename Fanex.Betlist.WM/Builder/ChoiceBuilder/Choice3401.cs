namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;
    using System.Text.RegularExpressions;
    using App_GlobalResources;
    using Entities;
    using Fanex.BetList.Core.Utils.TicketExt;
    using Templates;
    using Utils;

    public class Choice3401 : Choice1
    {
        private const string OpenBetTypeNameTag = "<span class='bet-type-name'>";
        private const string CloseBetTypeNameTag = "</span>";

        private const string OpenBetIdTag = "<span class='bet-id'>";
        private const string CloseBetIdTag = "</span>";

        private const string OpenGameIdTag = "<span class='game-id'>";
        private const string CloseGameIdTag = "</span>";

        private const string GameTableIdFormat = "<span class=\"gamegroupname\">{0}</span>";

        private Dictionary<string, string> _parsedTransDesc;

        public override Choice_Template Render(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowScoreMap)
        {
            _parsedTransDesc = ticket.ParseTransDesc();

            return base.Render(ticket, ticketHelper, ticketData, isShowScoreMap);
        }

        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
            => Template.Match = null;

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
            => Template.betTeam = GetBetChoice(ticketHelper);

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap = null;
            Template.betTeamClassName = Favorite;
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.BetType.betTypeName = string.Empty;

            if (!string.IsNullOrEmpty(ticket.TransDesc))
            {
                Template.BetType.betTypeName = $"{OpenBetIdTag}{CoreBetList.BetID}: {ticket.BetCheck}{CloseBetIdTag}";
            }
        }

        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
        {
            Template.League.LeagueName.leagueName = BuildBetTypeName(ticket, ticketHelper);

            if (string.IsNullOrEmpty(ticket.TransDesc))
            {
                Template.League.Append(BuildDetailLink(ticket));
            }
            else
            {
                Template.League.LeagueName.leagueName += " " + GetTableCode() + " " + BuildGameId(ticket);
            }
        }

        protected override void AdjustBetTeamToRTF()
        {
            RemoveHtmlTagFromLeagueName();
            RemoveHtmlTagFromBetTypeName();
        }

        private string GetBetChoice(ITicketHelper ticketHelper)
        {
            const string WMRefId = "WM_BT";
            const string BetChoiceDataKey = "Choice";
            if (_parsedTransDesc != null && _parsedTransDesc.ContainsKey(BetChoiceDataKey))
            {
                return ticketHelper.GetResourceData(WMRefId, _parsedTransDesc[BetChoiceDataKey]);
            }

            return string.Empty;
        }

        private static Choice_League_Block BuildDetailLink(ITicket ticket)
        {
            var detailLinkMarkupBuilder = new StringBuilder();
            var detailFunction = BuildDetailFunction(ticket);

            detailLinkMarkupBuilder
                .Append("<div class=\"detail wm-casino-detail\" onclick='")
                .Append(detailFunction)
                .Append("'>")
                .Append("<a style=\"color:#755200;font-weight:bold\" href=\"javascript:void(&quot;&quot;);\">")
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

            detailFunctionBuilder.Append("showWMBetDetails(&quot;")
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
        {
            const string TableCodeKey = "TableCode";
            if (_parsedTransDesc != null && _parsedTransDesc.ContainsKey(TableCodeKey))
            {
                return string.Format(GameTableIdFormat, _parsedTransDesc[TableCodeKey]);
            }

            return string.Empty;
        }

        private static string BuildGameId(ITicket ticket)
            => $"{OpenGameIdTag}{ticket.MatchCode}{CloseGameIdTag}";        

        private void RemoveHtmlTagFromLeagueName()
        {
            var htmlTagMatching = new Regex("</?[^<>]*>");
            Template.League.LeagueName.leagueName = htmlTagMatching.Replace(Template.League.LeagueName.leagueName, string.Empty);
        }

        private void RemoveHtmlTagFromBetTypeName()
        {
            Template.BetType.betTypeName = Template.BetType.betTypeName
                .Replace(OpenBetIdTag, string.Empty)
                .Replace(CloseBetIdTag, string.Empty);
        }
    }
}