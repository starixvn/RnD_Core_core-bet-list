namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;

    /// <summary>
    /// Double Chance.
    /// </summary>
    public class Choice24 : Choice1
    {
        /// <summary>
        /// Builds the bet team.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        /// <param name="ticketData">The ticket data.</param>
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            string team = ticket.BetTeam.Contains("1") ? Template.Match.homeTeam : Template.Match.awayTeam;
            string drawAway = ticket.BetTeam.EndsWith(BetTeamValue.X) ? CoreBetList.lblDraw : Template.Match.awayTeam;

            string[] betteam = new string[]
                                {
                                    " <span class=\"",
                                    Favorite,
                                    "\">",
                                    team,
                                    "</span>",
                                    HtmlCharacters.NoneBreakingSpace,
                                    CoreBetList.lblOr.ToLower(),
                                    " <span class=\"",
                                    Favorite,
                                    "\">",
                                    HtmlCharacters.NoneBreakingSpace,
                                    drawAway,
                                    "</span>"
                                };

            Template.betTeam = string.Join(null, betteam);
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            base.BuildBetTeamClassNameAndHandicap(ticket);

            Template.Handicap.handicap = null;
        }
    }
}