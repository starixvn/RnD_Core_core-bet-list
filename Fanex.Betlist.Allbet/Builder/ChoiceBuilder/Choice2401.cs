namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Templates;
    using Resource;

    public class Choice2401 : Choice1101
    {
        private const string OpenBetTypeNameTag = "<span class='allbet-bet-type-name'>";
        private const string CloseBetTypeNameTag = "</span>";

        private const string OpenBetIdTag = "<span class='allbet-bet-id'>";
        private const string CloseBetIdTag = "</span>";

        private const string AllBetRefId = "AllBet_BT";
        private const string BetChoiceDataKey = "CHOICE";

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.betTeam = string.Empty;

            if (!string.IsNullOrWhiteSpace(ticket.TransDesc))
            {
                Template.betTeam = GetBetChoice(ticket, ticketHelper);
            }
        }

        private string GetBetChoice(ITicket ticket, ITicketHelper ticketHelper)
        {
            var transDecsParsed = DeserializeTransDesc(ticket.TransDesc.ToUpper());

            if (transDecsParsed.ContainsKey(BetChoiceDataKey))
            {
                return ticketHelper.GetResourceData(AllBetRefId, transDecsParsed[BetChoiceDataKey]);
            }

            return string.Empty;
        }

        private Dictionary<string, string> DeserializeTransDesc(string transDesc)
        {
            Dictionary<string, string> transDescItems = new Dictionary<string, string>();

            if (string.IsNullOrEmpty(transDesc))
            {
                return transDescItems;
            }

            var keyValuePairs = transDesc.Split(new char[] { '*' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var keyValuePair in keyValuePairs)
            {
                var keyValue = keyValuePair.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                if (keyValue.Length == 2)
                {
                    transDescItems.Add(keyValue[0], keyValue[1]);
                }
            }

            return transDescItems;
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap = null;
            Template.betTeamClassName = Favorite;
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.BetType.betTypeName = string.Empty;

            if (!string.IsNullOrEmpty(ticket.TransDesc))
            {
                string betId = AllbetLang.BetID + ": " + ticket.BetCheck;
                Template.BetType.betTypeName = string.Format("{0}{1}{2}", OpenBetIdTag, betId, CloseBetIdTag);
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
                Template.League.LeagueName.leagueName += " " + ticket.MatchCode;
            }
        }

        private Choice_League_Block BuildDetailLink(ITicket ticket)
        {
            StringBuilder detailLinkMarkupBuilder = new StringBuilder();
            string detailFunction = BuildDetailFunction(ticket);

            detailLinkMarkupBuilder
                .Append("<div class=\"detail allbet-casino-detail\" onclick='")
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

        private string BuildDetailFunction(ITicket ticket)
        {
            StringBuilder detailFunctionBuilder = new StringBuilder();

            detailFunctionBuilder.Append("showTodayRunningBetsDetails(&quot;")
                .Append(ticket.TransId.ToString())
                .Append("&quot;")
                .Append(",&quot;")
                .Append(ticket.WinlostDate.ToString())
                .Append("&quot;")
                .Append(",&quot;")
                .Append(ticket.CustId.ToString())
                .Append("&quot;")
                .Append(",&quot;")
                .Append(GetTransactionType(ticket))
                .Append("&quot;);");

            return detailFunctionBuilder.ToString();
        }

        private string GetTransactionType(ITicket ticket)
        {
            if (ticket.WinlostDate.Date >= DateTime.Today.AddDays(-1))
            {
                switch (ticket.Status.ToLower())
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
                }
            }

            return "0";
        }

        private string BuildBetTypeName(ITicket ticket, ITicketHelper ticketHelper)
        {
            var betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId);

            return string.Format("{0}{1}{2}", OpenBetTypeNameTag, betTypeName, CloseBetTypeNameTag);
        }

        protected override void AdjustBetTeamToRTF()
        {
            RemoveHtmlTagFromLeagueName();
            RemoveHtmlTagFromBetTypeName();
        }

        private void RemoveHtmlTagFromLeagueName()
        {
            Template.League.LeagueName.leagueName = Template.League.LeagueName.leagueName
               .Replace(OpenBetTypeNameTag, string.Empty)
               .Replace(CloseBetTypeNameTag, string.Empty);
        }

        private void RemoveHtmlTagFromBetTypeName()
        {
            Template.BetType.betTypeName = Template.BetType.betTypeName
                .Replace(OpenBetIdTag, string.Empty)
                .Replace(CloseBetIdTag, string.Empty);
        }
    }
}