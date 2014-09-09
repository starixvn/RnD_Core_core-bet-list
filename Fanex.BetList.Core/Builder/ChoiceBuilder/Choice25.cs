namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Entities;

    /// <summary>
    /// Draw no bet.
    /// </summary>
    public class Choice25 : Choice1
    {
        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = null;
            Template.betTeamClassName = Underdog;
        }
    }
}