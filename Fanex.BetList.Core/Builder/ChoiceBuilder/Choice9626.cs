using System.Collections.Generic;
using Fanex.BetList.Core.App_GlobalResources;
using Fanex.BetList.Core.Constants;
using Fanex.BetList.Core.Entities;

namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    public class Choice9626 : Choice1
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
            => Template.betTeam = ticket.BetTeam.ToLowerInvariant() == BetTeamValue.N ? CoreBetList.lblNo : CoreBetList.lblYes;

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
            => Template.BetType.betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId, ticket.BetId, ticket.BetCheck);

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = null;
            Template.betTeamClassName = Favorite;
        }
    }
}