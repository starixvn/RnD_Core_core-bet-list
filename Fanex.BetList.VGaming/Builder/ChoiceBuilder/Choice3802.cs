namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils.TicketExt;
    using System;
    using System.Globalization;

    public class Choice3802 : ChoiceRNG
    {
        private const string JackpotTransIdInfo = "jpw=";
        private const string JackpotInfo = "jp=1";
        
        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
        {
            base.BuildLeague(ticket, ticketHelper);

            if (IsDetailTicket(ticket.BetCheck) && TicketHasJackpotInfo(ticket))
            {
                Template.League.Append(BuildShowJackpotWonDetails(ticket));
            }
        }

        private static bool TicketHasJackpotInfo(ITicket ticket)
            => !string.IsNullOrEmpty(ticket.JackpotInfo) && ticket.JackpotInfo.ToLowerInvariant().Contains(JackpotInfo);

        private static string BuildShowJackpotWonDetails(ITicket ticket)
        {
            var indexOfJackpotTransId = ticket.JackpotInfo.LastIndexOf(JackpotTransIdInfo, StringComparison.InvariantCultureIgnoreCase) + 4;
            var transId = ticket.JackpotInfo.Substring(indexOfJackpotTransId, ticket.JackpotInfo.Length - indexOfJackpotTransId);
            var detailLinkMarkup =
                $"<div class=\"detail vgaming-detail\" onclick=\"showVGamingJackpotWonDetails('{transId}', '{ticket.CustId}', '{ticket.BetTypeId}', '{ticket.WinlostDate.ToString(CultureInfo.InvariantCulture) }');\">" +
                $"<a class=\"detail-link\" href=\"javascript:void('');\">{CoreBetList.JackpotWonDetails}</a>" +
                $"</div>";

            return detailLinkMarkup;
        }

        protected override string BuildDetailsLink(ITicket ticket)
        {
            var detailLink = $"<a class=\"detail-link\" href=\"javascript:void('');\">{CoreBetList.details}</a>";
            var parameterList = $"'{ticket.TransId}', '{ticket.WinlostDate}', {ticket.CustId}, {ticket.GetTransactionType()}";

            return $"<div class=\"detail vgaming-detail\" onclick=\"showVGamingDetails({parameterList});\">{detailLink}</div>";
        }
    }
}