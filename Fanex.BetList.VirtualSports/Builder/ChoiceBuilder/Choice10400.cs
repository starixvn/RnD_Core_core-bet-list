namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Entities;
    public class Choice10400 : Choice1
    {
        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
        {
            base.BuildMatch(ticket, ticketHelper);

            var _matchRoundInfo = VirtualSportsChoiceUtils.ParseMatchInfoVS(VirtualSportsChoiceUtils.VS4MatchCode, ticket.TransDesc, ticketHelper);
            Template.AppendMatchOrRoundInfo(_matchRoundInfo);
        }
    }
}