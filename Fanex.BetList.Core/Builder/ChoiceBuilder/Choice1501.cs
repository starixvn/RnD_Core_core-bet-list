namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Entities;

    /// <summary>
    /// Keno games.
    /// </summary>
    public class Choice1501 : Choice1
    {
        /// <summary>
        /// Builds the choice.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        /// <param name="ticketData">The ticket data.</param>
        protected override void BuildChoice(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            // Does not show handicap, match, and score.
            this.Template.Handicap = null;

            this.UpdateAllMatchMemberToNull();

            this.Template.Score = null;
            string kenoBetTypeID;

            this.Template.betTeam = ticketHelper.GetKenoBetChoiceName(ticket.BetTeam, out kenoBetTypeID);
            this.Template.BetType.betTypeName = ticketHelper.GetKenoBetTypeName(kenoBetTypeID);

            this.Template.betTeamClassName = BLFAVORITE;

            if (this.Template.League != null)
            {
                this.Template.League.sportTypeName = ticketHelper.GetSportNameById(ticket.SportTypeId);
                string kenoLeagueInfo = string.Join(null, new string[] { ticketHelper.GetBetTypeNameById(ticket.BetTypeId), " [", ticket.MatchId.ToString(), "]" });

                this.Template.League.LeagueName.leagueName = kenoLeagueInfo;
            }
        }
    }
}