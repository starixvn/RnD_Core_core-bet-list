namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Entities;
    using System.Collections.Generic;

    public class Choice221 : Choice4
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var situationName = ticketHelper.GetFastMarketSituation(ticket.Handicap2, ticket.BetTeam);

            var matchMinute = GenerateMatchMinute(ticket.Handicap1);

            Template.betTeam = situationName + " " + matchMinute;
        }

        protected override void BuildScore(ITicket ticket)
        {
            Template.Score = null;
        }

        protected virtual string GenerateMatchMinute(decimal handicap1)
        {
            return string.Format("({0}:00-{0}:59)", FormatMatchMinute(handicap1));
        }

        protected string FormatMatchMinute(decimal handicap1)
        {
            return handicap1.ToString().Length == 1 ? "0" + handicap1 : handicap1.ToString();
        }
    }
}