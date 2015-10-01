namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Entities;
    using Fanex.BetList.Core.Utils;

    public class Choice302 : Choice301
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.betTeam = IsBetHomeTeam(ticket) ? CoreBetList.over : CoreBetList.under;
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            base.BuildBetTeamClassNameAndHandicap(ticket);

            Template.Handicap.handicap = ConvertByBetType.Hdp(ticket.Handicap1);

            BuildOddsSpread(ticket);
        }
    }
}