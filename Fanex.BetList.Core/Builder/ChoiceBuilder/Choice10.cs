namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Entities;

    /// <summary>
    /// Outright bet type.
    /// </summary>    
    public class Choice10 : Choice1
    {
        /// <summary>
        /// Builds the choice.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        /// <param name="ticketData">The ticket data.</param>
        protected override void BuildChoice(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            this.Template.Handicap.handicap = null;

            if (this.Template.Match != null)
            {
                this.Template.Match.homeTeam = null;
                this.Template.Match.awayTeam = null;
                this.Template.Match.VS = null;
                this.Template.Match.home_firstGoal_lastGoal = null;
                this.Template.Match.away_firstGoal_lastGoal = null;
            }

            if (!ticket.IsLive)
            {
                this.Template.Score = null;
            }
            else
            {
                this.Template.Score.homeScore = ticket.LiveHomeScore.ToString();
                this.Template.Score.awayScore = ticket.LiveAwayScore.ToString();            
            }

            this.Template.BetType.betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId);
            this.Template.betTeam = ticketHelper.GetTeamNameById(ticket.HomeId == 0 ? ticket.AwayId : ticket.HomeId);
            this.Template.betTeamClassName = BLFAVORITE;

            if (this.Template.League != null)
            {
                this.Template.League.sportTypeName = ticketHelper.GetSportNameById(ticket.SportTypeId);
                this.Template.League.LeagueName.leagueName = ticketHelper.GetLeagueNameById(ticket.LeagueId);
            }
        }
    }
}