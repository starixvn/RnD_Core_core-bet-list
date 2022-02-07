using Fanex.BetList.Core.Entities;

namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    public class Choice9662 : Choice9664
    {
        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = null;
            Template.betTeamClassName = Underdog;
        }
    }
}