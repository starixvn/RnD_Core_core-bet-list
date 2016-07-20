namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Constants;
    using Entities;
    using System.Collections.Generic;

    /// <summary>
    /// Bet type name: Both Team To Score in 1H/ 2H
    /// </summary>
    public class Choice445 : Choice1
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var betTeam = string.Empty;

            switch (ticket.BetTeam.ToLowerInvariant())
            {
                case BetTeamValue.YY:
                    betTeam = string.Format("{0}/{1}", CoreBetList.lblYes, CoreBetList.lblYes);
                    break;

                case BetTeamValue.YN:
                    betTeam = string.Format("{0}/{1}", CoreBetList.lblYes, CoreBetList.lblNo);
                    break;

                case BetTeamValue.NY:
                    betTeam = string.Format("{0}/{1}", CoreBetList.lblNo, CoreBetList.lblYes);
                    break;

                case BetTeamValue.NN:
                    betTeam = string.Format("{0}/{1}", CoreBetList.lblNo, CoreBetList.lblNo);
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