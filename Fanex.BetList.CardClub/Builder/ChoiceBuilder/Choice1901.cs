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
    public class Choice1901 : ChoiceRNG
    {
        public const string GameTypeRefId = "PG_GT";
        public const string GameGroupRefId = "PG_GG";
        public const string GameInfoRefId = "PG_GameInfo";
        private readonly Regex htmlTagMatching = new Regex("</?[^<>]*>");

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.betTeam = $"<span class=\"handicap\">{ticketHelper.GetResourceData(GameTypeRefId, ticket.TransDesc)}</span>";

            if (IsDetailTicket(ticket.BetCheck))
            {
                Template.betTeam += $" {GetTransId3rd(ticket.BetCheck)}";
            }
        }

        protected override void AdjustBetTeamToRTF()
            => Template.betTeam = htmlTagMatching.Replace(Template.betTeam, string.Empty);

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

            if (IsSummaryTicket(ticket.BetCheck))
            {
                Template.League.Append(BuildVideoPokerGameDetails(ticket));
            }
        }

        private static string GetGameGroupName(ITicket ticket, ITicketHelper ticketHelper)
            => ticketHelper.GetResourceData(GameGroupRefId, ticket.TransDesc);
        
        private static string BuildVideoPokerGameDetails(ITicket ticket)
        {
            var detailLink = $"<a class=\"detail-link\" href=\"javascript:void('');\">{CoreBetList.details}</a>";
            var parameterList = $"'{ticket.TransId}', '{ticket.WinlostDate}', {ticket.CustId}, {ticket.GetTransactionType()}";

            return $"<div class=\"detail advantplay-detail\" onclick=\"showVideoPokerGameDetails({parameterList});\">{detailLink}</div>";
        }

        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
        {
            if (IsSummaryTicket(ticket.BetCheck))
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