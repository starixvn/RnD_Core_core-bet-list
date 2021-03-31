namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Constants;
    using Entities;
    using System.Collections.Generic;

    /// <summary>
    /// Exact Quarter Draw
    /// </summary>
    public class Choice640 : Choice1
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            string choice = ticket.BetTeam;

            if (BetTeamValue.NumberOne == choice)
            {
                choice = BetTeamValue.NumberOneOver;
            }

            // "&nbsp" because export format
            Template.betTeam = HtmlCharacters.NoneBreakingSpace + choice;
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = null;
            Template.betTeamClassName = Favorite;
        }
    }
}