namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;
    using Utils;

    /// <summary>
    /// Bet Type: Home Team Quarters Win Over/Under.
    /// </summary>
    public class Choice638 : Choice1
    {
        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.betTeamClassName = BetTeamValue.O == ticket.BetTeam.ToLowerInvariant() ? Favorite : Underdog;

            Template.Handicap.handicap = ConvertByBetType.Hdp(ticket.Handicap1);
        }

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.betTeam = BetTeamValue.O == ticket.BetTeam.ToLowerInvariant() ? CoreBetList.over : CoreBetList.under;
        }
    }
}