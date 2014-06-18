namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;

    /// <summary>
    /// BetRadar BetType : Home to Win Both Halves - 133.
    /// </summary>
    public class Choice133 : Choice1
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
            this.Template.Handicap = null;
            this.Template.OddMixParlay = null;
            this.Template.Score = null;
            this.Template.betTeamClassName = Favorite;
            this.Template.betTeam = GetBetTeam(ticket.BetTeam);
        }

        /// <summary>
        /// Gets the bet team.
        /// </summary>
        /// <param name="betTeam">The bet team.</param>
        /// <returns>Yes or No.</returns>
        protected virtual string GetBetTeam(string betTeam)
        {
            betTeam = betTeam.ToLower();

            if (betTeam == "y")
            {
                return CoreBetList.lblYes;
            }
            else if (betTeam == "n")
            {
                return CoreBetList.lblNo;
            }

            return string.Empty;
        }
    }
}