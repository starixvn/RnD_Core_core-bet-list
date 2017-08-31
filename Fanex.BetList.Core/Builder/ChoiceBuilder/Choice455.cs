namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Entities;
    using Utils;

    /// <summary>
    /// PH AIO Phase 3 - Time Of 1st Goal.
    /// </summary>
    public class Choice455 : Choice1
    {
        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.betTeamClassName = Favorite;
            Template.Handicap.handicap = null;
        }
    }
}