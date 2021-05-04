namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text.RegularExpressions;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Templates;
    using Fanex.BetList.Core.Utils.StringExt;
    using Fanex.BetList.Core.Utils.TicketExt;

    /// <summary>
    /// Video Poker
    /// </summary>
    public class Choice1901 : Choice1
    {
        public const string GameTypeRefId = "PG_GT";
        public const string GameGroupRefId = "PG_GG";
        public const string GameInfoRefId = "PG_GameInfo";
        private readonly Regex htmlTagMatching = new Regex("</?[^<>]*>");

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var gameType = $"<span class=\"handicap\">{GetGameTypeName(ticket, ticketHelper)}</span>";
            Template.betTeam = IsSummaryTicket(ticket) ? gameType : $"{gameType} {ticket.BetCheck}";
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = null;
            Template.betTeamClassName = Underdog;
        }

        protected override void BuildScore(ITicket ticket)
        {
            Template.Score = null;
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.BetType = null;
        }

        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
        {
            if (IsSummaryTicket(ticket))
            {
                Template.Match = null;
            }
            else
            {
                var tableNumber = BuildTableNumber(ticket, ticketHelper);
                var point = BuildPoint(ticket, ticketHelper);

                Template.Match.SetValue("<div class=\"game-info\">");
                Template.Match.Append($"<div>{tableNumber} {point}</div>");
                Template.Match.Append("</div>");
            }
        }

        protected virtual string BuildTableNumber(ITicket ticket, ITicketHelper ticketHelper)
            => string.Format(
                ticketHelper.GetResourceData(GameInfoRefId, "Table"),
                GetAdditionalInfo("TableNumber", ticket));

        protected virtual string BuildPoint(ITicket ticket, ITicketHelper ticketHelper)
            => string.Format(
                ticketHelper.GetResourceData(GameInfoRefId, "PerPoint"),
                GetAdditionalInfo("Point", ticket));

        protected override void BuildSport(ITicket ticket, ITicketHelper ticketHelper)
        {
            if (Template.League == null)
            {
                return;
            }

            Template.League.sportTypeName = ticketHelper.GetResourceData(GameTypeRefId, ticket.SportTypeId.ToString());
        }

        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
        {
            var gameGroup = GetGameGroupName(ticket, ticketHelper);
            Template.League.LeagueName.SetValue($"<span class='gamegroupname'> {gameGroup}</span>");

            if (IsSummaryTicket(ticket))
            {
                Template.League.Append(BuildVideoPokerGameDetails(ticket));
            }
        }

        protected override void BuildBetTeamsInfo(ITicket ticket, ref RTFHelper rtfHelper)
        {
            if (Template.Match != null)
            {
                var gameInfoRows = Template.Match.ToString().SplitHTMLIntoRows();

                foreach (var row in gameInfoRows)
                {
                    rtfHelper.RTFRenderer.AddText("\n" + row);
                }
            }
        }

        protected override void AdjustBetTeamToRTF()
            => Template.betTeam = htmlTagMatching.Replace(Template.betTeam, string.Empty);

        protected override void BuildMatchInfo(ref RTFHelper rtfHelper)
        {
            var sportTypeName = Template.League.sportTypeName;
            var gameGroupName = htmlTagMatching.Replace(Template.League.LeagueName.ToString(), string.Empty);

            if (!string.IsNullOrEmpty(sportTypeName) && !string.IsNullOrEmpty(gameGroupName))
            {
                rtfHelper.RTFRenderer.AddText($"\n{sportTypeName}{gameGroupName}");
            }
        }

        private static string GetGameTypeName(ITicket ticket, ITicketHelper ticketHelper)
            => ticketHelper.GetResourceData(GameTypeRefId, ticket.TransDesc);

        private static string GetGameGroupName(ITicket ticket, ITicketHelper ticketHelper)
            => ticketHelper.GetResourceData(GameGroupRefId, ticket.TransDesc);

        private static Choice_League_Block BuildVideoPokerGameDetails(ITicket ticket)
        {
            string[] detailsFunctionDivCompositions =
            {
                "<div class=\"detail video-poker-game-detail\" onclick=\"showVideoPokerGameDetails(",
                ticket.TransId.ToString(CultureInfo.InvariantCulture),
                ", ",
                "'",
                ticket.WinlostDate.ToString(CultureInfo.InvariantCulture),
                "'",
                ", ",
                ticket.CustId.ToString(CultureInfo.InvariantCulture),
                ", ",
                ticket.GetTransactionType(),
                ");\">",
                "<a style=\"color:#755200;font-weight:bold\" href=\"javascript:void('');\">",
                CoreBetList.details,
                "</a>",
                "</div>"
            };
            string detailLinkMarkup = string.Join(null, detailsFunctionDivCompositions);
            var detailtTemplateBlock = new Choice_League_Block();

            detailtTemplateBlock.SetValue(detailLinkMarkup);

            return detailtTemplateBlock;
        }

        private static bool IsSummaryTicket(ITicket ticket) => string.IsNullOrWhiteSpace(ticket.BetCheck);

        private static string GetAdditionalInfo(string infoName, ITicket ticket)
        {
            var additionalItems = DeserializeAdditional(ticket.MatchCode);

            if (additionalItems.ContainsKey(infoName))
            {
                return additionalItems[infoName];
            }

            return string.Empty;
        }

        private static Dictionary<string, string> DeserializeAdditional(string additional)
        {
            const int validLengthOfStatement = 2;
            Dictionary<string, string> additionalItems = new Dictionary<string, string>();

            if (string.IsNullOrEmpty(additional))
            {
                return additionalItems;
            }

            var keyValuePairs = additional.Split(new[] { '*' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var keyValuePair in keyValuePairs)
            {
                var keyValue = keyValuePair.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);

                if (keyValue.Length == validLengthOfStatement)
                {
                    additionalItems.Add(keyValue[0], keyValue[1]);
                }
            }

            return additionalItems;
        }
    }
}