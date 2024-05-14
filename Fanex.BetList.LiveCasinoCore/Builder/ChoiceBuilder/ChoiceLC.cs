namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;
    using System.Text.RegularExpressions;
    using App_GlobalResources;
    using Entities;
    using Fanex.BetList.Core.Utils.Parsers;
    using Fanex.BetList.Core.Utils.TicketExt;
    using Templates;
    using Utils;

    public class ChoiceLC : Choice1
    {
        private const string OpenBetTypeNameTag = "<span class='bet-type-name'>";
        private const string CloseBetTypeNameTag = "</span>";

        private const string OpenBetIdTag = "<span class='bet-id'>";
        private const string CloseBetIdTag = "</span>";

        private const string OpenGameIdTag = "<span class='game-id'>";
        private const string CloseGameIdTag = "</span>";

        private const string GameTableIdFormat = " <span class=\"gamegroupname\">{0}</span>";

        private Dictionary<string, string> _parsedTransDesc;

        public override Choice_Template Render(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowScoreMap)
        {
            _parsedTransDesc = ExtraInfoParser.ParseExtraInfo(ticket.TransDesc);

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

            if (IsDetailTicket(ticket))
            {
                var _parseTransId3rd = GetTransId3rd(ticket.BetCheck);
                Template.BetType.betTypeName = $"{OpenBetIdTag}{CoreBetList.BetID}: {_parseTransId3rd}{CloseBetIdTag}";
            }
        }

        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
        {
            Template.League.LeagueName.leagueName = BuildBetTypeName(ticket, ticketHelper);

            if (IsSummaryTicket(ticket))
            {
                Template.League.Append(BuildDetailLink(ticket));
            }
            else
            {
                Template.League.LeagueName.leagueName += GetTableCode(ticketHelper) + BuildGameId(ticket);
            }
        }

        protected override void AdjustBetTeamToRTF()
        {
            RemoveHtmlTagFromLeagueName();
            RemoveHtmlTagFromBetTypeName();
        }

        public virtual string GetDisplayChoice(ITicketHelper ticketHelper, string choice)
            => choice;

        public virtual string GetDisplayTableCode(ITicketHelper ticketHelper, string tableCode)
            => tableCode;
            
        private string GetBetChoice(ITicketHelper ticketHelper)
        {
            const string BetChoiceDataKey = "choice";
            if (_parsedTransDesc != null && _parsedTransDesc.ContainsKey(BetChoiceDataKey))
            {
                return GetDisplayChoice(ticketHelper, _parsedTransDesc[BetChoiceDataKey]);
            }

            return string.Empty;
        }

        public virtual Choice_League_Block BuildDetailLink(ITicket ticket)
        {
            var detailLinkMarkupBuilder = new StringBuilder();
            var detailFunction = BuildDetailFunction(ticket);

            detailLinkMarkupBuilder
                .Append("<div class=\"detail live-casino-detail\" onclick='")
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

        public virtual string BuildDetailFunction(ITicket ticket)
            => string.Empty;

        public static string BuildBetTypeName(ITicket ticket, ITicketHelper ticketHelper)
        {
            var betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId);

            return $"{OpenBetTypeNameTag}{betTypeName}{CloseBetTypeNameTag}";
        }

        private string GetTableCode(ITicketHelper ticketHelper)
        {
            const string TableCodeKey = "tablecode";
            if (_parsedTransDesc != null && _parsedTransDesc.ContainsKey(TableCodeKey))
            {
                return string.Format(GameTableIdFormat, GetDisplayTableCode(ticketHelper ,_parsedTransDesc[TableCodeKey]));
            }

            return string.Empty;
        }

        private static string BuildGameId(ITicket ticket)
            => $" {OpenGameIdTag}{ticket.MatchCode}{CloseGameIdTag}";        

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

        protected virtual string GetTransId3rd(string transId3rd)
        {
            const string transId3rdSeparator = "|$|";
            if (transId3rd == null || !transId3rd.Contains(transId3rdSeparator))
            {
                return transId3rd;
            }

            var transIdParts = transId3rd.Split(new string[] { transId3rdSeparator }, StringSplitOptions.RemoveEmptyEntries);

            return transIdParts[transIdParts.Length - 1];
        }

        protected virtual bool IsDetailTicket(ITicket ticket)
            => !string.IsNullOrEmpty(ticket.TransDesc);

        protected virtual bool IsSummaryTicket(ITicket ticket)
            => !IsDetailTicket(ticket);
    }
}