namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Entities;
    using Fanex.BetList.Core.Constants;

    /// <summary>
    /// Money Line.
    /// </summary>
    public class Choice20 : Choice1
    {
        private const int MuayThaiSportId = 44;

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = null;

            if (ticket.SportTypeId != MuayThaiSportId)
            {
                Template.betTeamClassName = Underdog;
            }
            else
            {
                if (ticket.BetTeam.ToLower() == BetTeamValue.H)
                {
                    Template.betTeamClassName = Favorite;
                }
                else
                {
                    Template.betTeamClassName = "favorite light-blue";
                }
            }
        }
    }
}