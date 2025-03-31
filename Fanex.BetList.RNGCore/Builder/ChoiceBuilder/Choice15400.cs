namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils.TicketExt;

    public class Choice15400 : ChoiceRNG
    {
        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
        {
            base.BuildLeague(ticket, ticketHelper);

            var gameGroup= "ODG_RNG_GG";
            Template.League.LeagueName.SetValue($"<span class=\"gamegroupname\"> {ticketHelper.GetResourceData(gameGroup, ticket.BetTypeId.ToString())}</span>");
        }
        
        protected override string BuildDetailsLink(ITicket ticket)
        {
            var detailLink = $"<a class=\"detail-link\" href=\"javascript:void('');\">{CoreBetList.details}</a>";
            var parameterList = $"'{ticket.TransId}', '{ticket.WinlostDate}', {ticket.CustId}, {ticket.GetTransactionType()}";

            return $"<div class=\"detail OdingGaming-detail\" onclick=\"showOdinGamingBetDetails({parameterList});\">{detailLink}</div>";
        }
    }
}
