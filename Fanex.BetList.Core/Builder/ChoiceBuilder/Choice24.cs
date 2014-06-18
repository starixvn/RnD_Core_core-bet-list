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
        /// Builds the choice.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        /// <param name="ticketData">The ticket data.</param>
        protected override void BuildChoice(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            base.BuildChoice(ticket, ticketHelper, ticketData);

            Template.Handicap.handicap = null;

            string team = ticket.BetTeam.Contains("1") ? Template.Match.homeTeam : Template.Match.awayTeam;
            string draw_away = ticket.BetTeam.EndsWith(BetTeamValue.X) ? CoreBetList.lblDraw : Template.Match.awayTeam;

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
                                    draw_away,
                                    "</span>"
                                };

            Template.betTeam = string.Join(null, betteam);
        }
    }
}