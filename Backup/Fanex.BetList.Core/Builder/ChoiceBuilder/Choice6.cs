namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Constants;
    using Entities;
    using Fanex.BetList.Core.App_GlobalResources;

    /// <summary>
    /// Total Goal.
    /// </summary>
    public class Choice6 : Choice1
    {
        /// <summary>
        /// Set bet team to template.
        /// </summary>
        /// <param name="ticket"> Total goal ticket.</param>
        /// <param name="ticketHelper"> Valid ticker helper instance.</param>
        /// <param name="ticketData"> Ticket data.</param>
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            // &nbsp; because export format
            var betTeam = ticket.BetTeam.ToLowerInvariant();

            switch (betTeam)
            {
                case "4-over":
                case "4&over":
                    betTeam = CoreBetList.FourAndOver;
                    break;

                case "7-over":
                case "7&over":
                    betTeam = CoreBetList.SevenAndOver;
                    break;
            }

            Template.betTeam = string.Join(null, new string[] { HtmlCharacters.NoneBreakingSpace, betTeam });
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = null;
            Template.betTeamClassName = Favorite;
        }
    }
}