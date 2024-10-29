namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Constants;
    using Entities;
    using App_GlobalResources;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Utils;

    /// <summary>
    /// Betby - Over/Under
    /// </summary>
    public class Choice13004 : Choice13003
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