namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Entities;
    using Utils;

    /// <summary>
    ///   Match Total Games 3-way.
    /// </summary>
    public class Choice1307 : Choice1
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

            bool betOver = ticket.BetTeam == "1";
            bool betUnder = ticket.BetTeam == "2";

            if (betOver)
            {
                Template.betTeam = CoreBetList.over;
            }
            else
            {
                if (betUnder)
                {
                    Template.betTeam = CoreBetList.under;
                }
                else
                {
                    Template.betTeam = string.Format(CoreBetList.betweenXandY, ConvertByBetType.Hdp(ticket.Handicap2), ConvertByBetType.Hdp(ticket.Handicap1));
                }
            }

            Template.Handicap.handicap = betOver ? ConvertByBetType.Hdp(ticket.Handicap1) : betUnder ? ConvertByBetType.Hdp(ticket.Handicap2) : null;

            // Update betTeamClassName
            Template.betTeamClassName = betOver ? Favorite : Underdog;
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.BetType.betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId, ticket.BetId);
        }
    }
}