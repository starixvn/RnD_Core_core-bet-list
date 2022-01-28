namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils;

    public class Choice9730 : Choice1
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var teamName = GetTeamName(ticket, ticketHelper);
            var teamNameFormat = $"<span class='underdog'>{teamName}</span>";
            Template.betTeam = teamNameFormat;
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = ConvertByBetType.Hdp(ticket.Handicap1);
        }

        private static string GetTeamName(ITicket ticket, ITicketHelper ticketHelper)
        {
            
            if (!string.IsNullOrWhiteSpace(ticket.TransDesc))
            {
                ticketHelper.GetTeamNameById(ticket.TransDesc);
            }

            return ticket.TransDesc;
        }
    }
}