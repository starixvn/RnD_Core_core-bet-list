namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Xml.Linq;
    using Entities;
    using Templates;
    using Utils;
    using Utils.StringExt;

    public class Choice1401 : Choice1
    {
        public const string GameTypeRefId = "PG_GT";
        public const string GameModeRefId = "PG_Mode";
        public const string GameInfoRefId = "PG_GameInfo";

        private string _tableNumber;
        private string _playerId;

        public override Choice_Template Render(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowScoreMap)
        {
            if (!string.IsNullOrEmpty(ticket.ExtraInfo))
            {
                var extraInfoXml = ticket.ExtraInfo.ParseXml();

                ParseExtraInfo(extraInfoXml);
            }

            return base.Render(ticket, ticketHelper, ticketData, isShowScoreMap);
        }

        protected virtual void ParseExtraInfo(XDocument extraInfoXml)
        {
            const string tableNumberAttributeName = "tn";
            const string playerIdAttributeName = "si";

            _tableNumber = GetExtraInfo(extraInfoXml, tableNumberAttributeName);
            _playerId = GetExtraInfo(extraInfoXml, playerIdAttributeName);
        }

        protected static string GetExtraInfo(XDocument extraInfoXml, string attribute)
        {
            const string tag = "p33";
            var node = extraInfoXml.Descendants(tag);

            if (node.Any())
            {
                return node.FirstOrDefault().Attribute(attribute)?.Value;
            }

            return string.Empty;
        }

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
            => Template.betTeam = string.Empty;

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.betTeamClassName = Underdog;
            Template.Handicap.handicap = null;
        }

        protected override void BuildScore(ITicket ticket)
            => Template.Score = null;

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
            => Template.BetType.betTypeName = ticketHelper.GetResourceData(GameTypeRefId, ticket.BetTypeId.ToString());

        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
        {
            var matchInfoRows = GetMatchInfo(ticket, ticketHelper);

            Template.Match.SetValue("<div class=\"game-info\">");

            foreach (var row in matchInfoRows)
            {
                Template.Match.Append($"<div>{row}</div>");
            }

            Template.Match.Append("</div>");
        }

        protected virtual string[] GetMatchInfo(ITicket ticket, ITicketHelper ticketHelper)
            => new[]
            {
                BuildSeatAndTableBlock(ticket, ticketHelper),
                BuildModeInfoBlock(ticket, ticketHelper),
                BuildPlayerBlock(ticketHelper)
            };

        private string BuildSeatAndTableBlock(ITicket ticket, ITicketHelper ticketHelper)
        {
            const string seatResourceId = "Seat";
            const string tableResourceId = "Table";
            const string perPointResourceId = "PerPoint";

            var seatPattern = ticketHelper.GetResourceData(GameInfoRefId, seatResourceId);
            var seatInfo = string.Format(seatPattern, ticket.LiveHomeScore);
            var tablePattern = ticketHelper.GetResourceData(GameInfoRefId, tableResourceId);
            var tableInfo = string.Format(tablePattern, _tableNumber);
            var perPointPattern = ticketHelper.GetResourceData(GameInfoRefId, perPointResourceId);
            var perPointInfo = string.Format(perPointPattern, string.Format(Formatter.DECIMALFORMAT1, ticket.Handicap1));

            return $"{seatInfo}, {tableInfo} {perPointInfo}";
        }

        private static string BuildModeInfoBlock(ITicket ticket, ITicketHelper ticketHelper)
            => ticketHelper.GetResourceData(GameModeRefId, ticket.CommStatus);

        private string BuildPlayerBlock(ITicketHelper ticketHelper)
        {
            const string playerIdResourceId = "PlayerID";

            var playerPattern = ticketHelper.GetResourceData(GameInfoRefId, playerIdResourceId);

            return string.Format(playerPattern, _playerId);
        }

        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
            => Template.League.LeagueName.leagueName = ticket.BetCheck;

        protected override void BuildBetTypeName(ref RTFHelper rtfHelper)
            => rtfHelper.RTFRenderer.AddText(Template.BetType.betTypeName);

        protected override void BuildBetTeamsInfo(ITicket ticket, ref RTFHelper rtfHelper)
        {
            var gameInfoRows = SplitHtmlIntoRows(Template.Match.ToString());

            foreach (var row in gameInfoRows)
            {
                rtfHelper.RTFRenderer.AddText("\n" + row);
            }
        }

        protected static string[] SplitHtmlIntoRows(string html)
        {
            const string splitter = "</div>";
            var htmlTagMatching = new Regex("</?[^<>]*>");

            return Regex
                .Split(html, splitter)
                .Select(row => htmlTagMatching.Replace(row, string.Empty))
                .Where(row => row != string.Empty)
                .ToArray();
        }
    }
}