namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;

    /// <summary>
    /// Bet type name: 2ND Half Double Chance.
    /// </summary>
    public class Choice186 : Choice1
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
                case BetTeamValue.HD:
                    betTeam = string.Format("{0}/{1}", Template.Match.homeTeam, CoreBetList.draw);
                    break;

                case BetTeamValue.HA:
                    betTeam = string.Format("{0}/{1}", Template.Match.homeTeam, Template.Match.awayTeam);
                    break;

                case BetTeamValue.DA:
                    betTeam = string.Format("{0}/{1}", CoreBetList.draw, Template.Match.awayTeam);
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