namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;

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

            this.Template.Handicap.handicap = null;
            string awayName = ticketHelper.GetTeamNameById(ticket.AwayId);

            string team = ticket.BetTeam.Contains("1") ? this.Template.Match.homeTeam : awayName;
            string draw_away = ticket.BetTeam.EndsWith("x") ? CoreBetList.lblDraw : this.Template.Match.awayTeam;

            string[] betteam = new string[] 
                                { 
                                    " <span class=\"", 
                                    BLFAVORITE, 
                                    "\">", 
                                    team, 
                                    "</span>", 
                                    "&nbsp;", 
                                    CoreBetList.lblOr.ToLower(), 
                                    " <span class=\"", 
                                    BLFAVORITE, 
                                    "\">", 
                                    "&nbsp;", 
                                    draw_away, 
                                    "</span>" 
                                };

            this.Template.betTeam = string.Join(null, betteam);
        }
    }
}