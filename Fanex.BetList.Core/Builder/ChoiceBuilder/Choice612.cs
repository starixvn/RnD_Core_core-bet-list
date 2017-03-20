namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Entities;

    /// <summary>
    /// BAIO - Quarter X Moneyline
    /// </summary>
    public class Choice612 : Choice606
    {
        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = null;
            Template.betTeamClassName = Underdog;
        }
    }
}