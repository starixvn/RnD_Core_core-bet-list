namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Constants;
    using Entities;
    using System.Collections.Generic;
    using Utils;

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
                    betTeam = CoreBetList.YesAndOver;
                    break;

                case BetTeamValue.YU:
                    betTeam = CoreBetList.YesAndUnder;
                    break;

                case BetTeamValue.NO:
                    betTeam = CoreBetList.NoAndOver;
                    break;

                case BetTeamValue.NU:
                    betTeam = CoreBetList.NoAndUnder;
                    break;

                default:
                    betTeam = string.Empty;
                    break;
            }

            Template.betTeam = betTeam;
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = ConvertByBetType.Hdp(ticket.Handicap1);
            Template.betTeamClassName = Favorite;
        }
    }
}