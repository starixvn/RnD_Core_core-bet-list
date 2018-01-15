namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;

    public class Choice186 : Choice1
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var betTeam = string.Empty;
            string betTeamTemplate = "{0}<span class=\"underdog\"> " + CoreBetList.lblOr + " </span>{1}";

            switch (ticket.BetTeam.ToLowerInvariant())
            {
                case BetTeamValue.HD:
                    betTeam = string.Format(betTeamTemplate, CoreBetList.Home, CoreBetList.draw);
                    break;

                case BetTeamValue.HA:
                    betTeam = string.Format(betTeamTemplate, CoreBetList.Home, CoreBetList.Away);
                    break;

                case BetTeamValue.DA:
                    betTeam = string.Format(betTeamTemplate, CoreBetList.Away, CoreBetList.draw);
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