namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Entities;
    using Utils;

    /// <summary>
    ///   Set x Games y and y+1 Points.
    /// </summary>
    public class Choice1334 : Choice1
    {
        /// <summary>
        /// Builds the bet team.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        /// <param name="ticketData">The ticket data.</param>
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            bool betHome = ticket.BetTeam == "1";
            bool betAway = ticket.BetTeam == "2";

            Template.betTeam = betHome
                ? string.Join(null, new[] { "<span class=\"underdog\">", CoreBetList.under, "</span>" })
                : betAway
                    ? string.Join(null, new[] { "<span class=\"favorite\">", CoreBetList.over, "</span>" })
                    : string.Join(null, new[] { "<span class=\"underdog\">", CoreBetList.exactly, "</span>" });
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            base.BuildBetTeamClassNameAndHandicap(ticket);

            Template.Handicap.handicap = ConvertByBetType.Hdp(ticket.Handicap1);
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.BetType.betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId, ticket.RefNo, betId: ticket.BetId);
        }
    }
}