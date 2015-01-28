namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;

    /// <summary>
    /// Bet type name: Extra Time 1H 1X2.
    /// </summary>
    public class Choice167 : Choice1
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var betTeam = string.Empty;

            switch (ticket.BetTeam.ToLowerInvariant())
            {
                case "1":
                    betTeam = CoreBetList.ExtraTimeHTDotOne;
                    break;
                case "2":
                    betTeam = CoreBetList.ExtraTimeHTDotTwo;
                    break;
                case BetTeamValue.X:
                    betTeam = CoreBetList.ExtraTimeHTDotX;
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