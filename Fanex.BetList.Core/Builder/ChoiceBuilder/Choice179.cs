namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;
    using Mapper;

    /// <summary>
    /// Bet type name: Exact 1H Goals.
    /// </summary>
    public class Choice179 : Choice1
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
                case BetTeamValue.G0:
                    betTeam = CoreBetList.ZeroGoals;
                    break;

                case BetTeamValue.G1:
                    betTeam = CoreBetList.OneGoal;
                    break;

                case BetTeamValue.G2:
                    betTeam = CoreBetList.TwoGoals;
                    break;

                case BetTeamValue.G3:
                    betTeam = CoreBetList.ThreeGoals;
                    break;

                case BetTeamValue.G4:
                    betTeam = CoreBetList.FourAndOver;
                    break;

                default:
                    betTeam = string.Empty;
                    break;
            }

            Template.betTeam = betTeam;
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = null;
            Template.betTeamClassName = Favorite;
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var betTypeName = GetParentBetTypeName(ticket, ticketHelper);

            Template.BetType.betTypeName = betTypeName;
        }

        private static string GetParentBetTypeName(ITicket ticket, ITicketHelper ticketHelper)
        {
            var parentBetTypeId = MapperCollector.GetParentBetTypeOf(ticket.BetTypeId);

            return ticketHelper.GetBetTypeNameById(parentBetTypeId, ticket.RefNo);
        }
    }
}