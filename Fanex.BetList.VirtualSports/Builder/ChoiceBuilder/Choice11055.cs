namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Utils;
    using Entities;
    using Templates;
    
    public class Choice11055 : Choice11054
    {
        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            base.BuildBetTeamClassNameAndHandicap(ticket);

            Template.Handicap.handicap = ConvertByBetType.Hdp(ticket.Handicap1);
        }

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.betTeam = BetTeamValue.H == ticket.BetTeam.ToLowerInvariant() ? CoreBetList.over : CoreBetList.under;
        }
    }
}
