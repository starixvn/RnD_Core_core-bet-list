namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;

    /// <summary>
    /// Bet type name:  Result/Total Goals (3rd).
    /// </summary>
    public class Choice163 : Choice144
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
                case BetTeamValue.HU:
                    betTeam = string.Format("{0}/{1}", Template.Match.homeTeam, CoreBetList.under);
                    break;

                case BetTeamValue.HO:
                    betTeam = string.Format("{0}/{1}", Template.Match.homeTeam, CoreBetList.over);
                    break;

                case BetTeamValue.DU:
                    betTeam = string.Format("{0}/{1}", CoreBetList.draw, CoreBetList.under);
                    break;

                case BetTeamValue.DO:
                    betTeam = string.Format("{0}/{1}", CoreBetList.draw, CoreBetList.over);
                    break;

                case BetTeamValue.AU:
                    betTeam = string.Format("{0}/{1}", Template.Match.awayTeam, CoreBetList.under);
                    break;

                case BetTeamValue.AO:
                    betTeam = string.Format("{0}/{1}", Template.Match.awayTeam, CoreBetList.over);
                    break;
            }

            Template.betTeam = betTeam;
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var parentBetTypeId = ticketHelper.GetParentIdByBetTypeId(ticket.BetTypeId);

            Template.BetType.betTypeName = ticketHelper.GetBetTypeNameById(parentBetTypeId);
        }
    }
}