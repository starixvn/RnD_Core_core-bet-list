namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;

    /// <summary>
    /// Bingo Pending.
    /// </summary>
    public class Choice23202 : Choice1
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

            this.Template.BetType.betTypeName = null;
            this.Template.Match.VS = null;
            this.Template.League.sportTypeName = null;
            this.Template.Score = null;
            this.Template.Handicap.handicap = null;

            this.Template.betTeam = string.Join(null, new string[] { CoreBetList.bingo, " - ", CoreBetList.pending });
            this.Template.betTeamClassName = BLFAVORITE;
        }
    }
}