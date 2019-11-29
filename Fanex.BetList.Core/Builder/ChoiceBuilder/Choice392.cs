namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;

    /// <summary>
    /// Pool House All In One Phase 5: Penalty Shootout Correct Score.
    /// </summary>
    public class Choice392 : Choice413
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var betTeam = ticket.BetTeam.Trim().ToLowerInvariant();

            switch (betTeam)
            {
                case BetTeamValue.HAOS:
                    Template.betTeam = CoreBetList.HomeToWinAnyOtherScore;
                    break;

                case BetTeamValue.AAOS:
                    Template.betTeam = CoreBetList.AwayToWinAnyOtherScore;
                    break;

                default:
                    Template.betTeam = betTeam;
                    break;
            }
        }

        protected override bool IsAOS(ITicket ticket)
        {
            var betTeam = ticket.BetTeam.Trim().ToLowerInvariant();

            return !string.IsNullOrEmpty(ticket.TransDesc) && (betTeam == BetTeamValue.HAOS || betTeam == BetTeamValue.AAOS);
        }
    }
}