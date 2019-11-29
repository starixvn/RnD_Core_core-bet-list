namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;

    /// <summary>
    /// Pool House All In One Phase 5: Penalty Shootout 1X2 - Round X.
    /// </summary>
    public class Choice385 : Choice1
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var betTeam = ticket.BetTeam.Trim().ToLowerInvariant();

            switch (betTeam)
            {
                case BetTeamValue.NumberOne:
                    Template.betTeam = CoreBetList.Home;
                    break;

                case BetTeamValue.NumberTwo:
                    Template.betTeam = CoreBetList.Away;
                    break;

                case BetTeamValue.X:
                    Template.betTeam = CoreBetList.draw;
                    break;

                default:
                    Template.betTeam = string.Empty;
                    break;
            }
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = null;
            Template.betTeamClassName = Favorite;
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
            => Template.BetType.betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId, ticket.RefNo, ticket.BetId, ticket.BetCheck);
    }
}