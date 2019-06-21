namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;

    /// <summary>
    /// Bet type name: Both Team To Score in 1H/ 2H
    /// </summary>
    public class Choice445 : Choice1
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            switch (ticket.BetTeam.ToLowerInvariant())
            {
                case BetTeamValue.YY:
                    Template.betTeam = $"{CoreBetList.lblYes}/{CoreBetList.lblYes}";
                    break;

                case BetTeamValue.YN:
                    Template.betTeam = $"{CoreBetList.lblYes}/{CoreBetList.lblNo}";
                    break;

                case BetTeamValue.NY:
                    Template.betTeam = $"{CoreBetList.lblNo}/{CoreBetList.lblYes}";
                    break;

                case BetTeamValue.NN:
                    Template.betTeam = $"{CoreBetList.lblNo}/{CoreBetList.lblNo}";
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