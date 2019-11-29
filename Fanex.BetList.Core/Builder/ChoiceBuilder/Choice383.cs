namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;

    /// <summary>
    /// Pool House All In One Phase 5: Penalty Shootout Woodwork - Round X.
    /// </summary>
    public class Choice383 : Choice385
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            switch (ticket.BetTeam.ToLowerInvariant())
            {
                case BetTeamValue.H:
                    Template.betTeam = Template.Match.homeTeam;
                    break;

                case BetTeamValue.A:
                    Template.betTeam = Template.Match.awayTeam;
                    break;

                default:
                    Template.betTeam = string.Empty;
                    break;
            }
        }
    }
}