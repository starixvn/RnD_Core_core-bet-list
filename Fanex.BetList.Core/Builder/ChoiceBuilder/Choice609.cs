namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Entities;
    using Utils;

    /// <summary>
    /// BAIO - 1H Winning Margin 12 Way
    /// </summary>
    public class Choice609 : Choice606
    {
        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            base.BuildBetTeamClassNameAndHandicap(ticket);

            var handicap = IsBetHomeTeam(ticket) ?
                ticket.Handicap2 - ticket.Handicap1 :
                ticket.Handicap1 - ticket.Handicap2;

            Template.Handicap.handicap = ConvertByBetType.Hdp(handicap);
        }
    }
}