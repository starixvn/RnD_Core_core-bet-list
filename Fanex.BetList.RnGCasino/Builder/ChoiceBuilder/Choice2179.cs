namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Templates;

    /// <summary>
    /// Jackpot Game
    /// </summary>
    public class Choice2179 : Choice2101
    {
        private const string JackpotInfo = "jp=1";
        private const string JackpotTransIdInfo = "jpw=";

        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
        {
            base.BuildLeague(ticket, ticketHelper);

            var isDetailedTicket = !string.IsNullOrEmpty(ticket.BetCheck);

            if (isDetailedTicket && TicketHasJackpotInfo(ticket))
            {
                Template.League.Append(BuildShowProgressiveJackpotWonDetails(ticket));
            }
        }

        private static bool TicketHasJackpotInfo(ITicket ticket)
            => !string.IsNullOrEmpty(ticket.JackpotInfo) && ticket.JackpotInfo.ToLowerInvariant().Contains(JackpotInfo);

        private static Choice_League_Block BuildShowProgressiveJackpotWonDetails(ITicket ticket)
        {
            var indexOfJackpotTransId = ticket.JackpotInfo.LastIndexOf(JackpotTransIdInfo, StringComparison.InvariantCultureIgnoreCase) + 4;
            var transId = ticket.JackpotInfo.Substring(indexOfJackpotTransId, ticket.JackpotInfo.Length - indexOfJackpotTransId);
            var parames = new List<object>
            {
                $"'{transId}'",
                $"'{ticket.CustId}'",
                $"'{ticket.BetTypeId}'",
                $"'{ticket.WinlostDate.ToString(CultureInfo.InvariantCulture)}'"
            };
            var parameterList = string.Join(", ", parames);
            var detailLinkMarkup =
                $"<div class=\"detail rng-casino-detail\" onclick=\"showProgressiveJackpotWonDetails({parameterList});\">" +
                $"<a class=\"detail-link\" href=\"javascript:void('');\">{CoreBetList.ProgressiveJackpotWonDetails}</a>" +
                $"</div>";

            var detailtTemplateBlock = new Choice_League_Block();

            detailtTemplateBlock.SetValue(detailLinkMarkup);

            return detailtTemplateBlock;
        }
    }
}