namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;

    /// <summary>
    /// Clean Sheet.
    /// </summary>
    public class Choice13 : Choice1
    {
        /// <summary>
        /// Builds the choice.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        /// <param name="ticketData">The ticket data.</param>
        protected override void BuildChoice(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            base.BuildChoice(ticket, ticketHelper, ticketData);

            string team = ticket.BetTeam != null && ticket.BetTeam.Contains(BetTeamValue.H) ? Template.Match.homeTeam : Template.Match.awayTeam;
            string yesNo = ticket.BetTeam.Contains(BetTeamValue.Y) ? CoreBetList.lblYes : CoreBetList.lblNo;

            Template.betTeam = string.Join(null, new string[] { team, " <span class=\"", Favorite, "\">", yesNo.ToUpper(), "</span>" });

            Template.Handicap.handicap = null;
        }
    }
}