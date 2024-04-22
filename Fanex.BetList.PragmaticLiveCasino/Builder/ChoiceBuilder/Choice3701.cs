namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;
    using App_GlobalResources;
    using Entities;
    using Fanex.BetList.Core.Utils.StringExt;
    using Fanex.BetList.Core.Utils.TicketExt;
    using Templates;

    public class Choice3701 : ChoiceLC
    {
        private const string BetTypeNameTag = "<span class='bet-type-name'>{0}</span>";
        private const string BetIdTag = "<span class='bet-id'>{0}</span>";
        private const string GameIdTag = "<span class='game-id'>{0}</span>";

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            base.BuildBetType(ticket, ticketHelper, ticketData);

            Template.BetType.Visible = false;
        }

        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
        {
            Template.League.LeagueName.leagueName = BuildBetTypeName(ticket, ticketHelper);

            if (string.IsNullOrEmpty(ticket.BetCheck))
            {
                Template.League.Append(BuildDetailLink(ticket));
            }
            else
            {
                Template.League.LeagueName.leagueName += " " + BuildGameId(ticket);
            }
        }

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
            => Template.betTeam = GetBetChoice(ticket);

        private string GetBetChoice(ITicket ticket)
        {
            var betTeam = new StringBuilder();

            if (!string.IsNullOrEmpty(ticket.BetCheck))
            {
                var _parseTransId3rd = GetTransId3rd(ticket.BetCheck);
                betTeam.AppendFormat(BetIdTag, $"{CoreBetList.BetID}: {_parseTransId3rd}");
            }

            return betTeam.ToString();
        }

        private static Choice_League_Block BuildDetailLink(ITicket ticket)
        {
            var detailLinkMarkupBuilder = new StringBuilder();
            var detailFunction = BuildDetailFunction(ticket);

            detailLinkMarkupBuilder
                .Append("<div class=\"detail pragmatic-live-casino-detail\" onclick='")
                .Append(detailFunction)
                .Append("'>")
                .Append("<a class=\"detail-link\" href=\"javascript:void(&quot;&quot;);\">")
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

            detailFunctionBuilder.Append("showPragmaticLiveCasinoBetsDetails(&quot;")
                .Append(ticket.TransId.ToString())
                .Append("&quot;")
                .Append(",&quot;")
                .Append(ticket.WinlostDate.ToString(CultureInfo.InvariantCulture))
                .Append("&quot;")
                .Append(",&quot;")
                .Append(ticket.CustId.ToString())
                .Append("&quot;")
                .Append(",&quot;")
                .Append(ticket.GetTransactionType())
                .Append("&quot;);");

            return detailFunctionBuilder.ToString();
        }        

        private static string BuildBetTypeName(ITicket ticket, ITicketHelper ticketHelper)
        {
            var betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId);

            return string.Format(BetTypeNameTag, betTypeName);
        }

        private static string BuildGameId(ITicket ticket)
            => string.Format(GameIdTag, ticket.MatchCode);
    }
}