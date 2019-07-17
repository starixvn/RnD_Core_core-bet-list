namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;
    using Mapper;

    /// <summary>
    /// Bet type name: Exact 2ND Half Goals.
    /// </summary>
    public class Choice187 : Choice179
    {
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
                    betTeam = CoreBetList.TwoGoalsAndOver;
                    break;

                default:
                    betTeam = string.Empty;
                    break;
            }

            Template.betTeam = betTeam;
        }
    }
}