namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;

    /// <summary>
    /// Next Odd/Even.
    /// </summary>
    public class Choice86 : Choice85
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.betTeam = ticket.BetTeam.ToLowerInvariant() == BetTeamValue.H ? CoreBetList.odd : CoreBetList.even;

            var liveScore = string.Empty;

            if (ticket.IsLive)
            {
                var betId = ticket.BetId.ToString() == "0" ? string.Empty : ticket.BetId.ToString();
                liveScore = (!ticket.IsLive || string.IsNullOrEmpty(betId)) ? " [0]" : $" [{betId}]";
            }

            Template.betTeam += liveScore;
        }
    }
}