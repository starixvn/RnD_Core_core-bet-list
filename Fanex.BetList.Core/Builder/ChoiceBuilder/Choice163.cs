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
            switch (ticket.BetTeam.ToLowerInvariant())
            {
                case BetTeamValue.HU:
                    Template.betTeam = $"{CoreBetList.Home}/{CoreBetList.under}";
                    break;

                case BetTeamValue.HO:
                    Template.betTeam = $"{CoreBetList.Home}/{CoreBetList.over}";
                    break;

                case BetTeamValue.DU:
                    Template.betTeam = $"{CoreBetList.draw}/{CoreBetList.under}";
                    break;

                case BetTeamValue.DO:
                    Template.betTeam = $"{CoreBetList.draw}/{CoreBetList.over}";
                    break;

                case BetTeamValue.AU:
                    Template.betTeam = $"{CoreBetList.Away}/{CoreBetList.under}";
                    break;

                case BetTeamValue.AO:
                    Template.betTeam = $"{CoreBetList.Away}/{CoreBetList.over}";
                    break;

                default:
                    Template.betTeam = string.Empty;
                    break;
            }
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var parentBetTypeId = ticketHelper.GetParentIdByBetTypeId(ticket.BetTypeId);

            Template.BetType.betTypeName = ticketHelper.GetBetTypeNameById(parentBetTypeId, ticket.RefNo);
        }
    }
}