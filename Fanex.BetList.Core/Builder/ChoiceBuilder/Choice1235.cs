namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Entities;

    /// <summary>
    /// Virtual Tennis - ScoreBet.
    /// </summary>
    public class Choice1235 : Choice1220
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

            switch (ticket.BetTeam.Trim())
            {
                case "1":
                    betTeam = CoreBetList.virtualtennis_scorebet_win_0;
                    break;
                case "2":
                    betTeam = CoreBetList.virtualtennis_scorebet_win_15;
                    break;
                case "3":
                    betTeam = CoreBetList.virtualtennis_scorebet_win_30;
                    break;
                case "4":
                    betTeam = CoreBetList.virtualtennis_scorebet_win_40;
                    break;
                case "5":
                    betTeam = CoreBetList.virtualtennis_scorebet_0_win;
                    break;
                case "6":
                    betTeam = CoreBetList.virtualtennis_scorebet_15_win;
                    break;
                case "7":
                    betTeam = CoreBetList.virtualtennis_scorebet_30_win;
                    break;
                case "8":
                    betTeam = CoreBetList.virtualtennis_scorebet_40_win;
                    break;
            }

            Template.betTeam = betTeam;
        }
    }
}