namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Constants;
    using Entities;
    using System.Collections.Generic;

    /// <summary>
    /// Bet type name: First Goal Method
    /// </summary>
    public class Choice424 : Choice1
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var betTeam = string.Empty;

            switch (ticket.BetTeam.ToLowerInvariant())
            {
                case BetTeamValue.S:
                    betTeam = CoreBetList.Shot;
                    break;

                case BetTeamValue.H:
                    betTeam = CoreBetList.Header;
                    break;

                case BetTeamValue.P:
                    betTeam = CoreBetList.Penalty;
                    break;

                case BetTeamValue.FK:
                    betTeam = CoreBetList.FreeKick;
                    break;

                case BetTeamValue.OG:
                    betTeam = CoreBetList.OwnGoal;
                    break;

                case BetTeamValue.NG:
                    betTeam = CoreBetList.nogoal;
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