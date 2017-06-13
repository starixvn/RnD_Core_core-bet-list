namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Entities;

    public class Choice1031 : Choice1
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            string lotteryBetTypeId;

            Template.betTeam = ticketHelper.GetLotteryBetChoiceName(ticket.BetTeam, out lotteryBetTypeId);
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            string lotteryBetTypeId;

            ticketHelper.GetLotteryBetChoiceName(ticket.BetTeam, out lotteryBetTypeId);
            Template.BetType.betTypeName = ticketHelper.GetLotteryBetTypeName(lotteryBetTypeId);
        }

        protected override void BuildSport(ITicket ticket, ITicketHelper ticketHelper)
            => Template.League.sportTypeName = ticketHelper.GetSportNameById(ticket.SportTypeId);

        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
        {
            string gameTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId);

            Template.League.LeagueName.leagueName = $"{gameTypeName} [{ticket.MatchId}]";
        }

        protected override void BuildScore(ITicket ticket)
            => Template.Score = null;

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap = null;
            Template.betTeamClassName = Favorite;
        }

        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
            => Template.Match = null;
    }
}