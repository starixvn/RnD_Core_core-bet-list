namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;

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

            string awayName = ticketHelper.GetTeamNameById(ticket.AwayId);

            string team = ticket.BetTeam != null && ticket.BetTeam.Contains(BetTeamValue.H) ? this.Template.Match.homeTeam : awayName;
            string yesNo = ticket.BetTeam.Contains(BetTeamValue.Y) ? CoreBetList.lblYes : CoreBetList.lblNo;

            this.Template.betTeam = string.Join(null, new string[] { team, " <span class=\"", BLFAVORITE, "\">", yesNo.ToUpper(), "</span>" });

            this.Template.Handicap.handicap = null;
        }
    }
}