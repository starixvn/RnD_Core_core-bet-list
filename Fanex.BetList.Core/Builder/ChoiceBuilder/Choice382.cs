namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;

    /// <summary>
    /// Pool House All In One Phase 5: Home Penalty Shootout - Round X.
    /// </summary>
    public class Choice382 : Choice385
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            switch (ticket.BetTeam.ToLowerInvariant())
            {
                case BetTeamValue.S:
                    Template.betTeam = CoreBetList.Score;
                    break;

                case BetTeamValue.M:
                    Template.betTeam = CoreBetList.Miss;
                    break;

                default:
                    Template.betTeam = string.Empty;
                    break;
            }
        }
    }
}