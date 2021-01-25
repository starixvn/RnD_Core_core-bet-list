namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;
    using Fanex.BetList.Core.Utils;

    /// <summary>
    /// Home Inns Runs Fancy.
    /// </summary>
    public class Choice9541 : Choice1
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
            => Template.betTeam = ticket.BetTeam.ToLowerInvariant() == BetTeamValue.N ? CoreBetList.lblNo : CoreBetList.lblYes;

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            var handicap = ticket.BetTeam.ToLowerInvariant() == BetTeamValue.N ? ticket.Handicap1 : ticket.Handicap2;
            Template.Handicap.handicap = ConvertByBetType.Hdp(handicap);

            Template.betTeamClassName = Favorite;
        }
    }
}