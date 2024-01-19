namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Templates;
    using Fanex.BetList.Core.Utils.TicketExt;

    public class Choice5500 : ChoiceRNG
    {
        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
        {
            var bettype = $"<span class=\"handicap\"> {ticketHelper.GetBetTypeNameById(ticket.BetTypeId)}</span>";
            Template.League.LeagueName.SetValue(bettype);
            if (IsSummaryTicket(ticket.BetCheck))
            {
                Template.League.Append(BuildDetailLink(ticket));
            }
        }

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
            => Template.betTeam = string.IsNullOrEmpty(ticket.BetCheck) ? string.Empty : $"{ticket.BetCheck}";

        private static Choice_League_Block BuildDetailLink(ITicket ticket)
        {
            var detailLinkMarkupBuilder = new StringBuilder();
            var detailFunction = BuildDetailFunction(ticket);

            detailLinkMarkupBuilder
                .Append("<div class=\"detail rngwar-casino-detail\" onclick='")
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

            detailFunctionBuilder.Append("showRNGWARBetDetails(&quot;")
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
    }
}
