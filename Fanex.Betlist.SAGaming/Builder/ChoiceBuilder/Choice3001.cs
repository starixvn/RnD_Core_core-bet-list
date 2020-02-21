namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;
    using App_GlobalResources;
    using Entities;
    using Fanex.BetList.Core.Utils.StringExt;
    using Resource;
    using Templates;
    using Utils;

    public class Choice3001 : Choice1
    {
        private const string BetTeamFormat = "<div><span class='favorite'>{0}</span>&nbsp;<span class='handicap custom'>({1})</span></div>";

        private const string BetTypeNameTag = "<span class='sagaming-bet-type-name'>{0}</span>";

        private const string BetIdTag = "<span class='sagaming-bet-id'>{0}</span>";

        private const string GameIdTag = "<span class='sagaming-game-id'>{0}</span>";

        private const string TableIdFormat = "<span class=\"gamegroupname\">{0}</span>";

        private const string SAGamingBetRefId = "SA_BT";
        private const string TableCodeKey = "TableCode";

        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
        {
            Template.Match = null;
        }

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
            => Template.betTeam = GetBetChoice(ticket, ticketHelper);

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap = null;
            Template.betTeamClassName = string.Empty;
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.BetType.betTypeName = string.Empty;

            if (!string.IsNullOrEmpty(ticket.TransDesc))
            {
                Template.BetType.betTypeName = string.Format(BetIdTag, $"{SAGamingResources.BetId}: {ticket.BetId}");
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
                Template.League.LeagueName.leagueName += " " + GetTableCode(ticket) + " " + BuildGameId(ticket);
            }
        }

        private static string GetBetChoice(ITicket ticket, ITicketHelper ticketHelper)
        {
            var betTeam = new StringBuilder();
            var choiceOption = ticket.ParseBetcheck();
            foreach (var item in choiceOption)
            {
                var choiceName = ticketHelper.GetResourceData(SAGamingBetRefId, item.Key);
                var choiceStake = item.Value;

                betTeam.AppendFormat(BetTeamFormat, choiceName, choiceStake);
            }

            return betTeam.ToString();
        }

        protected static Choice_League_Block BuildDetailLink(ITicket ticket)
        {
            var detailLinkMarkupBuilder = new StringBuilder();
            var detailFunction = BuildDetailFunction(ticket);

            detailLinkMarkupBuilder
                .Append("<div class=\"detail sagaming-casino-detail\" onclick='")
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
                .Append(GetTransactionType(ticket))
                .Append("&quot;);");

            return detailFunctionBuilder.ToString();
        }

        private static string GetTransactionType(ITicket ticket)
        {
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

            return "0";
        }

        protected static string BuildBetTypeName(ITicket ticket, ITicketHelper ticketHelper)
        {
            var betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId);

            return string.Format(BetTypeNameTag, betTypeName);
        }

        private static string GetTableCode(ITicket ticket)
        {
            var transDescOption = ticket.ParseTransDesc();
            if (transDescOption != null && transDescOption.ContainsKey(TableCodeKey))
            {
                return string.Format(TableIdFormat, transDescOption[TableCodeKey]);
            }

            return string.Empty;
        }

        private static string BuildGameId(ITicket ticket)
            => string.Format(GameIdTag, ticket.MatchCode);

        protected override void AdjustBetTeamToRTF()
        {
            Template.betTeam = string.Join("\n", Template.betTeam.SplitHTMLIntoRows());
            Template.League.LeagueName.leagueName = string.Join(string.Empty, Template.League.LeagueName.leagueName.StripHTML());
            Template.BetType.betTypeName = string.Join(string.Empty, Template.BetType.betTypeName.StripHTML());
        }
    }
}