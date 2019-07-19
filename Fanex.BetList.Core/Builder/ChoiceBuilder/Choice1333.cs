namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Entities;

    /// <summary>
    ///  Set x Games y and y+1 Winner.
    /// </summary>
    public class Choice1333 : Choice1
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            bool betHome = ticket.BetTeam == "1";
            bool betAway = ticket.BetTeam == "2";

            if (betHome)
            {
                Template.betTeam = Template.Match.homeTeam;
            }
            else if (betAway)
            {
                Template.betTeam = Template.Match.awayTeam;
            }
            else
            {
                Template.betTeam = CoreBetList.lblNeither;
            }
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            base.BuildBetTeamClassNameAndHandicap(ticket);

            Template.Handicap.handicap = null;
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.BetType.betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId, ticket.RefNo, betId: ticket.BetId);
        }
    }
}