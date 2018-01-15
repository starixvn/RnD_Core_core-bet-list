namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;

    /// <summary>
    /// Bet type name:  Result/Total Goals (3rd).
    /// </summary>
    public class Choice163 : Choice143
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var betTeam = string.Empty;

            switch (ticket.BetTeam.ToLowerInvariant())
            {
                case BetTeamValue.HU:
                    betTeam = string.Format("{0}/{1}", CoreBetList.Home, CoreBetList.under);
                    break;

                case BetTeamValue.HO:
                    betTeam = string.Format("{0}/{1}", CoreBetList.Home, CoreBetList.over);
                    break;

                case BetTeamValue.DU:
                    betTeam = string.Format("{0}/{1}", CoreBetList.draw, CoreBetList.under);
                    break;

                case BetTeamValue.DO:
                    betTeam = string.Format("{0}/{1}", CoreBetList.draw, CoreBetList.over);
                    break;

                case BetTeamValue.AU:
                    betTeam = string.Format("{0}/{1}", CoreBetList.Away, CoreBetList.under);
                    break;

                case BetTeamValue.AO:
                    betTeam = string.Format("{0}/{1}", CoreBetList.Away, CoreBetList.over);
                    break;

                default:
                    betTeam = string.Empty;
                    break;
            }

            Template.betTeam = betTeam;
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var parentBetTypeId = ticketHelper.GetParentIdByBetTypeId(ticket.BetTypeId);

            Template.BetType.betTypeName = ticketHelper.GetBetTypeNameById(parentBetTypeId, ticket.RefNo);
        }
    }
}