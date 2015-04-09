namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;

    /// <summary>
    /// Bet type name: First Corner.
    /// </summary>
    public class Choice206 : Choice5
    {
        /// <summary>
        /// Builds the bet team.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        /// <param name="ticketData">The ticket data.</param>
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var betTeam = string.Empty;

            switch (ticket.BetTeam.ToLowerInvariant())
            {
                case BetTeamValue.H:
                    betTeam = Template.Match.homeTeam;
                    break;

                case BetTeamValue.A:
                    betTeam = Template.Match.awayTeam;
                    break;

                case BetTeamValue.N:
                    betTeam = CoreBetList.none;
                    break;
            }

            Template.betTeam = betTeam;
        }
    }
}