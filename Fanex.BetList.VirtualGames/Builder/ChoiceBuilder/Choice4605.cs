namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.Entities;

    public class Choice4605 : Choice4601
    {
        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
        {
            var tableCode = GetTableCode(ticket.BetCheck);

            Template.League.LeagueName.leagueName = string.IsNullOrEmpty(tableCode) ? string.Empty : $"[{ticket.BetCheck}]";
        }

    }
}