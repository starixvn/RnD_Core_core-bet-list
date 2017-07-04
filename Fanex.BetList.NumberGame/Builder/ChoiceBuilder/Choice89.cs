namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Entities;

    /// <summary>
    /// Next Combo
    /// do NOT show odds type because this bet type just offer for DEC
    /// </summary>
    public class Choice89 : Choice87
    {
        /// <summary>
        /// Builds the bet team.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        /// <param name="ticketData">The ticket data.</param>
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var betId = ticket.BetId.ToString() == "0" ? string.Empty : ticket.BetId.ToString();
            var liveScore = (!ticket.IsLive || string.IsNullOrEmpty(betId)) ? " [0]" : $" [{betId}]";
            var betTeam = string.Empty;

            switch (ticket.BetTeam)
            {
                case "1:1":
                    betTeam = CoreBetList.lblOverOdd;
                    break;

                case "1:2":
                    betTeam = CoreBetList.lblOverEven;
                    break;

                case "2:1":
                    betTeam = CoreBetList.lblUnderOdd;
                    break;

                case "2:2":
                    betTeam = CoreBetList.lblUnderEven;
                    break;

                default:
                    betTeam = string.Empty;
                    break;
            }

            Template.betTeam = betTeam + NumberGameValue + liveScore;
        }
    }
}