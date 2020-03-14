namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.Entities;

    public class Choice1902 : Choice1901
    {
        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
        {
            Template.Match = null;
        }
    }
}