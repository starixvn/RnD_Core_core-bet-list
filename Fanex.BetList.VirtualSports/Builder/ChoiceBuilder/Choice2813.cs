namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Entities;
    using Templates;

    /// <summary>
    /// BetRadar Virtual Basketball - Ending Game.
    /// </summary>
    public class Choice2813 : Choice2801
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var choice = ticket.BetTeam;
            Template.betTeam = string.IsNullOrEmpty(choice) ? choice : choice.Replace(":", "-");
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = null;
            Template.betTeamClassName = Favorite;
        }
    }
}