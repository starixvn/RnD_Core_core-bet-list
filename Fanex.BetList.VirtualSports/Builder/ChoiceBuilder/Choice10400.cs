namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Entities;
    public class Choice10400 : Choice1
    {
        private const string VS4MatchCode = "EGVS_MatchCode";

        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
        {
            base.BuildMatch(ticket, ticketHelper);

            var _matchRoundInfo = VirtualSportsChoiceUtils.ParseMatchInfo(VS4MatchCode, ticket.TransDesc, ticketHelper);
            Template.AppendMatchOrRoundInfo(_matchRoundInfo);
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = null;
            Template.betTeamClassName = Underdog;
        }
    }
}