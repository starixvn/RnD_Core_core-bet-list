namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;

    /// <summary>
    ///   Set x Game y Total Points Exact.
    /// </summary>
    public class Choice1328 : Choice1
    {
        /// <summary>
        /// Builds the bet team.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        /// <param name="ticketData">The ticket data.</param>
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            string choice = ticket.BetTeam;
            var betTeam = string.Empty;
            switch (choice)
            {
                case "01":
                    betTeam = "4 " + CoreBetList.points;
                    break;

                case "02":
                    betTeam = "5 " + CoreBetList.points;
                    break;

                case "03":
                    betTeam = "6 " + CoreBetList.points;
                    break;

                case "04":
                    betTeam = "7 " + CoreBetList.upPoints;
                    break;
            }

            // "&nbsp" because export format
            Template.betTeam = HtmlCharacters.NoneBreakingSpace + betTeam;
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            base.BuildBetTeamClassNameAndHandicap(ticket);

            Template.Handicap.handicap = null;
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.BetType.betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId, ticket.BetId);
        }
    }
}