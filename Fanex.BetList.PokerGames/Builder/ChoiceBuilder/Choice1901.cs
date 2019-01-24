namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Templates;

    public class Choice1901 : Choice1401
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.betTeam = string.Empty;
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
            Template.BetType.betTypeName = IsSummaryTicket(ticket) ? string.Empty : GetGameTypeName(ticket, ticketHelper);
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
                var playerId = BuildPlayerId(ticket, ticketHelper);

                Template.Match.SetValue("<div class=\"game-info\">");
                Template.Match.Append($"<div>{tableNumber} {point}</div>");
                Template.Match.Append($"<div>{playerId}</div>");
                Template.Match.Append("</div>");
            }
        }

        protected virtual string BuildTableNumber(ITicket ticket, ITicketHelper ticketHelper)
            => string.Format(
                ticketHelper.GetResourceData(GameInfoRefId, "Table"),
                GetAdditionalInfo("TableNumber", ticket));

        protected virtual string BuildPoint(ITicket ticket, ITicketHelper ticketHelper)
            => string.Format(
                ticketHelper.GetResourceData(GameInfoRefId, "Point"),
                GetAdditionalInfo("Point", ticket));

        protected virtual string BuildPlayerId(ITicket ticket, ITicketHelper ticketHelper)
           => string.Format(
               ticketHelper.GetResourceData(GameInfoRefId, "PlayerID"),
               GetAdditionalInfo("PlayerID", ticket));

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
            if (IsSummaryTicket(ticket))
            {
                Template.League.LeagueName.leagueName = GetBetTypeName(ticket, ticketHelper);
                Template.League.Append(BuildVideoPokerGameDetails(ticket));
            }
            else
            {
                Template.League.LeagueName.leagueName = ticket.BetCheck;
            }
        }

        protected override void BuildBetTeamsInfo(ITicket ticket, ref RTFHelper rtfHelper)
        {
            if (Template.Match != null)
            {
                base.BuildBetTeamsInfo(ticket, ref rtfHelper);
            }
        }

        private static string GetBetTypeName(ITicket ticket, ITicketHelper ticketHelper)
            => ticketHelper.GetResourceData(GameTypeRefId, ticket.BetTypeId.ToString());

        private static string GetGameTypeName(ITicket ticket, ITicketHelper ticketHelper)
            => ticketHelper.GetResourceData(GameTypeRefId, ticket.TransDesc);

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
                GetTransactionType(ticket),
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

        private static string GetTransactionType(ITicket ticket)
        {
            // @TransType
            // + 0 : All (DRAW/WON/LOSE/Reject/Void/Refund/Running/Waiting)
            // + 1 : DRAW/WON/LOSE/Refund
            // + 2 : Reject/Void
            // + 3 : Running/Waiting
            // Use this because web server and db are in the same timezone
            if (ticket.WinlostDate.Date >= DateTime.Today.AddDays(-1))
            {
                switch (ticket.Status.ToLowerInvariant())
                {
                    case BetStatus.Won:
                    case BetStatus.Lose:
                    case BetStatus.Draw:
                    case BetStatus.Refund:
                        return "1";

                    case BetStatus.Reject:
                    case BetStatus.Void:
                        return "2";

                    case BetStatus.Running:
                    case BetStatus.Waiting:
                        return "3";

                    default:
                        return "0";
                }
            }

            return "0"; // Previous days Ticket
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
            Dictionary<string, string> additionalItems = new Dictionary<string, string>();

            if (string.IsNullOrEmpty(additional))
            {
                return additionalItems;
            }

            var keyValuePairs = additional.Split(new[] { '*' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var keyValuePair in keyValuePairs)
            {
                var keyValue = keyValuePair.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);

                if (keyValue.Length == 2)
                {
                    additionalItems.Add(keyValue[0], keyValue[1]);
                }
            }

            return additionalItems;
        }
    }
}