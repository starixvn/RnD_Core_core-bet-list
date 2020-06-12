namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;
    using App_GlobalResources;
    using Entities;
    using Fanex.BetList.Core.Utils.StringExt;
    using Templates;
    using Utils;

    public class Choice5601 : Choice1
    {
        private const string BetTeamFormat = "<div><span class='favorite'>{0}</span>&nbsp;<span class='handicap custom'>({1}{2})</span></div>";
        private const string OpenBetTypeNameTag = "<span class='bet-type-name'>";
        private const string OpenBetIdTag = "<span class='bet-id'>";
        private const string CloseSpanTag = "</span>";
        private const string ChoiceKey = "Choice";

        private IDictionary<string, string> _parsedTransDesc;
        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap = null;
            Template.betTeamClassName = Underdog;
        }

        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
            => Template.Match = null;

        public override Choice_Template Render(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowScoreMap)
        {
            _parsedTransDesc = ticket.ParseTransDesc();

            return base.Render(ticket, ticketHelper, ticketData, isShowScoreMap);
        }

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
            => Template.betTeam = GetChoiceInfo(ticket, ticketHelper);

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.BetType.betTypeName = string.Empty;

            if (!ticket.IsSummaryTicket())
            {
                Template.BetType.betTypeName = $"{OpenBetIdTag}{CoreBetList.BetID}: {ticket.BetCheck}{CloseSpanTag}";
            }
        }

        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
        {
            var bettype = BuildBetTypeName(ticket, ticketHelper);
            if (ticket.IsSummaryTicket())
            {
                Template.League.LeagueName.leagueName = bettype;
                Template.League.Append(BuildDetailLink(ticket));
            }
            else
            {
                Template.League.LeagueName.leagueName = bettype;
            }
        }

        protected override void AdjustBetTeamToRTF()
        {
            Template.betTeam = string.Join("\n", Template.betTeam.SplitHTMLIntoRows());
            Template.League.LeagueName.leagueName = string.Join(string.Empty, Template.League.LeagueName.leagueName.StripHTML());
            RemoveHtmlTagFromBetTypeName();
        }
        private string GetChoiceInfo(ITicket ticket, ITicketHelper ticketHelper)
        {
            var betTeam = new StringBuilder();
            var choices = _parsedTransDesc.ContainsKey(ChoiceKey) ? _parsedTransDesc[ChoiceKey].Split(';') : new string[] { };
            foreach (var choice in choices)
            {
                var choiceInfo = choice.Split(':');
                const int minInfoLength = 2;
                if (choiceInfo.Length >= minInfoLength)
                {
                    var choiceId = choiceInfo[0];
                    var amount = Cast.AsDecimal(choiceInfo[1]);
                    var refResourceId = $"SBO_ION_BT_{ticket.BetTypeId}";
                    var choiceName = ticketHelper.GetResourceData(refResourceId, choiceId);

                    betTeam.AppendFormat(BetTeamFormat, choiceName, $"{ticket.Currency} ", ConvertByBetType.Stake(amount));
                }
            }

            return betTeam.ToString();
        }

        private static string BuildDetailFunction(ITicket ticket)
        {
            var detailFunctionBuilder = new StringBuilder();

            detailFunctionBuilder.Append("showIONCasinoBetDetails(&quot;")
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

        private static Choice_League_Block BuildDetailLink(ITicket ticket)
        {
            var detailLinkMarkupBuilder = new StringBuilder();
            var detailFunction = BuildDetailFunction(ticket);

            detailLinkMarkupBuilder
                .Append("<div class=\"detail ioncasino-detail\" onclick='")
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

        private static string BuildBetTypeName(ITicket ticket, ITicketHelper ticketHelper)
        {
            var betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId);

            return $"{OpenBetTypeNameTag}{betTypeName}{CloseSpanTag}";
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

        private void RemoveHtmlTagFromBetTypeName()
            => Template.BetType.betTypeName = Template.BetType.betTypeName
                .Replace(OpenBetIdTag, string.Empty)
                .Replace(CloseSpanTag, string.Empty);
    }
}