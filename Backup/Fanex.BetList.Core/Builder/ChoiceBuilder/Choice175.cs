namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;

    /// <summary>
    /// Bet type name: Match Decided Method.
    /// </summary>
    public class Choice175 : Choice1
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
                case BetTeamValue.HR:
                    betTeam = string.Format("{0}/{1}", Template.Match.homeTeam, CoreBetList.RegularTime);
                    break;

                case BetTeamValue.HE:
                    betTeam = string.Format("{0}/{1}", Template.Match.homeTeam, CoreBetList.ExtraTime);
                    break;

                case BetTeamValue.HP:
                    betTeam = string.Format("{0}/{1}", Template.Match.homeTeam, CoreBetList.Penalty);
                    break;

                case BetTeamValue.AR:
                    betTeam = string.Format("{0}/{1}", Template.Match.awayTeam, CoreBetList.RegularTime);
                    break;

                case BetTeamValue.AE:
                    betTeam = string.Format("{0}/{1}", Template.Match.awayTeam, CoreBetList.ExtraTime);
                    break;

                case BetTeamValue.AP:
                    betTeam = string.Format("{0}/{1}", Template.Match.awayTeam, CoreBetList.Penalty);
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