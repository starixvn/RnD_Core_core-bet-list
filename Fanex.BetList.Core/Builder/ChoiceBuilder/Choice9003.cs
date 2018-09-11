namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;
    using Utils;

    /// <summary>
    /// E-Sports 2.0: Map X Total Kills Over/Under.
    /// </summary>
    public class Choice9003 : Choice9002
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.betTeam = BetTeamValue.O == ticket.BetTeam.ToLowerInvariant() ? CoreBetList.over : CoreBetList.under;
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = ConvertByBetType.Hdp(ticket.Handicap1);
            Template.betTeamClassName = BetTeamValue.O == ticket.BetTeam.ToLowerInvariant() ? Favorite : Underdog;
        }
    }
}