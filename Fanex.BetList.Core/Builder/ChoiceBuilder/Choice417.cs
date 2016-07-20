namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Constants;
    using Entities;
    using System.Collections.Generic;

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
                    betTeam = string.Format("{0}/{1}", CoreBetList.lblYes, Template.Match.homeTeam);
                    break;

                case BetTeamValue.YD:
                    betTeam = string.Format("{0}/{1}", CoreBetList.lblYes, CoreBetList.draw);
                    break;

                case BetTeamValue.YA:
                    betTeam = string.Format("{0}/{1}", CoreBetList.lblYes, Template.Match.awayTeam);
                    break;

                case BetTeamValue.NH:
                    betTeam = string.Format("{0}/{1}", CoreBetList.lblNo, Template.Match.homeTeam);
                    break;

                case BetTeamValue.ND:
                    betTeam = string.Format("{0}/{1}", CoreBetList.lblNo, CoreBetList.draw);
                    break;

                case BetTeamValue.NA:
                    betTeam = string.Format("{0}/{1}", CoreBetList.lblNo, Template.Match.awayTeam);
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