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
        /// Builds the choice.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        /// <param name="ticketData">The ticket data.</param>
        protected override void BuildChoice(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            base.BuildChoice(ticket, ticketHelper, ticketData);

            Template.Handicap.handicap = ConvertByBetType.Hdp(ticket.Handicap1);
            bool team1 = ticket.BetTeam == "1";
            bool team2 = ticket.BetTeam == "2";

            if (team1)
            {
                Template.betTeam = string.Join(null, new string[] { "<span class=\"underdog\">", CoreBetList.under, "</span>" });
            }
            else
            {
                if (team2)
                {
                    Template.betTeam = string.Join(null, new string[] { "<span class=\"favorite\">", CoreBetList.over, "</span>" });
                }
                else
                {
                    Template.betTeam = string.Join(null, new string[] { "<span class=\"underdog\">", CoreBetList.exactly, "</span>" });
                }
            }
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper)
        {
            Template.BetType.betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId, ticket.BetId);
        }
    }
}