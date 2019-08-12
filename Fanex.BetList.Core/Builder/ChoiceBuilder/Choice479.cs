namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils;

    /// <summary>
    /// PHAIO4B - Time of First Corner.
    /// </summary>
    public class Choice479 : Choice1
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var betOption = ticket.BetTeam.ToLowerInvariant() == BetTeamValue.Y
                ? CoreBetList.CornerBefore
                : CoreBetList.NoCornerBefore;

            Template.betTeam = string.Format(betOption, ConvertByBetType.Hdp(ticket.Handicap1));
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = null;
            Template.betTeamClassName = Favorite;
        }
    }
}