namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;
    using System.Collections.Generic;

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

            if (noChoices.Exists(i => i == ticket.BetTypeId))
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
            var gameId3rd = GetTransId3rd(ticket.BetCheck);

            Template.League.LeagueName.leagueName = $"[{gameId3rd}]";
        }

    }
}