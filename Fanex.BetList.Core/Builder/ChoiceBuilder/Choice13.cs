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
        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            base.BuildBetTeamClassNameAndHandicap(ticket);

            Template.Handicap.handicap = null;
        }

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            string team = ticket.BetTeam != null && ticket.BetTeam.Contains(BetTeamValue.H) ? Template.Match.homeTeam : Template.Match.awayTeam;
            string yesNo = ticket.BetTeam != null && ticket.BetTeam.Contains(BetTeamValue.Y) ? CoreBetList.lblYes : CoreBetList.lblNo;

            Template.betTeam = string.Join(null, new string[] { team, " <span class=\"", Favorite, "\">", yesNo.ToUpper(), "</span>" });
        }
    }
}