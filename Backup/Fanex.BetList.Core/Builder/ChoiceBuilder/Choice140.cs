namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;

    /// <summary>
    /// BetRadar BetType : Choice of bet type Highest Scoring Half.
    /// </summary>
    public class Choice140 : Choice133
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

            switch (ticket.BetTeam.ToLower())
            {
                case BetTeamValue.TwoH:
                    betTeam = CoreBetList.SecondHalf;
                    break;

                case BetTeamValue.OneH:
                    betTeam = CoreBetList.FirstHalf;
                    break;

                case BetTeamValue.Tie:
                    betTeam = CoreBetList.Tie;
                    break;
            }

            Template.betTeam = betTeam;
        }
    }
}