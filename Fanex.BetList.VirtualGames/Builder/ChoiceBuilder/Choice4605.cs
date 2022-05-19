namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;
    using System.Linq;

    public class Choice4605 : Choice4601
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var noChoices = new List<int>
            {
                VirtualGamesBetTypes.SoccerTowers,
                VirtualGamesBetTypes.BasketballTowers,
                VirtualGamesBetTypes.CricketTowers
            };

            if (noChoices.Any(i => i == ticket.BetTypeId))
            {
                Template.betTeam = string.Empty;
            }
            else
            {
                base.BuildBetTeam(ticket, ticketHelper, ticketData);
            }
        }

        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
        {
            var tableCode = GetTableCode(ticket.BetCheck);

            Template.League.LeagueName.leagueName = string.IsNullOrEmpty(tableCode) ? string.Empty : $"[{ticket.BetCheck}]";
        }

    }
}