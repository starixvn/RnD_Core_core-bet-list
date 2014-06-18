namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Entities;

    /// <summary>
    /// Outright bet type.
    /// </summary>
    public class Choice10 : Choice1
    {
        protected override void BuildChoice(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            this.BuildScore(ticket);

            this.BuildBetAndMatch(ticket, ticketHelper);

            this.BuildLeague(ticket, ticketHelper);
        }

        protected override void BuildBetAndMatch(ITicket ticket, ITicketHelper ticketHelper)
        {
            this.Template.Handicap.handicap = null;

            this.UpdateAllMatchMemberToNull();

            this.Template.BetType.betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId);
            this.Template.betTeam = ticketHelper.GetTeamNameById(ticket.HomeId == 0 ? ticket.AwayId : ticket.HomeId);
            this.Template.betTeamClassName = BLFAVORITE;
        }
    }
}