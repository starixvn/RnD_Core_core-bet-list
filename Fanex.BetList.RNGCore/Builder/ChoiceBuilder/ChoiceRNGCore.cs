namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils.TicketExt;

    public class ChoiceRNGCore : ChoiceRNG
    {
        protected virtual string RefIdForGameGroup => string.Empty;

        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
        {
            Template.League.LeagueName.SetValue($"<span class=\"gamegroupname\"> {ticketHelper.GetResourceData(RefIdForGameGroup, ticket.BetTypeId.ToString())}</span>");

            if (IsSummaryTicket(ticket.BetCheck))
            {
                Template.League.Append(BuildDetailsLink(ticket, ticketHelper));
            }
        }

        private static string BuildDetailsLink(ITicket ticket, ITicketHelper ticketHelper)
        {
            var detailLink = $"<a class=\"detail-link\" href=\"javascript:void('');\">{CoreBetList.details}</a>";
            var parameterList = $"'{ticket.TransId}', '{ticket.WinlostDate}', {ticket.CustId}, {ticket.GetTransactionType()}, {ticket.SportTypeId}, '{ticketHelper.GetSportNameById(ticket.SportTypeId)}'";

            return $"<div class=\"detail rng-detail\" onclick=\"showRNGBetDetails({parameterList});\">{detailLink}</div>";
        }
    }
}