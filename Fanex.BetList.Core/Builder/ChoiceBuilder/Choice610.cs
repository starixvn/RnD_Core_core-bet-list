namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;
    using Utils;

    /// <summary>
    /// BAIO - Quarter X Over Under
    /// </summary>
    public class Choice610 : Choice606
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