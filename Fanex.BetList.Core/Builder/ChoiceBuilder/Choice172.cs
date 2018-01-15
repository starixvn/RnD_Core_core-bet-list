namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;

    /// <summary>
    /// Bet type name: 1X2 and First Team To Score.
    /// </summary>
    public class Choice172 : Choice1
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var betTeam = string.Empty;

            switch (ticket.BetTeam.ToLowerInvariant())
            {
                case BetTeamValue.HH:
                    betTeam = string.Format("{0}/{1}", CoreBetList.Home, CoreBetList.Home);
                    break;

                case BetTeamValue.DH:
                    betTeam = string.Format("{0}/{1}", CoreBetList.draw, CoreBetList.Home);
                    break;

                case BetTeamValue.HA:
                    betTeam = string.Format("{0}/{1}", CoreBetList.Home, CoreBetList.Away);
                    break;

                case BetTeamValue.AH:
                    betTeam = string.Format("{0}/{1}", CoreBetList.Away, CoreBetList.Home);
                    break;

                case BetTeamValue.DA:
                    betTeam = string.Format("{0}/{1}", CoreBetList.draw, CoreBetList.Away);
                    break;

                case BetTeamValue.AA:
                    betTeam = string.Format("{0}/{1}", CoreBetList.Away, CoreBetList.Away);
                    break;

                case BetTeamValue.NO:
                    betTeam = CoreBetList.none;
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
    }
}