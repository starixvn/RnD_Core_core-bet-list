namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Entities;
    using Fanex.BetList.Core.Utils;

    /// <summary>
    /// Betby - Handicap
    /// </summary>
    public class Choice13003 : Choice13002
    {
        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            var isBetHomeTeam = IsBetHomeTeam(ticket);

            string betTeamClassName;

            if (isBetHomeTeam)
            {
                betTeamClassName = ticket.Handicap2 >= ticket.Handicap1 ? Underdog : Favorite;
            }
            else
            {
                betTeamClassName = ticket.Handicap2 <= ticket.Handicap1 ? Underdog : Favorite;
            }

            Template.betTeamClassName = betTeamClassName;

            decimal handicap = ticket.Handicap2 > ticket.Handicap1 ? ticket.Handicap2 : ticket.Handicap1;
            handicap = betTeamClassName == Favorite ? -handicap : handicap;

            Template.Handicap.handicap = ConvertByBetType.Hdp(handicap);
        }
    }
}