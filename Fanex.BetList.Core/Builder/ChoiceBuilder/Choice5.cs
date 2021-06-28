namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Entities;

    /// <summary>
    /// Bet Type: FT. 1x2.
    /// </summary>
    public class Choice5 : Choice1
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var betTeam = ticket.BetTeam.ToLowerInvariant();
            switch (betTeam)
            {
                case "1":
                    betTeam = ticketHelper.GetTeamNameById(ticket.HomeId);
                    break;
                case "2":
                    betTeam = ticketHelper.GetTeamNameById(ticket.AwayId);
                    break;
                default:
                    betTeam = CoreBetList.draw;
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