namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;

    /// <summary>
    /// Type: Warrior.
    /// </summary>
    public class Choice88 : Choice1
    {
        protected override void BuildScore(ITicket ticket)
        {
            Template.Score = null;
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = null;
            Template.betTeamClassName = Favorite;
        }

        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
        {
            base.BuildMatch(ticket, ticketHelper);

            Template.Match.VS = null;
            Template.Match.homeTeam = string.Join(null, new string[] { CoreBetList.numbergameno, HtmlCharacters.NoneBreakingSpace, ticket.MatchCode });
        }

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            base.BuildBetTeam(ticket, ticketHelper, ticketData);

            Template.betTeam = ticket.BetTeam == BetTeamValue.H ? CoreBetList.warrior2ndBall : CoreBetList.warrior3rdBall;
        }
    }
}