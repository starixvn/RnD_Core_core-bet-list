namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Templates;

    public class Choice1801 : Choice1
    {
        private const string OpenBetTypeNameTag = "<span class='ag-casino-bet-type'>";
        private const string CloseBetTypeNameTag = "</span>";

        private const string OpenGameNameTag = "<span class='ag-casino-game-name'>";
        private const string CloseGameNameTag = "</span>";

        private const string RoundName = "ROUND";
        private const string TableCodeName = "TABLECODE";
        public const string BetChoiceDataName = "TYPE";

        private static readonly IDictionary<int, string> BetTypeRefIdNames = new Dictionary<int, string>
        {
            { 1801, "AG_Baccarat" },
            { 1802, "AG_Roulette" },
            { 1803, "AG_DragonTiger" },
            { 1804, "AG_SicBo" }
        };

        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
        {
            Template.Match = null;
        }

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.betTeam = string.Empty;

            if (!string.IsNullOrWhiteSpace(ticket.TransDesc))
            {
                Template.betTeam = GetBetChoice(ticket, ticketHelper);
            }
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap = null;
            Template.betTeamClassName = Favorite;
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.BetType = null;
        }

        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
        {
            var betType = BuildBetTypeName(ticket.BetTypeId, ticketHelper);

            Template.League.LeagueName.leagueName = betType;

            if (!string.IsNullOrWhiteSpace(ticket.TransDesc))
            {
                Template.League.LeagueName.leagueName += string.Format("{0} {1}{2}", OpenGameNameTag, GetRoomAndTable(ticket, ticketHelper), CloseGameNameTag);
            }
            else
            {
                Template.League.Append(BuildShowMicroGamingRnGBetsDetails(ticket));
            }
        }

        private static string BuildBetTypeName(int betTypeId, ITicketHelper ticketHelper)
        {
            var betTypeName = ticketHelper.GetBetTypeNameById(betTypeId);

            return string.Format("{0}{1}{2}", OpenBetTypeNameTag, betTypeName, CloseBetTypeNameTag);
        }

        protected override void AdjustBetTeamToRTF()
        {
            Template.League.LeagueName.leagueName = Template.League.LeagueName.leagueName
                .Replace(OpenBetTypeNameTag, string.Empty)
                .Replace(CloseBetTypeNameTag, string.Empty);

            Template.League.LeagueName.leagueName = Template.League.LeagueName.leagueName
                .Replace(OpenGameNameTag, string.Empty)
                .Replace(CloseGameNameTag, string.Empty);
        }

        private static Choice_League_Block BuildShowMicroGamingRnGBetsDetails(ITicket ticket)
        {
            string[] detailsFunctionCompositions =
            {
                "showAGCasinoDetails(&quot;",
                ticket.TransId.ToString(),
                "&quot;",
                ",&quot;",
                ticket.WinlostDate.ToString(CultureInfo.InvariantCulture),
                "&quot;",
                ",&quot;",
                ticket.CustId.ToString(),
                "&quot;",
                ",&quot;",
                GetTransactionType(ticket),
                "&quot;);"
            };

            string detailsFunction = string.Join(null, detailsFunctionCompositions);

            string[] detailsFunctionDivCompositions =
            {
                "<div class=\"detail\" onclick='",
                detailsFunction,
                "'>",
                "<a style=\"color:#755200;font-weight:bold\" href=\"javascript:void(&quot;&quot;);\">",
                CoreBetList.details,
                "</a>",
                "</div>"
            };

            string markups = string.Join(null, detailsFunctionDivCompositions);

            var detailtTemplateBlock = new Choice_League_Block();

            detailtTemplateBlock.SetValue(markups);

            return detailtTemplateBlock;
        }

        private static string GetTransactionType(ITicket ticket)
        {
            ////@TransType
            ////+ 0 : All (DRAW/WON/LOSE/Reject/Void/Refund/Running/Waiting)
            ////+ 1 : DRAW/WON/LOSE/Refund
            ////+ 2 : Reject/Void
            ////+ 3 : Running/Waiting
            //// Use this because web server vs db is same GMT
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

            return "0"; ////Yesterday Ticket
        }

        private static string GetRoomAndTable(ITicket ticket, ITicketHelper ticketHelper)
        {
            var transDecsParsed = DeserializeTransDesc(ticket.TransDesc.ToUpperInvariant());

            var roomAndName = string.Empty;

            if (transDecsParsed.ContainsKey(RoundName))
            {
                roomAndName += ticketHelper.GetResourceData("AG_Room", transDecsParsed[RoundName]) + " ";
            }

            if (transDecsParsed.ContainsKey(TableCodeName))
            {
                roomAndName += transDecsParsed[TableCodeName];
            }

            return roomAndName;
        }

        private string GetBetChoice(ITicket ticket, ITicketHelper ticketHelper)
        {
            var transDecsParsed = DeserializeTransDesc(ticket.TransDesc.ToUpperInvariant());

            if (transDecsParsed.ContainsKey(BetChoiceDataName))
            {
                return GetBetChoiceName(ticketHelper, ticket.BetTypeId, transDecsParsed);
            }

            return string.Empty;
        }

        protected virtual string GetBetChoiceName(ITicketHelper ticketHelper, int betTypeId, Dictionary<string, string> transDecsParsed)
        {
            return ticketHelper.GetResourceData(BetTypeRefIdNames[betTypeId], transDecsParsed[BetChoiceDataName]);
        }

        private static Dictionary<string, string> DeserializeTransDesc(string transDesc)
        {
            const int numberOfExpressionsOfEachProperty = 2;

            var transDescItems = new Dictionary<string, string>();

            if (string.IsNullOrEmpty(transDesc))
            {
                return transDescItems;
            }

            var keyValuePairs = transDesc.Split(new[] { '*' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var keyValuePair in keyValuePairs)
            {
                var keyValue = keyValuePair.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                if (keyValue.Length == numberOfExpressionsOfEachProperty)
                {
                    transDescItems.Add(keyValue[0], keyValue[1]);
                }
            }

            return transDescItems;
        }
    }
}