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
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            string awayName = ticketHelper.GetTeamNameById(ticket.AwayId);
            string homeName = GetHomeTeamName(ticket, ticketHelper);
            string team = ticket.BetTeam.Contains("1") ? homeName : awayName;
            string draw_away = ticket.BetTeam.EndsWith(BetTeamValue.X) ? CoreBetList.lblDraw : awayName;

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

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            base.BuildBetTeamClassNameAndHandicap(ticket);

            Template.Handicap.handicap = null;
        }
    }
}