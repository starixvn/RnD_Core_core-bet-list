namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;

    /// <summary>
    /// Bet type name: First Corner.
    /// </summary>
    public class Choice206 : Choice5
    {
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

                default:
                    betTeam = string.Empty;
                    break;
            }

            Template.betTeam = betTeam;
        }
    }
}