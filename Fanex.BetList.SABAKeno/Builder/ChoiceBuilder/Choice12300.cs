namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils.TicketExt;

    public class Choice12300 : ChoiceRNG
    {   
        protected override void BuildSport(ITicket ticket, ITicketHelper ticketHelper)
        {
            if (Template.League == null)
            {
                return;
            }

            Template.League.sportTypeName = ticketHelper.GetSportNameById(ticket.SportTypeId);
        }

        protected override string BuildDetailsLink(ITicket ticket)
        {
            var detailLink =
                $"<a class=\"detail-link\" href=\"javascript:void('');\">{CoreBetList.details}</a>";
            var parameterList =
                $"'{ticket.TransId}', '{ticket.WinlostDate}', {ticket.CustId}, {ticket.GetTransactionType()}";

            return $"<div class=\"detail sabakeno-detail\" onclick=\"showSabaKenoDetails({parameterList});\">{detailLink}</div>";
        }
    }
}
