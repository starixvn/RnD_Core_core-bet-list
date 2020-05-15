namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Constants;
    using Entities;

    /// <summary>
    /// Correct Map Score.
    /// </summary>
    public class Choice9088 : Choice9002
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var choice = ticket.BetTeam;

            Template.betTeam = HtmlCharacters.NoneBreakingSpace + 
                (string.IsNullOrEmpty(choice) ? choice : choice.Replace(":", "-"));
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = null;
            Template.betTeamClassName = Favorite;
        }
    }
}