namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;

    /// <summary>
    /// Bet type name: 1X2 and First Team To Score.
    /// </summary>
    public class Choice172 : Choice1
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
                case BetTeamValue.HH:
                    betTeam = string.Format("{0}/{1}", Template.Match.homeTeam, Template.Match.homeTeam);
                    break;

                case BetTeamValue.DH:
                    betTeam = string.Format("{0}/{1}", CoreBetList.draw, Template.Match.homeTeam);
                    break;

                case BetTeamValue.HA:
                    betTeam = string.Format("{0}/{1}", Template.Match.homeTeam, Template.Match.awayTeam);
                    break;

                case BetTeamValue.AH:
                    betTeam = string.Format("{0}/{1}", Template.Match.awayTeam, Template.Match.homeTeam);
                    break;

                case BetTeamValue.DA:
                    betTeam = string.Format("{0}/{1}", CoreBetList.draw, Template.Match.awayTeam);
                    break;

                case BetTeamValue.AA:
                    betTeam = string.Format("{0}/{1}", Template.Match.awayTeam, Template.Match.awayTeam);
                    break;

                case BetTeamValue.NO:
                    betTeam = CoreBetList.none;
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