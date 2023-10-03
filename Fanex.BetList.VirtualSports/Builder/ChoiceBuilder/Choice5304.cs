namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{    
    using Entities;

    public class Choice5304 : Choice2703
    {
        protected override void GeneralMatchRoundInfo(ITicket ticket, ITicketHelper ticketHelper)
        {
            _matchRoundInfo = VirtualSportsChoiceUtils.ParseMatchInfo3(ticket.TransDesc, ticketHelper);
        }
    }
}