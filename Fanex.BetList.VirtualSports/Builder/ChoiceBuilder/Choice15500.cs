namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Entities;

    public class Choice15500 : Choice2770
    {
        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
        {
            var _matchRoundInfo = VirtualSportsChoiceUtils.ParseMatchInfo("KVS_MatchCode", ticket.TransDesc, ticketHelper);
            Template.Match.homeTeam = _matchRoundInfo;
            Template.Match.awayTeam = string.Empty;
            Template.Match.home_firstGoal_lastGoal = string.Empty;
            Template.Match.away_firstGoal_lastGoal = string.Empty;
            Template.Match.VS = null;
        }       
    }
}