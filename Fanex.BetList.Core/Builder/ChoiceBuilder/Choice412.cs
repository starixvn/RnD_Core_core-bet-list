namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Entities;

    public class Choice412 : Choice1
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var betTeam = string.Empty;

            switch (ticket.BetTeam.ToLowerInvariant())
            {
                case "0":
                    betTeam = CoreBetList.nogoal;
                    break;

                case "1":
                    betTeam = CoreBetList.OneGoal;
                    break;

                case "2":
                    betTeam = CoreBetList.TwoGoals;
                    break;

                case "3&over":
                    betTeam = CoreBetList.ThreeAndOver;
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