namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Entities;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Constants;

    /// <summary>
    /// Set x Correct Score (SS).
    /// </summary>
    public class Choice1317 : Choice1302
    {
        /// <summary>
        /// Builds the bet team for Set x Correct Score.
        /// If bet team is XY then the displaying bet team is x:y.
        /// If bet team is HAOS then the displaying bet team is Home To Win Any Other Score.
        /// If bet team is AAOS then the displaying bet team is Away To Win Any Other Score.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        /// <param name="ticketData">The ticket data.</param>
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var betTeam = string.Empty;

            if (string.Equals(ticket.BetTeam, BetTeamValue.HAOS))
            {
                betTeam = CoreBetList.HomeToWinAnyOtherScore;
            }
            else if (string.Equals(ticket.BetTeam, BetTeamValue.AAOS))
            {
                betTeam = CoreBetList.AwayToWinAnyOtherScore;
            }
            else if (ticket.BetTeam.Length == 2)
            {
                betTeam = string.Join(null, new string[] { ticket.BetTeam.Substring(0, 1), ":", ticket.BetTeam.Substring(1, 1) });
            }

            Template.betTeam = string.Join(null, new string[] { HtmlCharacters.NoneBreakingSpace, betTeam });
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.BetType.betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId, ticket.BetId, ticket.BetCheck);
        }
    }
}