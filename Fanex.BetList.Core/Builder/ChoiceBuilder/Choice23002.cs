namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;

    /// <summary>
    /// Casino pending.
    /// </summary>
    public class Choice23002 : Choice1
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
         
            this.Template.Match.VS = null;
            this.Template.League.sportTypeName = null;
            this.Template.Score = null;
            this.Template.Handicap.handicap = null;

            this.Template.betTeam = CoreBetList.casinoGame;
            this.Template.betTeamClassName = BLFAVORITE;
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper)
        {
            this.Template.BetType.betTypeName = null;
        }
    }
}