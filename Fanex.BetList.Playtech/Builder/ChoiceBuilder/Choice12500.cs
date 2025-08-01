namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils.TicketExt;

    public class Choice12500 : ChoiceRNGCore
    {
        protected override string RefIdForGameGroup => "PT_RNG_GG";

        protected override string BuildDetailsLink(ITicket ticket)
        {
            var detailLink = $"<a class=\"detail-link\" href=\"javascript:void('');\">{CoreBetList.details}</a>";
            var parameterList = $"'{ticket.TransId}', '{ticket.WinlostDate}', {ticket.CustId}, {ticket.GetTransactionType()}";

            return $"<div class=\"detail playtech-detail\" onclick=\"showPlaytechBetDetails({parameterList});\">{detailLink}</div>";
        }       
    }
}