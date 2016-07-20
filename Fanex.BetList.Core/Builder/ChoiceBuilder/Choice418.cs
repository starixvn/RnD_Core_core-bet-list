namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Constants;
    using Entities;
    using System.Collections.Generic;

    /// <summary>
    /// Bet type name: Both Teams to Score/ Total Goal
    /// </summary>
    public class Choice418 : Choice1
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var betTeam = string.Empty;

            switch (ticket.BetTeam.ToLowerInvariant())
            {
                case BetTeamValue.YO:
                    betTeam = string.Format("{0} {1}", CoreBetList.YesAndOver, ticket.Handicap1);
                    break;

                case BetTeamValue.YU:
                    betTeam = string.Format("{0} {1}", CoreBetList.YesAndUnder, ticket.Handicap1);
                    break;

                case BetTeamValue.NO:
                    betTeam = string.Format("{0} {1}", CoreBetList.NoAndOver, ticket.Handicap1);
                    break;

                case BetTeamValue.NU:
                    betTeam = string.Format("{0} {1}", CoreBetList.NoAndUnder, ticket.Handicap1);
                    break;
            }

            Template.betTeam = betTeam;
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = null;
            Template.betTeamClassName = Favorite;
        }
    }
}