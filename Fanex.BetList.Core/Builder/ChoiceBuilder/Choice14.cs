namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Entities;
    using Fanex.BetList.Core.App_GlobalResources;

    /// <summary>
    /// First Goal/Last Goal.
    /// </summary>
    public class Choice14 : Choice1
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var betTeam = string.Empty;

            switch (ticket.BetTeam)
            {
                case "0:0":
                    betTeam = CoreBetList.NoGoalBetType14;
                    break;
                case "1:1":
                    betTeam = CoreBetList.HomeFG;
                    break;
                case "1:2":
                    betTeam = CoreBetList.HomeLG;
                    break;
                case "2:1":
                    betTeam = CoreBetList.AwayFG;
                    break;
                case "2:2":
                    betTeam = CoreBetList.AwayLG;
                    break;
            }

            Template.betTeam = betTeam;
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.betTeamClassName = Favorite;
            Template.Handicap.handicap = null;
        }
    }
}