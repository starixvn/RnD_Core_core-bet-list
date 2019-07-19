namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Entities;
    using Utils;

    /// <summary>
    ///   Set x Games y and y+1 Points.
    /// </summary>
    public class Choice1334 : Choice1
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            bool betOnUnder = ticket.BetTeam == "1";
            bool betOnOver = ticket.BetTeam == "2";

            if (betOnUnder)
            {
                Template.betTeam = $"<span class=\"underdog\">{CoreBetList.under}</span>";
            }
            else if (betOnOver)
            {
                Template.betTeam = $"<span class=\"favorite\">{CoreBetList.over}</span>";
            }
            else
            {
                Template.betTeam = $"<span class=\"underdog\">{CoreBetList.exactly}</span>";
            }
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            base.BuildBetTeamClassNameAndHandicap(ticket);

            Template.Handicap.handicap = ConvertByBetType.Hdp(ticket.Handicap1);
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.BetType.betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId, ticket.RefNo, betId: ticket.BetId);
        }
    }
}