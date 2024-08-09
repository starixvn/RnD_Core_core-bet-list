namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Entities;
    using Templates;
    
    public class Choice11050 : Choice11054
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