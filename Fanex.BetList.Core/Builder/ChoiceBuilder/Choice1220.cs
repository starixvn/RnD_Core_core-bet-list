namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Entities;

    /// <summary>
    /// Virtual Tennis - PlayerWin.
    /// </summary>
    public class Choice1220 : Choice1
    {
        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap = null;
            Template.betTeamClassName = Underdog;
        }
    }
}