namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;
    using System.Text.RegularExpressions;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Templates;
    using Fanex.BetList.Core.Utils.Parsers;
    using Fanex.BetList.Core.Utils.TicketExt;

    public class Choice8202 : Choice1
    {
        private const string OpenBetTypeNameTag = "<span class='bet-type-name'>";
        private const string OpenBetIdTag = "<span class='bet-id'>";
        private const string OpenGameIdTag = "<span class='game-id'>";
        private const string CloseSpanTag = "</span>";

        private const string HTMLTagMatching ="</?[^<>]*>";
        private const string TableCodeKey = "tableCode";

        private Dictionary<string, string> _parsedTransDesc;

        public override Choice_Template Render(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowScoreMap)
        {
            _parsedTransDesc = ticket.ParseTransDesc();

            return base.Render(ticket, ticketHelper, ticketData, isShowScoreMap);
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.BetType.betTypeName = string.Empty;

            if (!string.IsNullOrEmpty(ticket.MatchCode))
            {
                Template.BetType.betTypeName = $"{OpenBetIdTag}{CoreBetList.BetID}: {ticket.BetCheck}{CloseSpanTag}";
            }
        }

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
            => Template.betTeam = GetBetChoice(ticket, ticketHelper);

        private string GetBetChoice(ITicket ticket, ITicketHelper ticketHelper)
        {
            if (!string.IsNullOrEmpty(ticket.MatchCode))
            {
                var bettypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId);
                if (_parsedTransDesc != null && _parsedTransDesc.ContainsKey(TableCodeKey))
                {

                    return $"{bettypeName} {_parsedTransDesc[TableCodeKey]}";
                }
                 
                return bettypeName;
            }

            return string.Empty;
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap = null;
            Template.betTeamClassName = Underdog;
        }

        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
        {
            Template.League.LeagueName.leagueName = BuildBetTypeName(ticket, ticketHelper);

            if (string.IsNullOrEmpty(ticket.MatchCode))
            {
                Template.League.Append(BuildDetailLink(ticket));
            }
            else
            {
                Template.League.LeagueName.leagueName += " " + OpenGameIdTag + ticket.MatchCode + CloseSpanTag;
            }
        }

        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
        {
            Template.Match = null;
        }

        public static string BuildBetTypeName(ITicket ticket, ITicketHelper ticketHelper)
        {
            var betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId);

            return $"{OpenBetTypeNameTag}{betTypeName}{CloseSpanTag}";
        }

        public static Choice_League_Block BuildDetailLink(ITicket ticket)
        {
            var detailLinkMarkupBuilder = new StringBuilder();
            var detailFunction = BuildDetailFunction(ticket);

            detailLinkMarkupBuilder
                .Append("<div class=\"detail fgg-detail\" onclick='")
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

            detailFunctionBuilder.Append("showTodayRunningBetsDetails(&quot;")
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

        protected override void AdjustBetTeamToRTF()
        {
            RemoveHtmlTagFromBetTypeName();
            RemoveHtmlTagFromLeagueName();
        }
        private void RemoveHtmlTagFromLeagueName()
        {
            var htmlTagMatching = new Regex(HTMLTagMatching);
            Template.League.LeagueName.leagueName = htmlTagMatching.Replace(Template.League.LeagueName.leagueName, string.Empty);
        }

        private void RemoveHtmlTagFromBetTypeName()
        {
            var htmlTagMatching = new Regex(HTMLTagMatching);
            Template.BetType.betTypeName = htmlTagMatching.Replace(Template.BetType.betTypeName, string.Empty);
        }
    }
}
