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
        /// <summary>
        /// Builds the choice.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        /// <param name="ticketData">The ticket data.</param>
        protected override void BuildChoice(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            base.BuildChoice(ticket, ticketHelper, ticketData);

            Template.Handicap.handicap = null;
            bool team1 = ticket.BetTeam == "1";
            bool team2 = ticket.BetTeam == "2";

            if (team1)
            {
                Template.betTeam = Template.Match.homeTeam;
            }
            else if (team2)
            {
                Template.betTeam = Template.Match.awayTeam;
            }
            else
            {
                Template.betTeam = CoreLang.lblNeither;
            }
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.BetType.betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId, ticket.BetId);
        }
    }
}