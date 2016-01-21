namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Templates;
    using System.Text;
    using System.Collections.Generic;

    public class Choice2401 : Choice1101
    {
        private const string OpenBetTypeNameTag = "<span class='allbet-bet-type-name'>";
        private const string CloseBetTypeNameTag = "</span>";

        private const string OpenBetTypeIdTag = "<span class='allbet-bet-type-id'>";
        private const string CloseBetTypeIdTag = "</span>";

        private const string OpenGameIdTag = "<span class='allbet-game-id'>";
        private const string CloseGameIdTag = "</span>";

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
                return GetBetChoiceName(ticketHelper, ticket.BetTypeId, transDecsParsed);
            }

            return string.Empty;
        }

        protected virtual string GetBetChoiceName(ITicketHelper ticketHelper, int betTypeId, Dictionary<string, string> transDecsParsed)
        {
            return ticketHelper.GetResourceData(AllBetRefId, transDecsParsed[BetChoiceDataKey]);
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

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            string betTypeId = "BetID: " + ticket.BetTypeId;
            Template.BetType.betTypeName = string.Format("{0}{1}{2}", OpenBetTypeIdTag, betTypeId, CloseBetTypeIdTag);
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
                Template.League.LeagueName.leagueName += string.Format("{0}{1}{2}", OpenGameIdTag, ticket.BetTypeId, CloseGameIdTag);
            }
        }

        private Choice_League_Block BuildDetailLink(ITicket ticket)
        {
            StringBuilder detailLinkMarkupBuilder = new StringBuilder();
            string detailFunction = BuildDetailFunction(ticket);

            detailLinkMarkupBuilder
                .Append("<div class=\"detail\" onclick='")
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
                .Append(ticket.TransId)
                .Append("&quot;")
                .Append(",&quot;")
                .Append(ticket.StatusId)
                .Append("&quot;")
                .Append(",&quot;")
                .Append(ticket.TransDate)
                .Append("&quot;);");

            return detailFunctionBuilder.ToString();
        }

        private string BuildBetTypeName(ITicket ticket, ITicketHelper ticketHelper)
        {
            var betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetId);

            return string.Format("{0}{1}{2}", OpenBetTypeNameTag, betTypeName, CloseBetTypeNameTag);
        }

    }
}
