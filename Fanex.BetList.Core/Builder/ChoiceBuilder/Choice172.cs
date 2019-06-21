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
            switch (ticket.BetTeam.ToLowerInvariant())
            {
                case BetTeamValue.HH:
                    Template.betTeam = $"{CoreBetList.Home}/{CoreBetList.Home}";
                    break;

                case BetTeamValue.DH:
                    Template.betTeam = $"{CoreBetList.draw}/{CoreBetList.Home}";
                    break;

                case BetTeamValue.HA:
                    Template.betTeam = $"{CoreBetList.Home}/{CoreBetList.Away}";
                    break;

                case BetTeamValue.AH:
                    Template.betTeam = $"{CoreBetList.Away}/{CoreBetList.Home}";
                    break;

                case BetTeamValue.DA:
                    Template.betTeam = $"{CoreBetList.draw}/{CoreBetList.Away}";
                    break;

                case BetTeamValue.AA:
                    Template.betTeam = $"{CoreBetList.Away}/{CoreBetList.Away}";
                    break;

                case BetTeamValue.NO:
                    Template.betTeam = CoreBetList.none;
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
    }
}