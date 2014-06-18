namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils;

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
                this.Template.betTeam = CoreBetList.over;
            }
            else
            {
                if (betUnder)
                {
                    this.Template.betTeam = CoreBetList.under;
                }
                else
                {
                    this.Template.betTeam = string.Format(CoreBetList.betweenXandY, ConvertByBetType.Hdp(ticket.Handicap2), ConvertByBetType.Hdp(ticket.Handicap1));
                }
            }

            this.Template.Handicap.handicap = betOver ? ConvertByBetType.Hdp(ticket.Handicap1) : betUnder ? ConvertByBetType.Hdp(ticket.Handicap2) : null;

            // Update betTeamClassName
            this.Template.betTeamClassName = betOver ? BLFAVORITE : BLUNDERDOG;
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper)
        {
            this.Template.BetType.betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId, ticket.BetId);
        }
    }
}