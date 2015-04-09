namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;

    /// <summary>
    /// BetRadar BetType : Home to Win Both Halves - 133.
    /// </summary>
    public class Choice133 : Choice1
    {
        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.betTeamClassName = Favorite;
            Template.Handicap.handicap = null;
        }

        protected override void BuildScore(ITicket ticket)
        {
            Template.Score = null;
        }

        /// <summary>
        /// Builds the bet team.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        /// <param name="ticketData">The ticket data.</param>
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var betTeam = string.Empty;

            switch (ticket.BetTeam.ToLower())
            {
                case BetTeamValue.Y:
                    betTeam = CoreBetList.lblYes;
                    break;

                case BetTeamValue.N:
                    betTeam = CoreBetList.lblNo;
                    break;
            }

            Template.betTeam = betTeam;
        }
    }
}