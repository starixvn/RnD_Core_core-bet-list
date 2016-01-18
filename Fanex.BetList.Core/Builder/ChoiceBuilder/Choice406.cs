namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;

    /// <summary>
    /// Pool House All In One: Exact Total Goals.
    /// Its parent bet type is Exact Total Goals of Bet Radar, bet type ID is 159.
    /// </summary>
    public class Choice406 : Choice1
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
                    betTeam = CoreBetList.TwoGoals;
                    break;

                case BetTeamValue.G3:
                    betTeam = CoreBetList.ThreeGoals;
                    break;

                case BetTeamValue.G4:
                    betTeam = CoreBetList.FourGoals;
                    break;

                case BetTeamValue.G5:
                    betTeam = CoreBetList.FiveGoals;
                    break;

                case BetTeamValue.G6:
                    betTeam = CoreBetList.SixAndOver;
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
            var parentBetTypeId = ticketHelper.GetParentIdByBetTypeId(ticket.BetTypeId);

            Template.BetType.betTypeName = ticketHelper.GetBetTypeNameById(parentBetTypeId);
        }
    }
}