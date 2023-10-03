namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{    
    using Entities;

    public class Choice5302 : Choice2701
    {
        protected override void GeneralMatchRoundInfo(ITicket ticket, ITicketHelper ticketHelper)
        {
            _matchRoundInfo = VirtualSportsChoiceUtils.ParseMatchInfo3(ticket.TransDesc, ticketHelper);
        }
    }
}