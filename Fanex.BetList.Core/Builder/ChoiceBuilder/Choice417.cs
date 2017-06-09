namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;

    /// <summary>
    /// Bet type name: Both Team Score/ Result
    /// </summary>
    public class Choice417 : Choice1
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var betTeam = string.Empty;

            switch (ticket.BetTeam.ToLowerInvariant())
            {
                case BetTeamValue.YH:
                    betTeam = string.Format("{0}/{1}", CoreBetList.lblYes, CoreBetList.Home);
                    break;

                case BetTeamValue.YD:
                    betTeam = string.Format("{0}/{1}", CoreBetList.lblYes, CoreBetList.draw);
                    break;

                case BetTeamValue.YA:
                    betTeam = string.Format("{0}/{1}", CoreBetList.lblYes, CoreBetList.Away);
                    break;

                case BetTeamValue.NH:
                    betTeam = string.Format("{0}/{1}", CoreBetList.lblNo, CoreBetList.Home);
                    break;

                case BetTeamValue.ND:
                    betTeam = string.Format("{0}/{1}", CoreBetList.lblNo, CoreBetList.draw);
                    break;

                case BetTeamValue.NA:
                    betTeam = string.Format("{0}/{1}", CoreBetList.lblNo, CoreBetList.Away);
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