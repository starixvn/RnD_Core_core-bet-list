namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Constants;
    using Entities;
    using System.Collections.Generic;

    /// <summary>
    /// Quarter X Last Digit Score
    /// </summary>
    public class Choice627 : Choice1
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var choice = string.IsNullOrWhiteSpace(ticket.BetTeam) ? string.Empty : ticket.BetTeam.ToUpperInvariant();

            // "&nbsp" because export format
            Template.betTeam = HtmlCharacters.NoneBreakingSpace + choice;
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = null;
            Template.betTeamClassName = Favorite;
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.BetType.betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId, ticket.RefNo, ticket.BetId, ticket.BetCheck);
        }
    }
}