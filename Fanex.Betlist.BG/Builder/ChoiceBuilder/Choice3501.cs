namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;
    using App_GlobalResources;
    using Entities;
    using Fanex.BetList.Core.Utils;
    using Fanex.BetList.Core.Utils.StringExt;
    using Templates;

    public class Choice3501 : Choice1
    {
        private const string BetTypeNameTag = "<span class='bet-type-name'>{0}</span>";
        private const string BetIdTag = "<span class='bet-id'>{0}</span>";
        private const string GameIdTag = "<span class='game-id'>{0}</span>";

        public virtual string GetRefResourceId() => "BG_BT";

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            base.BuildBetType(ticket, ticketHelper, ticketData);

            Template.BetType.Visible = false;
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.betTeamClassName = Favorite;
            Template.Handicap = null;
        }

        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
            => Template.Match = null;

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var choiceKey = TransDescParser.GetChoiceKey(ticket.TransDesc);
            var choiceName = ticketHelper.GetResourceData(GetRefResourceId(), choiceKey);
            var betTeam = new StringBuilder(choiceName);

            if (!string.IsNullOrEmpty(ticket.TransDesc))
            {
                betTeam.AppendFormat(BetIdTag, $"{CoreBetList.BetID}: {ticket.BetCheck}");
            }

            Template.betTeam = betTeam.ToString();
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
                Template.League.LeagueName.leagueName += " " + BuildGameId(ticket);
            }
        }

        protected override void AdjustBetTeamToRTF()
        {
            Template.betTeam = Template.betTeam.Replace("<span class='bet-id'>", "\n");
            Template.betTeam = string.Join(string.Empty, Template.betTeam.StripHTML());
            Template.League.LeagueName.leagueName = string.Join(string.Empty, Template.League.LeagueName.leagueName.StripHTML());
        }

        protected override void BuildBetTypeName(ref RTFHelper rtfHelper)
            => rtfHelper.RTFRenderer.AddText(string.Empty);

        private static Choice_League_Block BuildDetailLink(ITicket ticket)
        {
            var detailLinkMarkupBuilder = new StringBuilder();
            var detailFunction = BuildDetailFunction(ticket);

            detailLinkMarkupBuilder
                .Append("<div class=\"detail bg-casino-detail\" onclick='")
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

            detailFunctionBuilder.Append("showBGBetDetails(&quot;")
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
                    case BetStatus.Reject:
                    case BetStatus.Void:
                        return "2";

                    case BetStatus.Won:
                    case BetStatus.Lose:
                    case BetStatus.Draw:
                    case BetStatus.Refund:
                        return "1";

                    case BetStatus.Running:
                    case BetStatus.Waiting:
                        return "3";

                    default:
                        return "0";
                }
            }

            return "0";
        }

        private static string BuildGameId(ITicket ticket)
            => string.Format(GameIdTag, ticket.MatchCode);

        private static string BuildBetTypeName(ITicket ticket, ITicketHelper ticketHelper)
        {
            var betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId);

            return string.Format(BetTypeNameTag, betTypeName);
        }
    }
}